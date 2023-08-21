using TMS_APP.Data;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
       
      
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
