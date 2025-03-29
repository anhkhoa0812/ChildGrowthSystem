using AutoMapper;
using ChildGrowth.API.Payload.Request.GrowthRecord;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services.Implement;

public class GrowthRecordService : BaseService<GrowthRecord>, IGrowthRecordService
{
    public GrowthRecordService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<GrowthRecord> logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
    }


    public async Task<GrowthRecordResponse> CreateGrowthRecordAsync(CreateGrowthRecordRequest request)
    {
        var growthRecord = _mapper.Map<GrowthRecord>(request);
        await _unitOfWork.GetRepository<GrowthRecord>().InsertAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }

    public async Task<GrowthRecordResponse> GetGrowthRecordByIdAsync(int recordId)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null)
            throw new KeyNotFoundException("Growth record not found");
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }

    public async Task<GrowthRecordResponse> UpdateGrowthRecordAsync(int recordId, UpdateGrowthRecordRequest request)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null)
            throw new KeyNotFoundException("Growth record not found");

        _mapper.Map(request, growthRecord);
        _unitOfWork.GetRepository<GrowthRecord>().UpdateAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GrowthRecordResponse>(growthRecord);
    }

    public async Task<bool> DeleteGrowthRecordAsync(int recordId)
    {
        var growthRecord = await _unitOfWork.GetRepository<GrowthRecord>()
            .SingleOrDefaultAsync(predicate: x => x.RecordId == recordId);
        if (growthRecord == null) return false;

        _unitOfWork.GetRepository<GrowthRecord>().DeleteAsync(growthRecord);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<IPaginate<GrowthRecordResponse>> GetGrowthRecordByChildIdAsync(int page, int size, int childId)
    {
        var growthRecords = await _unitOfWork.GetRepository<GrowthRecord>().GetPagingListAsync(
            predicate: x => x.ChildId == childId,
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(y => y.RecordDate)
        );
        return _mapper.Map<IPaginate<GrowthRecordResponse>>(growthRecords);
    }

    public async Task<GrowthRecordDataChartResponse> GetGrowthRecordDataChartByChildIdAsync(int childId,
        EGrowthRecordMode mode)
    {
        // Retrieve the child entity to get the birthdate
        var child = await _unitOfWork.GetRepository<Child>().SingleOrDefaultAsync(predicate: x => x.ChildId == childId);
        if (child == null)
        {
            throw new KeyNotFoundException("Child not found");
        }

        // Fetch all growth records for the child
        var records = await _unitOfWork.GetRepository<GrowthRecord>()
            .GetListAsync(predicate: x => x.ChildId == childId);

        // Initialize the list to hold chart data
        var data = new List<GrowthRecordDataChartItemResponse>();

        // Process data based on the mode
        if (mode == EGrowthRecordMode.Last12Months)
        {
            // Calculate the date 12 months ago from today
            var cutoffDate = DateOnly.FromDateTime(DateTime.Today).AddYears(-1);

            // Filter records from the last 12 months and map to chart items
            data = records
                .Where(r => r.RecordDate.HasValue && r.RecordDate.Value >= cutoffDate && r.Bmi.HasValue)
                .OrderBy(r => r.RecordDate.Value)
                .Select(r => new GrowthRecordDataChartItemResponse
                {
                    Date = r.RecordDate.Value,
                    Bmi = r.Bmi.Value,
                    Height = r.Height.Value,
                    Weight = r.Weight.Value
                })
                .ToList();
        }
        else if (mode == EGrowthRecordMode.YearlyAverage)
        {
            // Convert birthdate to DateTime for year calculations
            var birthDateTime = child.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);

            // Group records by age year, calculate average BMI, and map to chart items
            data = records
                .Where(r => r.AgeAtRecord.HasValue && r.Bmi.HasValue)
                .GroupBy(r => r.AgeAtRecord.Value / 12) // Assuming AgeAtRecord is in months
                .Select(g => new GrowthRecordDataChartItemResponse
                {
                    Date = DateOnly.FromDateTime(birthDateTime.AddYears(g.Key)),
                    Bmi = g.Average(r => r.Bmi.Value),
                    Height = g.Average(r => r.Height.Value),
                    Weight = g.Average(r => r.Weight.Value)
                })
                .OrderBy(item => item.Date)
                .ToList();
        }

        // Return the response with the mode and data
        return new GrowthRecordDataChartResponse
        {
            Mode = mode,
            Data = data
        };
    }
}