using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ChildGrowth.API.Enums;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Utils;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.Identity.Client;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using Microsoft.IdentityModel.Tokens;

namespace ChildGrowth.API.Services.Implement;

public class UserService : BaseService<UserService>, IUserService
{
    private IConfiguration _config;
    public UserService(IUnitOfWork<ChildGrowDBContext> unitOfWork, ILogger<UserService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration config) : base(unitOfWork, logger, mapper, httpContextAccessor)
    {
        _config = config;
    }
    public async Task<IPaginate<UserResponse>> GetUserAsync(int page, int size)
    {
        var users = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
            page: page,
            size: size,
            orderBy: x => x.OrderByDescending(x => x.CreatedAt)
        );
        return _mapper.Map<IPaginate<UserResponse>>(users);
    }

    public async Task<SignUpResponse> SignUp(SignUpRequest request)
    {
        try
        {
            var isUserExist = IsUserExist(request.Username);
            if (isUserExist.Result)
            {
                throw new Exception("User already exist");
            }
            var user = _mapper.Map<User>(request);
            user.Password = PasswordUtil.HashPassword(request.Password);
            await _unitOfWork.GetRepository<User>().InsertAsync(user);
            await _unitOfWork.CommitAsync();
            return new SignUpResponse
            {
                AccessToken = GetAccessToken(user),
            };
        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw new Exception(e.Message);
        }
    }
    
    private async Task<bool> IsUserExist(string username)
    {
        var user = await _unitOfWork.GetRepository<User>(
            ).SingleOrDefaultAsync(predicate: u => u.Username == username);
        return user != null;
    }

    private string GetAccessToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_config["Jwt:Issuer"]
            , _config["Jwt:Audience"]
            , new Claim[]
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.UserType),
            },
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}