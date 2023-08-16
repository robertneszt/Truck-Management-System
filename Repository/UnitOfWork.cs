using TMS_APP.Data;
using TMS_APP.Repository.IRepository;

namespace TMS_APP.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private ApplicationDbContext _db;
       
        public IDriverRepository driver { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            driver = new DriverRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
