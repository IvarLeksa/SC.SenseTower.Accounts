using AutoMapper;
using IdentityModel.Client;
using SC.SenseTower.Accounts.Dto.Identity;

namespace SC.SenseTower.Accounts.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TokenResponse, TokenResponseDto>();
            CreateMap<UserInfoResponseDto, UserInfoDto>();
        }
    }
}
