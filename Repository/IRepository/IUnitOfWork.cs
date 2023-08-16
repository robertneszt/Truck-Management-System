namespace TMS_APP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDriverRepository driver { get; }
        void Save();
    }
}
