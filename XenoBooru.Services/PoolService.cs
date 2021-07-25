using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class PoolService
	{
		private readonly IPostRepository _postRepository;
		private readonly IPoolRepository _poolRepository;
		private readonly IMapper _mapper;

		public PoolService(IPostRepository postRepository, IPoolRepository poolRepository, IMapper mapper)
		{
			_postRepository = postRepository;
			_poolRepository = poolRepository;
			_mapper = mapper;
		}

		IEnumerable<Pool> GetByPost(int postId)
		{
			var poolsDb = _poolRepository.GetByPost(postId);
			var pools = _mapper.Map<IEnumerable<Pool>>(poolsDb);
			return pools;
		}
	}
}
