using OnlineSchoolForKids.API.DTOs.Auth;
using OnlineSchoolForKids.API.DTOs.Categories;
using OnlineSchoolForKids.API.DTOs.Comments;
using OnlineSchoolForKids.API.DTOs.Contents;
using OnlineSchoolForKids.API.DTOs.Diaries;
using OnlineSchoolForKids.API.DTOs.Modules;
using OnlineSchoolForKids.API.DTOs.TODOs;

namespace OnlineSchoolForKids.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<RegisterDto, ApplicationUser>();
		CreateMap<ApplicationUser, UserResponseDTO>();
		CreateMap<UserToUpdateDto, ApplicationUser>();


		CreateMap<CategoryToAddOrUpdate, Category>();
		CreateMap<Category, CategoryDTOEn>();
		CreateMap<Category, CategoryDTOAr>();

		CreateMap<ModuleToAddOrUpdate, Module>();
		CreateMap<Module, ModuleDTOEn>();
		CreateMap<Module, ModuleDTOAr>();

		CreateMap<ContentToAddOrUpdate, Content>();
		CreateMap<Content, ContentDTOEn>();
		CreateMap<Content, ContentDTOAr>();

		CreateMap<Diary , DiaryDTO>().ReverseMap();
		CreateMap<TODO, ToDoDTO>().ReverseMap();
		CreateMap<Comment, CommentDTO>().ReverseMap();

	}
}
