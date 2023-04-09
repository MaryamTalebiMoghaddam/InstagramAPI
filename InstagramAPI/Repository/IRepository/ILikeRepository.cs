using InstagramAPI.Models.DTO;

namespace InstagramAPI.Repository.IRepository
{
    public interface ILikeRepository
    {
        Task<bool> DisLike(PostLikeDTO dislikeDTO);
        Task<bool> Like(PostLikeDTO likeDTO);
        Task<int> GetLikes(PostGetLikesDTO getLikesDTO);
        
    }
}
