using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Diary : BaseEntity
	{
		public string Content { get; set; }
		public DateTime CreationDate { get; set; } = DateTime.UtcNow;
		public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
