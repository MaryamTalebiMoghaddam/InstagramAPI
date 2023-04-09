using InstagramAPI.Models;
using InstagramAPI.Models.DTO;

namespace InstagramAPI.Repository.IRepository
{
    public interface IPostRepository
    {
        Task<Post> CreatePost(PostDTO postDTO);
        Task<string> UpdatePostCaption(PostCaptionDTO updateCaption);
        Task<bool> UpdatePostTurnOffcommenting(PostTurnOffCommentingDTO postTurnOffCommentingDTO);
        Task<bool> DeletePost(int postId, int userId);
        Task<bool> Archive(int postId, int userId);
        Task<Post> GetPostByPostId(GetPostByPostIdDTO postIdDTO);
        Task<List<Post>> GetPostByPageId(GetPostsByPageDTO byPageDTO, PeriodDTO period);
        Task SaveAsync();
    }
}
