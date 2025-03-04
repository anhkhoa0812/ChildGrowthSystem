using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class StandardGrowthMetric
{
    public int MetricId { get; set; }

    public string? Gender { get; set; }

    public int? AgeInMonths { get; set; }

    public int? Percentile { get; set; }

    public decimal? WeightMin { get; set; }

    public decimal? WeightMax { get; set; }

    public decimal? HeightMin { get; set; }

    public decimal? HeightMax { get; set; }

    public decimal? Bmimin { get; set; }

    public decimal? Bmimax { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Source { get; set; }

    public string? AgeGroup { get; set; }

    public string? Description { get; set; }
}
