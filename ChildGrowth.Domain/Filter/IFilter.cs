using System.Linq.Expressions;

namespace ChildGrowth.Domain.Filter;

public interface IFilter<T>
{
    Expression<Func<T, bool>> ToExpression();
}