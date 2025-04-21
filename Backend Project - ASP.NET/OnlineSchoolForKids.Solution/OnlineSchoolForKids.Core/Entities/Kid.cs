using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities;

public class Kid : BaseEntity
{
    public DateTime DateOfBirth { get; set; }
    public int TimeLimitWithMinutes { get; set; }
    public int Points { get; set; }

    public int ParentId { get; set; }
    public int? AgeGroupId { get; set; }

}
