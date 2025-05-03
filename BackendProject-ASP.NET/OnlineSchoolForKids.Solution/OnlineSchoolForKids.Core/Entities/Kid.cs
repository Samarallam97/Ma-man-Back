using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSchoolForKids.Core.Entities;

public class Kid : BaseEntity
{
    public DateTime DateOfBirth { get; set; }
    public int TimeLimitWithMinutes { get; set; }
    public int Points { get; set; } 
    public int ParentId { get; set; }
 
    /// /////////////////
    [ForeignKey("ParentId")]
    public virtual Parent Parent { get; set; }
    /// ////////////

    public int? AgeGroupId { get; set; }

}
