﻿namespace OnlineSchoolForKids.API.DTOs.Modules
{
	public class ModuleToAddOrUpdate : ModuleDTO
	{
        public string Name { get; set; }
		public string NameAr { get; set; }

		public string Description { get; set; }
		public string DescriptionAr { get; set; }
	}
}
