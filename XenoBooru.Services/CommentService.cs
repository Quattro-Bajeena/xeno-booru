using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Entities;
using XenoBooru.Data.Repositories.Interfaces;

namespace XenoBooru.Services
{
	public class CommentService
	{
		private readonly ICommentRepository _repository;
		private readonly IMapper _mapper;

		public CommentService(ICommentRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public IEnumerable<Comment> GetFromPost(int postId)
		{
			var commentsDb = _repository.GetFromPost(postId);
			var comments = _mapper.Map<IEnumerable<Comment>>(commentsDb).OrderByDescending(comment => comment.Date);
			return comments;

		}

		public void Add(Comment comment)
		{
			var commentDb = _mapper.Map<CommentEntity>(comment);
			_repository.Add(commentDb);
		}
	}
}
