using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class AgeGroup : BaseEntity
{
	public string Name { get; set; } // 3-5, 6-8, 8-10, 10-14, 14-18, parent, admin
	public string NameAr { get; set; }
	public List<Content> Contents { get; set; }
	//public List<ApplicationUser> Users { get; set; }
}
