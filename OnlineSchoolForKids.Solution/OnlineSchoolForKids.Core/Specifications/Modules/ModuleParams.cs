using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Specifications.Modules
{
	public class ModuleParams
	{
		public string Language { get; set; } = "En";
        public string? CategoryId { get; set; }

        private string? search;
		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower(); }
		}


		private const int MAX_PAGE_SIZE = 8;

		private int pageSize = MAX_PAGE_SIZE;
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value; }
		}
		public int PageIndex { get; set; } = 1;
	}
}
