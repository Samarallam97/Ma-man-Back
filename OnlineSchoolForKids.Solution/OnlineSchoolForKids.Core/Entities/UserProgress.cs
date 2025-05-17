using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class UserProgress : BaseEntity
	{
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		public string ModuleId { get; set; }
		public Module Module { get; set; }
		public double CompletionPercentage { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime LastUpdated { get; set; }
		public List<ContentProgress> ContentProgress { get; set; }
	}
}
