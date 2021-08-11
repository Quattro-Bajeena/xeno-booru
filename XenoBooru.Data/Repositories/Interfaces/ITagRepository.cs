﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenoBooru.Core.Models;
using XenoBooru.Data.Entities;

namespace XenoBooru.Data.Repositories.Interfaces
{
	public interface ITagRepository
	{
		TagEntity Get(string name);
		IEnumerable<TagEntity> GetAll();
		IEnumerable<TagEntity> GetFromPost(int postId);
		IEnumerable<TagEntity> GetFiltered(string name, string type, string order);
		void Add(TagEntity tag);
		void Remove(int id);
		void Update(TagEntity tag);
		int GetTagPostCount(int tagId);
		ICollection<TagEntity> GetFromStr(string tagsStr);
	}
}
