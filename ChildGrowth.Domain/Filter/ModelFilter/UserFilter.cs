using System.Linq.Expressions;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;

namespace ChildGrowth.Domain.Filter.ModelFilter;

public class UserFilter : IFilter<User>
{
    public string? Name { get; set; }
    public EUserType? UserType { get; set; }
    public Expression<Func<User, bool>> ToExpression()
    {
        return user => 
            ((!UserType.HasValue || user.UserType == UserType.ToString()) && 
            string.IsNullOrEmpty(Name) || user.FullName.Contains(Name));
    }
}