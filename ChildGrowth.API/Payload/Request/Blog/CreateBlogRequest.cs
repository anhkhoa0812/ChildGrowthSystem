using ChildGrowth.API.Utils;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ChildGrowth.API.Payload.Request.Blog;

public class CreateBlogRequest
{
    [Required(ErrorMessage = "Title is required.")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Content is required.")]
    public string Content { get; set; } = string.Empty;

    [Required(ErrorMessage = "AuthorId is required.")]
    public int AuthorId { get; set; }

    [MaxLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
    public string? Category { get; set; }

    [MaxLength(200, ErrorMessage = "Tags cannot exceed 200 characters.")]
    public string? Tags { get; set; }

    [JsonConverter(typeof(DateOnlyJsonConverter))] 
    public DateOnly? PublishDate { get; set; }

    public string? Status { get; set; } = "Draft"; 

    [Url(ErrorMessage = "Invalid URL format.")]
    public string? ImageUrl { get; set; }

    public int ViewCount { get; set; } = 0;

    public int LikeCount { get; set; } = 0;

    public int CommentCount { get; set; } = 0;

    [MaxLength(500, ErrorMessage = "Meta description cannot exceed 500 characters.")]
    public string? MetaDescription { get; set; }

    public bool FeaturedStatus { get; set; } = false;
}
