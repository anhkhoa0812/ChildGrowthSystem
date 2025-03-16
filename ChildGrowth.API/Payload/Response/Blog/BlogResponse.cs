namespace ChildGrowth.API.Payload.Response.Blog
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; }
        public DateTime PublishDate { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
    }

}
