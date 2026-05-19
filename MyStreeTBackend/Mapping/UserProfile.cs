using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MyStreeTBackend.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {       
            CreateMap<DTO.UserRegistrationDTO, Models.User>();
        }
    }
}