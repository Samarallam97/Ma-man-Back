using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Notification : BaseEntity
	{
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public string Message { get; set; }
		public string MessageAr { get; set; }
		public bool IsRead { get; set; } = false;
        public string Type { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
    }
}
