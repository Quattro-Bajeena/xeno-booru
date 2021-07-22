using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.DTO;
using XenoBooru.Data.Entities;

namespace XenoBooru.Services
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<PostEntity, Post>();
			CreateMap<Post, PostEntity>();
		}
	}
}
