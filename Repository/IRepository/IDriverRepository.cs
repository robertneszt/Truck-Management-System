using TMS_APP.Models;

namespace TMS_APP.Repository.IRepository
{
    public interface IDriverRepository : IRepository<User>
    {
        void Update (User user);
    }
}
