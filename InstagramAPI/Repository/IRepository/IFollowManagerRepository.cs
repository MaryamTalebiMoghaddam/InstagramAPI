

namespace InstagramAPI.Repository.IRepository
{
    public interface IFollowManagerRepository
    {
        Task<bool> FollowUser(int targetId, int userId);
        Task<bool> UnFollow(int targetId, int userId);
        Task<bool> RemoveFollower(int targetId, int userId);
    }
}
