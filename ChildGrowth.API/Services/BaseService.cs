using AutoMapper;
using ChildGrowth.Domain.Context;
using ChildGrowth.Repository.Interfaces;

namespace ChildGrowth.API.Services;

public abstract class BaseService<T> where T : class
{
    protected IUnitOfWork<ChildGrowDBContext> _unitOfWork;
    protected ILogger<T> _logger;
    protected IMapper _mapper;
    protected IHttpContextAccessor _httpContextAccessor;

    public BaseService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<T> logger, IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    
}