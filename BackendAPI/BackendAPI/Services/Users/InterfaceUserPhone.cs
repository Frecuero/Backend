using BackendAPI.Models;
using BackendAPI.Services.IbaseServices;

namespace BackendAPI.Services.Users
{
    public interface InterfaceUserPhone: IBaseService<PhoneBook>
    {
        Task<List<PhoneBook>> GetList();
        Task<PhoneBook> Get(int idUser);
        Task<PhoneBook> Add(PhoneBook user);
        Task<bool> Update(PhoneBook user);
        Task<bool> Delete(PhoneBook idUser);
    }
}
