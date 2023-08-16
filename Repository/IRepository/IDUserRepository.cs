using TMS_APP.Models;

namespace TMS_APP.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update (User user);
    }
}
