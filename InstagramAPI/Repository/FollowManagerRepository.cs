using InstagramAPI.Data;
using InstagramAPI.Models;
using InstagramAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InstagramAPI.Repository
{
    public class FollowManagerRepository : IFollowManagerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _user;

        public FollowManagerRepository(ApplicationDbContext db, UserManager<User> user)
        {
            _db = db;
            _user = user;
        }

        public async Task<bool> FollowUser(int targetId, int userId)
        {
            try
            {
                var target = await _user.Users.SingleOrDefaultAsync(t => t.Id == targetId);
                var followExist = await _db.FollowManagers.SingleOrDefaultAsync(t => t.UserId == userId && t.TargetId == targetId);

                if (target != null && followExist == null)
                {
                    FollowManager follow = new()
                    {
                        UserId = userId,
                        TargetId = targetId,

                    };
                    await _db.FollowManagers.AddAsync(follow);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while following", ex);
            }

        }

        public async Task<bool> RemoveFollower(int targetId, int userId)
        {
            try
            {
                var target = await _user.Users.SingleOrDefaultAsync(t => t.Id == targetId);
                var followExist = await _db.FollowManagers.SingleOrDefaultAsync(t => t.UserId == userId && t.TargetId == targetId);

                if (target != null && followExist != null)
                {
                    _db.FollowManagers.Remove(followExist);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred while removing follower", ex);
            }
        }

        public async Task<bool> UnFollow(int targetId, int userId)
        {
            try
            {
                var target = await _user.Users.SingleOrDefaultAsync(t => t.Id == targetId);
                var followExist = await _db.FollowManagers.SingleOrDefaultAsync(t => t.UserId == userId && t.TargetId == targetId);

                if (target != null && followExist != null)
                {
                    _db.FollowManagers.Remove(followExist);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw new Exception("Error occurred", ex);
            }
        }
    }
}
