using AutoMapper;
using ReviewApplication.Models;
using ReviewDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApplication.MappingProfile
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {
            CreateMap<ReviewCreateRequest, Review>();
            CreateMap<ReviewCreateDto, Review>();
            
        }
    }
}
