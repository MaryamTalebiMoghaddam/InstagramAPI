using InstagramAPI.Data;
using InstagramAPI.Models;
using InstagramAPI.Models.DTO;
using InstagramAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace InstagramAPI.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly User _user;
        public LikeRepository(ApplicationDbContext db, User user)
        {
            _db = db;
            _user = user;
        }

        public async Task<int> GetLikes(PostGetLikesDTO getLikesDTO)
        {
            try
            {
                var page = await _db.Users.SingleOrDefaultAsync(t => t.Id == getLikesDTO.PageId);
                var post =  page.Posts.SingleOrDefault(t => t.PostId == getLikesDTO.PostId);
                if (post != null)
                {
                    return post.Likes.Count();
                    
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting likes", ex);
            }
        }


        public async Task<bool> Like(PostLikeDTO likeDTO)
        {
            try
            {
                var page = await _db.Users.SingleOrDefaultAsync(t => t.Id == likeDTO.PageId);
                var post = page.Posts.SingleOrDefault(t => t.PostId == likeDTO.PostId);
                if (post != null )
                {
                    var likeStatus = post.Likes.SingleOrDefault(t => t.LikeId == likeDTO.LikerPageId);
                    if (likeStatus == null)
                    {
                        Like like = new Like()
                        {
                            PostId = likeDTO.PostId,
                            PageId=likeDTO.PageId,                            
                        };
                        _db.Likes.Add(like);
                        await _db.SaveChangesAsync();
                        return true;
                    }                                        
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while liking post", ex);
            }
        }

        public async Task<bool> DisLike(PostLikeDTO dislikeDTO)
        {
            try
            {
                var page = await _db.Users.SingleOrDefaultAsync(t => t.Id == dislikeDTO.PageId);
                var post = page.Posts.SingleOrDefault(t => t.PostId == dislikeDTO.PostId);
                if (post != null)
                {
                    var dislikeStatus = post.Likes.SingleOrDefault(t => t.LikeId == dislikeDTO.LikerPageId);
                    if (dislikeStatus != null)
                    {
                        _db.Remove(dislikeStatus);
                        await _db.SaveChangesAsync();
                        return true;
                    }                    
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while disliking post", ex);
            }
        }
    }
}
