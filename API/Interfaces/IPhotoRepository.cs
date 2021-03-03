using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IPhotoRepository
    {   
        Task<PagedList<PhotoForApprouvalDTo>> GetUnaprovedPhotosAsync(PhotosManagedParams photosManagedParams);
        Task<Photo> GetPhotoByIdAsync(int id);
        void RemovePhoto(Photo photo);

    }
}