using InstagramAPI.Models;
using InstagramAPI.Models.DTO;

namespace InstagramAPI.Repository.IRepository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComment(GetWithFilterDTO filterDTO);
        Task<int> GetCommentsCount(PostCommentsCountDTO countDTO);
        Task<Comment> CreateComment(PostCommentsCreateDTO createDTO);
        Task<bool> DeletePostComments(PostCommentDeleteDTO deleteDTO);
    }
}
