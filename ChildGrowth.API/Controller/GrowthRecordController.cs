using AutoMapper;
using ChildGrowth.API.Constants;
using ChildGrowth.API.Payload.Request.GrowthRecord;
using ChildGrowth.API.Payload.Response.GrowthRecord;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.Domain.Paginate;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller
{
    [ApiController]
    public class GrowthRecordController : BaseController<GrowthRecordController>
    {
        private readonly IGrowthRecordService _growthRecordService;
        private readonly IMapper _mapper;

        public GrowthRecordController(
            ILogger<GrowthRecordController> logger,
            IGrowthRecordService growthRecordService,
            IMapper mapper)
            : base(logger)
        {
            _growthRecordService = growthRecordService;
            _mapper = mapper;
        }

        [HttpGet(ApiEndPointConstant.GrowthRecord.GrowthRecordEndPoint)]
        [ProducesResponseType(typeof(IPaginate<GrowthRecordResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByChildId(
            [FromQuery] int childId,
            [FromQuery] int page = 1,
            [FromQuery] int size = 30)
        {
            var records = await _growthRecordService.GetGrowthRecordByChildIdAsync(page, size, childId);
            return Ok(records);
        }


        [HttpGet(ApiEndPointConstant.GrowthRecord.GetById)]
        [ProducesResponseType(typeof(GrowthRecordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int recordId)
        {
            var record = await _growthRecordService.GetGrowthRecordByIdAsync(recordId);
            if (record == null)
                return NotFound();
            return Ok(record);
        }


        [HttpPost(ApiEndPointConstant.GrowthRecord.Create)]
        [ProducesResponseType(typeof(GrowthRecordResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateGrowthRecordRequest request)
        {
            var record = await _growthRecordService.CreateGrowthRecordAsync(request);
            return CreatedAtAction(nameof(GetById), new { recordId = record.RecordId }, record);
        }

        [HttpPut(ApiEndPointConstant.GrowthRecord.Update)]
        [ProducesResponseType(typeof(GrowthRecordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int recordId, [FromBody] UpdateGrowthRecordRequest request)
        {
            var record = await _growthRecordService.UpdateGrowthRecordAsync(recordId, request);
            return Ok(record);
        }


        [HttpDelete(ApiEndPointConstant.GrowthRecord.Delete)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int recordId)
        {
            var deleted = await _growthRecordService.DeleteGrowthRecordAsync(recordId);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
