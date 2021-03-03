using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosManagedController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPhotoService _photoService;
        public PhotosManagedController(IUnitOfWork unitOfWork, IPhotoService photoService)
        {
            _photoService = photoService;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Policy = "Moderator,Admin")]
        [HttpGet("photos")]
        public async Task<ActionResult<IEnumerable<PhotoForApprouvalDTo>>> GetPhotos([FromQuery] PhotosManagedParams photosManagedParams)
        {
            var photos = await this._unitOfWork.PhotoRepository.GetUnaprovedPhotosAsync(photosManagedParams);

            if (photos == null) return NotFound();

            Response.AddPaginationHeader(photos.CurrentPage, photos.PageSize, photos.TotalCount, photos.TotalPages);

            return Ok(photos);
        }

        [Authorize(Policy = "Moderator,Admin")]
        [HttpPut("approuved/photos/{photoId}")]
        public async Task<ActionResult> GetPhoto(int photoId)
        {
            var photo = await this._unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

            if (photo == null) return NotFound();

            if (photo.IsValid) return BadRequest("Photo is already valid.");

            var user = await this._unitOfWork.UserRepository.GetUserByIdAscync(photo.AppUserId);

            if (user == null) return NotFound();

            var PhotoMain = user.Photos.FirstOrDefault(p => p.IsMain);

            if (PhotoMain == null) photo.IsMain = true;

            photo.IsValid = true;

            if (await this._unitOfWork.Complete()) return NoContent();

            return BadRequest("Error in approuved photo section");
        }

        [Authorize(Policy = "Moderator,Admin")]
        [HttpDelete("decline/photo/{photoId}")]
        public async Task<ActionResult>DeclinePhoto(int photoId)
        {
            var photo = await this._unitOfWork.PhotoRepository.GetPhotoByIdAsync(photoId);

            if (photo == null) return NotFound();

            var user = await this._unitOfWork.UserRepository.GetUserByIdAscync(photo.AppUserId);

            if(user == null) return NotFound();

            if(photo.PublicId != null){
                var result = await this._photoService.DeletePhotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);
            }

            this._unitOfWork.PhotoRepository.RemovePhoto(photo);

            if(await this._unitOfWork.Complete()) return Ok();

            return BadRequest("Error in remove photo section");
        }
    }
}