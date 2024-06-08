using AutoMapper;
using Common.DTO;
using Service.Database.Models;

namespace Service.Database.Mapper
{
    public static class MappingHelper
    {
        private static IMapper _mapper;

        static MappingHelper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VozacDTO, Vozac>();
                cfg.CreateMap<LinijaDTO, Linija>();
                cfg.CreateMap<Vozac, VozacDTO>();
                cfg.CreateMap<Linija, LinijaDTO>();
            });

            _mapper = config.CreateMapper();
        }

        public static Vozac MapVozacDTOToVozac(VozacDTO dto)
        {
            return _mapper.Map<Vozac>(dto);
        }

        public static Linija MapLinijaDTOToLinija(LinijaDTO dto)
        {
            return _mapper.Map<Linija>(dto);
        }

        public static VozacDTO MapVozacToVozacDTO(Vozac vozac)
        {
            return _mapper.Map<Vozac, VozacDTO>(vozac);
        }

        public static LinijaDTO MapLinijaToLinijaDTO(Linija linija)
        {
            return _mapper.Map<Linija, LinijaDTO>(linija);
        }
    }
}
