using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class HiddenModule : BaseEntity
	{
		public string UserId { get; set; } // Parent or Adult
		public ApplicationUser User { get; set; }
		public string ModuleId { get; set; }
		public Module Module { get; set; }
	}
}
