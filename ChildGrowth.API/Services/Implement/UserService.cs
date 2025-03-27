using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ChildGrowth.API.Payload.Request.User;
using ChildGrowth.API.Payload.Response.User;
using ChildGrowth.API.Services.Interfaces;
using ChildGrowth.API.Utils;
using ChildGrowth.Domain.Context;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;
using ChildGrowth.Domain.Filter.ModelFilter;
using ChildGrowth.Domain.Paginate;
using ChildGrowth.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IPaginate<UserResponse>> GetUserAsync(int page, int size, UserFilter filter, string? sortBy, bool isAsc)
    {
        var users = await _unitOfWork.GetRepository<User>().GetPagingListAsync(
            predicate: x => x.UserType == RoleEnum.Member.ToString() || x.UserType == RoleEnum.Doctor.ToString(),
            page: page,
            size: size,
            filter: filter,
            sortBy: sortBy,
            isAsc: isAsc,
            include: x => x.Include(u => u.UserMemberships).ThenInclude(um => um.Plan) // Include MembershipPlan
        );
        return _mapper.Map<IPaginate<UserResponse>>(users);
    }

    public async Task<UserResponse> GetUserByIdAsync(int id)
    {
        var userResponse = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
            predicate: u => u.UserId == id,
            include: x => x.Include(u => u.UserMemberships).ThenInclude(um => um.Plan));
        return _mapper.Map<UserResponse>(userResponse);
    }

    public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
    {
        try
        {
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.UserId == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var emailExist = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector: u => u.Email,
                predicate: u => u.Email == request.Email && u.UserId != id);
            if (emailExist != null)
            {
                throw new Exception("Email already exists");
            }
            var userUpdate = _mapper.Map(request, user);
            _unitOfWork.GetRepository<User>().UpdateAsync(userUpdate);
            await _unitOfWork.CommitAsync();
            return true;
        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw new Exception(e.Message);
        }
    }

    public async Task<SignInResponse> SignUp(SignUpRequest request)
    {
        try
        {
            var userExist = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == request.Username);
            if (userExist != null)
            {
                throw new Exception("Username already exists");
            }
            var emailExist = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                selector:u => u.Email,
                predicate: u => u.Email == request.Email);
            if (emailExist != null)
            {
                throw new Exception("Email already exists");
            }
            var user = _mapper.Map<User>(request);
            user.Password = PasswordUtil.HashPassword(request.Password);
            await _unitOfWork.GetRepository<User>().InsertAsync(user);
            await _unitOfWork.CommitAsync();
            return new SignInResponse()
            {
                AccessToken = GetAccessToken(user),
            };
        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            throw new Exception(e.Message);
        }
    }

    public async Task<SignInResponse> SignIn(SignInRequest request)
    {
        try
        {
            var isUserExist = IsUserExist(request.Username);
            if (!isUserExist.Result)
            {
                throw new Exception("User not found");
            }
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(predicate: u => u.Username == request.Username && u.Password == PasswordUtil.HashPassword(request.Password));
            if (user == null)
            {
                throw new Exception("Invalid username or password");
            }
            var accessToken = GetAccessToken(user);
            return new SignInResponse()
            {
                UserId = user.UserId,
                Role = user.UserType,
                AccessToken = accessToken,
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
                new Claim("userId", user.UserId.ToString()),
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