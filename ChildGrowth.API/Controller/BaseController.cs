using ChildGrowth.API.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ChildGrowth.API.Controller;

[Route(ApiEndPointConstant.ApiEndpoint)]
[ApiController]
public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    protected ILogger<T> _logger;

    public BaseController(ILogger<T> logger)
    {
        _logger = logger;
    }
}