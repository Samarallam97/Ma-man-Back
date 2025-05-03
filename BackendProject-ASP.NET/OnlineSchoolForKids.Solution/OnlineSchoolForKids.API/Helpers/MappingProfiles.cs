namespace OnlineSchoolForKids.API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentDTO, Content>();
    }
}
