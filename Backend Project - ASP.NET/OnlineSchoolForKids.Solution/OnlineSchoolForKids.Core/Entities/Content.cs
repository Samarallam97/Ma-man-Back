using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class Content : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
	public string URL { get; set; }

	public int? FormatId { get; set; }
	public int? CategoryId { get; set; }
	public int? AdminId { get; set; }
	public DateTime CreatedAt { get; set; }
	public int AverageRate { get; set; }

}
