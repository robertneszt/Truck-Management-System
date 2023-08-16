using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Repository
{
    public class UserRepository: Repository<User>, IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /*   public void Save()
           {
               _db.SaveChanges();
           }*/

        public void Update(User obj)
        {
            _db.Update(obj);
        }

    }
}
