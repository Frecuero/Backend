using BackendAPI.Models;
using BackendAPI.Services.IbaseServices;

namespace BackendAPI.Services.TypeContacts
{
    public interface InterfaceTypeContacts : IBaseService<TypeContact>
    {
        Task<List<TypeContact>> GetList();
        // 1:34:42
    }
}
