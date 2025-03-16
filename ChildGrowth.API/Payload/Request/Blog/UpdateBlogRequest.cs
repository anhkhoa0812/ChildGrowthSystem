using ChildGrowth.API.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChildGrowth.API.Payload.Request.Blog;

public class UpdateBlogRequest
{
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Category { get; set; }

    public string? Tags { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly? PublishDate { get; set; }

    public string? Status { get; set; }

    [Url(ErrorMessage = "Invalid URL format.")]
    public string? ImageUrl { get; set; }

    public int? ViewCount { get; set; }

    public int? LikeCount { get; set; }

    public int? CommentCount { get; set; }

    public string? MetaDescription { get; set; }

    public bool? FeaturedStatus { get; set; }
}
