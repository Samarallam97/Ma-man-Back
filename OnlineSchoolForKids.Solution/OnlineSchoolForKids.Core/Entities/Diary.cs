using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Diary : BaseEntity
	{
        public string Title { get; set; }
        public string Content { get; set; }
		public DateOnly CreationDate { get; set; } 
		public DateOnly LastUpdateDate { get; set; }
		public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
