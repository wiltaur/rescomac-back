using rescomac_back.repository.Dto;

namespace rescomac_back.business.Interface
{
    public interface IPropietarioService
    {
        Task<bool> AddPropiedad(PropiedadRequest propiedad);
        Task<bool> DeletePropiedad(int id);
        Task<List<PropiedadModRequest>> GetAllPropiedad();
        Task<bool> UpdatePropiedad(PropiedadModRequest propiedad);
    }
}