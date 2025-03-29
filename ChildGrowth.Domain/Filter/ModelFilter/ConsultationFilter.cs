using System.Linq.Expressions;
using ChildGrowth.Domain.Entities;
using ChildGrowth.Domain.Enum;

namespace ChildGrowth.Domain.Filter.ModelFilter;

public class ConsultationFilter : IFilter<Consultation>
{
    public int? ConsultationId { get; set; }
    public string? ParentFullName { get; set; }
    
    public EConsultationStatus? Status { get; set; }
    public Expression<Func<Consultation, bool>> ToExpression()
    {
        return consultation => (string.IsNullOrEmpty(ParentFullName) || consultation.Parent.FullName.Contains(ParentFullName)) &&
                               (!ConsultationId.HasValue || consultation.ConsultationId == ConsultationId) &&
                               (!Status.HasValue || consultation.Status == Status.ToString());
    }
}