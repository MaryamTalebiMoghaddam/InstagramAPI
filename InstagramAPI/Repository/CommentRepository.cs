using InstagramAPI.Data;
using InstagramAPI.Models;
using InstagramAPI.Models.DTO;
using InstagramAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InstagramAPI.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public CommentRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<Comment> CreateComment(PostCommentsCreateDTO createDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(t => t.Id == createDTO.PageId);
            var post = user.Posts.SingleOrDefault(t => t.PostId == createDTO.PostId && t.CommentFlag==true);
            try
            {
                if (post != null && user != null)
                {
                    Comment comment = new()
                    {
                        CommentDate = DateTime.Now,
                        CommentText = createDTO.Text,
                        PosterPageId = createDTO.MyPageId

                    };
                    post.Comments.Add(comment);
                    await _db.SaveChangesAsync();
                    return comment;
                }
                
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while creating comment", ex);
            }
        }

        public async Task<bool> DeletePostComments(PostCommentDeleteDTO deleteDTO)
        {
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(t => t.Id == deleteDTO.PageId);
                var post = user.Posts.SingleOrDefault(t => t.PostId == deleteDTO.PostId);
                var comment = post.Comments.SingleOrDefault(t => t.CommentId == deleteDTO.CommentId && t.MyPageId == deleteDTO.MyPageId);
                if (user != null && post != null && comment != null)
                {
                    _db.Remove(comment);
                    await _db.SaveChangesAsync();
                    return true;
                }                               
                    return false;                
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while deleting", ex);
            }
        }

        public async Task<List<Comment>> GetAllComment(GetWithFilterDTO filterDTO)
        {
            try
            {
                var post = await _db.Posts.Include(p => p.Comments)
                                    .SingleOrDefaultAsync(p => p.PostId == filterDTO.PostId && p.CommentFlag==true);
                if (post != null)
                {
                    List<Comment> comments = post.Comments.Take(filterDTO.Take).Skip(filterDTO.Skip).ToList();
                    return comments;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while getting all comments", ex);
            }
            
        }

        public async Task<int> GetCommentsCount(PostCommentsCountDTO countDTO)
        {
            try
            {
                var targetUser = await _db.Users.SingleOrDefaultAsync(t => t.Id == countDTO.PageId);

                if (targetUser != null)
                {
                    var post = targetUser.Posts.SingleOrDefault(t => t.PostId == countDTO.PostId  && t.CommentFlag == true);

                    if (post != null)
                    {
                        int commentCount = post.Comments.Count();
                        return commentCount;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting comments count", ex);
            }            
        }
    }
}
