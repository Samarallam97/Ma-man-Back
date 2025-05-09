using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Rating : BaseEntity
	{
		public string ModuleId { get; set; }
		public Module Module { get; set; }
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public int Stars { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
