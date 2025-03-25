using System.Linq.Expressions;
using ChildGrowth.Domain.Entities;

namespace ChildGrowth.Domain.Filter.ModelFilter;

public class ConsultationFilter : IFilter<Consultation>
{
    public string? ParentFullName { get; set; }
    public Expression<Func<Consultation, bool>> ToExpression()
    {
        return consultation => (string.IsNullOrEmpty(ParentFullName) || consultation.Parent.FullName.Contains(ParentFullName));
    }
}