using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class Module : BaseEntity
	{
		public string Name { get; set; }
		public string NameAr { get; set; }

		public string Description { get; set; }
		public string DescriptionAr { get; set; }

		public string CategoryId { get; set; }
		public Category Category { get; set; }
		public string? CreatedByAdminId { get; set; }
		public ApplicationUser? CreatedByAdmin { get; set; } 
		public double AverageRating { get; set; }

        public string Color { get; set; }
		public string PicutureUrl { get; set; }
		public List<Comment> Comments { get; set; }
		public List<Rating> Ratings { get; set; }
		public List<Content> Contents { get; set; }
	}
}
