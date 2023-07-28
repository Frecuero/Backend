using Microsoft.EntityFrameworkCore;
using BackendAPI.Models;
using BackendAPI.Services.Users;
using BackendAPI.DTOs;

namespace BackendAPI.Services.Implementation
{
    public class UsersService : InterfaceUserPhone
    {
        private DbPhoneContext _phoneContext;

        public UsersService(DbPhoneContext dbcontext)
        {
            _phoneContext = dbcontext;
        }
        public async Task<List<PhoneBook>> GetList()
        {
            try
            {
                List<PhoneBook> allPhoneBook = await _phoneContext.PhoneBooks
                     .Include(p => p.ContactType)
                    .ToListAsync();
                return allPhoneBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PhoneBook> Get(int idUser)
        {
            try
            {
                PhoneBook? findUser = new PhoneBook();
                findUser = await _phoneContext.PhoneBooks.Where(x => x.Id == idUser).FirstOrDefaultAsync();
                return findUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PhoneBook> Add(PhoneBook phoneBook)
        {
            try
            {
                _phoneContext.PhoneBooks.Add(phoneBook);
                await _phoneContext.SaveChangesAsync();
                return phoneBook;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Delete a PhoneBook entry
        public async Task<bool> Delete(PhoneBook idUser)
        {
            try
            {
                PhoneBook phoneBook = await _phoneContext.PhoneBooks.FindAsync(idUser.Id);
                if (phoneBook == null)
                {
                    // Handle the case when the entry doesn't exist
                    throw new Exception($"PhoneBook entry with Id {idUser} not found.");
                }

                _phoneContext.PhoneBooks.Remove(phoneBook);
                await _phoneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(PhoneBook phoneBook)
        {
            try
            {
                _phoneContext.PhoneBooks.Update(phoneBook);
                await _phoneContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
 