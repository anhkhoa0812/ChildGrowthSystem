using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? AuthorId { get; set; }

    public string? Category { get; set; }

    public string? Tags { get; set; }

    public DateTime? PublishDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Status { get; set; }

    public string? ImageUrl { get; set; }

    public int? ViewCount { get; set; }

    public int? LikeCount { get; set; }

    public int? CommentCount { get; set; }

    public string? MetaDescription { get; set; }

    public bool? FeaturedStatus { get; set; }

    public virtual User? Author { get; set; }
}
