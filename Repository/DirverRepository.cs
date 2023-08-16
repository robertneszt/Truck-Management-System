using TMS_APP.Data;
using TMS_APP.Models;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Repository
{
    public class DriverRepository: Repository<Driver>, IDriverRepository
    {
        private ApplicationDbContext _db;
        public DriverRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /*   public void Save()
           {
               _db.SaveChanges();
           }*/

        public void Update(Driver obj)
        {
            _db.Update(obj);
        }

    }
}
