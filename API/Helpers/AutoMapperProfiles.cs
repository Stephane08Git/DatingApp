using System;
using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDTo>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => 
                    src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));

            CreateMap<Photo, PhotoDto>();
            CreateMap<Photo, PhotoForApprouvalDTo>()
                .ForMember(p => p.Username, opt =>
                    opt.MapFrom(src => src.AppUser.UserName));
                    
            CreateMap<MemberUpdateDTo, AppUser>();
            CreateMap<RegisterDTo, AppUser>();
            CreateMap<Message, MessageDTo>()
                .ForMember(dest => dest.SenderPhotoUrl, opt => 
                    opt.MapFrom(src => src.Sender.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RecipientPhotoUrl, opt => 
                    opt.MapFrom(src => src.Recipient.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}