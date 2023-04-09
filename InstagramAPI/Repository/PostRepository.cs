using InstagramAPI.Data;
using InstagramAPI.Models;
using InstagramAPI.Models.DTO;
using InstagramAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Permissions;




namespace InstagramAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public PostRepository(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;

        }



        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


        public async Task<bool> Archive(int postId, int userId)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(t => t.Id == userId);

                var post = user.Posts.SingleOrDefault(t => t.PostId == postId);
                if (post == null || user == null)
                {
                    return false;
                }

                post.ArchivedPost = true;
                await SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while archiving post", ex);
            }


        }

        public async Task<Post> CreatePost(PostDTO postDTO)
        {

            try
            {
                Post newPost = new()
                {
                    Caption = postDTO.Caption,
                    MainFileIsImage = postDTO.MainFileIsImage,
                    CoverImageUrl = postDTO.CoverImageUrl,
                    PostCreateTime = DateTime.Now,
                    PostFiles = postDTO.PostFiles,
                    ArchivedPost = false,

                };
                if (newPost != null)
                {
                    await _db.Posts.AddAsync(newPost);
                    await SaveAsync();
                    return newPost;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while creating post", ex);
            }

        }

        public async Task<bool> DeletePost(int postId, int userId)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(t => t.Id == userId);
                var postToDelete = user.Posts.SingleOrDefault(t => t.PostId == postId);
                if (postToDelete != null)
                {
                    _db.Posts.Remove(postToDelete);
                    await SaveAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while deleting post", ex);
            }

        }

        public async Task<List<Post>> GetPostByPageId(GetPostsByPageDTO byPageDTO, PeriodDTO period)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(t => t.Id == byPageDTO.PageId);
                if (user != null)
                {
                    var posts = user.Posts.Where(t => !t.ArchivedPost)
                    .Take(period.Take)
                    .Skip(period.Skip);

                    return await Task.FromResult(posts.ToList());
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting posts", ex);
            }


        }

        public async Task<Post> GetPostByPostId(GetPostByPostIdDTO postIdDTO)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(t => t.Id == postIdDTO.TargetPageId);
                var post =  user.Posts.SingleOrDefault(t => t.PostId == postIdDTO.PostId && t.ArchivedPost == false);

                if (post != null)
                {
                    return post;
                }
                return null;
            }

            catch (Exception ex)
            {

                throw new Exception("Error occurred while getting post", ex);
            }
        }


        public async Task<string> UpdatePostCaption(PostCaptionDTO updateCaption)
        {
           
            try
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(p => p.Id == updateCaption.PageId);
                var post = user.Posts.SingleOrDefault(t => t.PostId == updateCaption.PostId );
                if (post != null && user!=null)
                {
                    post.Caption = updateCaption.Caption;
                    post.PostUpdateTime = DateTime.Now;
                    await _db.SaveChangesAsync();
                    return post.Caption;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating caption ", ex);
            }
        }


        public async Task<bool> UpdatePostTurnOffcommenting(PostTurnOffCommentingDTO postTurnOffCommentingDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(p => p.Id == postTurnOffCommentingDTO.PageId);
            var post =  user.Posts.SingleOrDefault(t => t.PostId == postTurnOffCommentingDTO.PostId);
            try
            {
                if (post != null && user!=null)
                {
                    post.CommentFlag = false;
                    await _db.SaveChangesAsync();
                }
                return post.CommentFlag;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while turning off the comments ", ex);
            }
        }
    }
}


