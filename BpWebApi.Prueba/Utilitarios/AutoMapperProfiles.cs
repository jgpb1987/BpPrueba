using AutoMapper;
using BpNegocio.Prueba.Dto.Financiero;
using BpNegocio.Prueba.Entidades.Financiero;

namespace BpWebApi.Prueba.Utilitarios
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Movimiento, DtoResultMovimientoReporte>();
        }
    }
}
