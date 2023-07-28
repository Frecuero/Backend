using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;
using BackendAPI.Services.TypeContacts;
namespace BackendAPI.Services.Implementation
{
    public class TypeContactService: InterfaceTypeContacts
    {
        private DbPhoneContext _typeContact;

        public TypeContactService(DbPhoneContext dbcontext)
        {
            _typeContact = dbcontext;
        }

        public Task<TypeContact> Add(TypeContact entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(TypeContact entity)
        {
            throw new NotImplementedException();
        }

        public Task<TypeContact> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TypeContact>> GetList() {
            try
            {
                List<TypeContact> allTypeContacts = await _typeContact.TypeContacts.ToListAsync();
                return allTypeContacts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<bool> Update(TypeContact entity)
        {
            throw new NotImplementedException();
        }
    }
}
