using ChildGrowth.Domain.Enum;

namespace ChildGrowth.API.Payload.Response.GrowthRecord;

public class GrowthRecordDataChartResponse
{
    public EGrowthRecordMode Mode { get; set; }
    public List<GrowthRecordDataChartItemResponse> Data { get; set; }
}

public class GrowthRecordDataChartItemResponse
{
    public DateOnly Date { get; set; }
    public decimal? Bmi { get; set; }
    public decimal? Height { get; set; }
    public decimal? Weight { get; set; }
}