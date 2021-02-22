using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository MyProperty { get; }
        IMessageRepository MessageRepository { get; }
        ILikesRepository LikesRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}