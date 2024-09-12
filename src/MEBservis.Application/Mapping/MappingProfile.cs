using AutoMapper;
using MEBservis.Application.DTOs;

namespace MEBservis.Application.Mapping
{
    /// <summary>
    /// AutoMapper profilini yapılandıran sınıf.
    /// Bu sınıf, model ve DTO'lar arasındaki eşlemeleri tanımlar.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// MappingProfile sınıfının yapılandırıcısı.
        /// Burada, model ve DTO'lar arasındaki eşlemeler tanımlanır.
        /// </summary>
        public MappingProfile()
        {
            // User ve UserDto arasında eşleme
            CreateMap<User, UserDto>();

            // UserDto ve User arasında eşleme
            CreateMap<UserDto, User>();

            // UserCreateDto ve User arasında eşleme
            // Oluşturulma tarihini şu anki zamana ayarlıyoruz
            CreateMap<UserCreateDto, User>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now));

            // UserUpdateDto ve User arasında eşleme

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()) // UserId'yi manuel olarak ayarlayacağız.
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


            // UserRoleUpdateDto ve User arasında eşleme
            // Yeni Rol ID'si ile günceller
            CreateMap<UserRoleUpdateDto, User>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.NewRoleId));

            // Entity -> DTO dönüşümleri
            CreateMap<TrackingSession, TrackingSessionDto>();

            CreateMap<TrackingData, TrackingDataDto>();

            // User'dan UserWithPasswordDto'ya eşleme
            CreateMap<User, UserWithPasswordDto>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash));

            // UserWithPasswordDto'dan User'a eşleme (Gerekirse ekleyin)
            CreateMap<UserWithPasswordDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.PasswordHash));
        }
    }
}
