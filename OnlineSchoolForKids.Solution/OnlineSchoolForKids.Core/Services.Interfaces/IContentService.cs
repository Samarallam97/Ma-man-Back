using OnlineSchoolForKids.Core.Specifications.Contents;

namespace OnlineSchoolForKids.Core.Services.Interfaces
{
	public interface IContentService
	{
		Task<bool> AddAsync(Content content);
		Task<bool> UpdateAsync(Content content);
		Task<bool> DeleteAsync(Content content);

		Task<Content?> GetContentByIdAsync(string Id);
		Task<int> GetCountAsync(ContentParams contentParams);
		Task<IReadOnlyList<Content>> GetAllContentsAsync(ContentParams contentParams);
	}
}
