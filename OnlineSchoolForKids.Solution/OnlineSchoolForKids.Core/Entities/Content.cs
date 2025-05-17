using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Content : BaseEntity
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string Type { get; set; } // video, book, audio
		public string ContentUrl { get; set; }
		public string ModuleId { get; set; }
		public Module Module { get; set; }
		public string? CreatedByAdminId { get; set; } 
		public ApplicationUser? CreatedByAdmin { get; set; } 
		public List<AgeGroup> AgeGroups { get; set; }
	}
}
