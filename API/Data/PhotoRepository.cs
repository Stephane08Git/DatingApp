using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PhotoRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Photo> GetPhotoByIdAsync(int id)
        {
            return await this._context.Photos.SingleOrDefaultAsync(photo => photo.Id == id);
        }

        public async Task<PagedList<PhotoForApprouvalDTo>> GetUnaprovedPhotosAsync(PhotosManagedParams photosParam)
        {
            var query = this._context.Photos
                .AsQueryable()
                .IgnoreQueryFilters()
                .Where(photo => !photo.IsValid);

            return await PagedList<PhotoForApprouvalDTo>.CreateAsync(
                    query.ProjectTo<PhotoForApprouvalDTo>(this._mapper.ConfigurationProvider).AsNoTracking(),
                    photosParam.PageNumber,
                    photosParam.PageSize
            );
        }

        public void RemovePhoto(Photo photo)
        {
            this._context.Photos.Remove(photo);
        }
    }
}