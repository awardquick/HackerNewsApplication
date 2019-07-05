using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using HackerNewsAppCore;
using NewHackerNewsAppInfrastructure.Models;

namespace NewHackerNewsAppInfrastructure.Mappers
{
    public class ItemMappingProfile : Profile
    {
        public ItemMappingProfile()
        {
            CreateMap<Item, Article>();
        }
       
    }
}
