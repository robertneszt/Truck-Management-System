namespace TMS_APP.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDriverRepository driver { get; }
        IUserRepository user { get; }
        void Save();
    }
}
