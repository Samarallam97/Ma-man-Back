using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class TODO : BaseEntity
	{
		public string Content { get; set; }
		public DateOnly CreationDate { get; set; }
		public DateOnly LastUpdateDate { get; set; }
		public bool Status { get; set; }
		public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
