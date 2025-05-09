using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Entities
{
	public class ContentProgress : BaseEntity
	{
		public string UserProgressId { get; set; }
		public UserProgress UserProgress { get; set; }
		public string ContentId { get; set; }
		public Content Content { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime CompletedAt { get; set; }
	} 
}
