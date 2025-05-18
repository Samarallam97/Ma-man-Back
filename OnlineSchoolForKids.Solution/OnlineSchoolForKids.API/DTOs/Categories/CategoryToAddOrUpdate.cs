namespace OnlineSchoolForKids.API.DTOs.Categories
{
	public class CategoryToAddOrUpdate  : CategoryDTO
	{
		public string Name { get; set; }
		public string NameAR { get; set; }
		public string Description { get; set; }
		public string DescriptionAR { get; set; }
	}
}
