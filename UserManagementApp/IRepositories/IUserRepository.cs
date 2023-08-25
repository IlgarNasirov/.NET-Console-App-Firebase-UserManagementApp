using UserManagementApp.Models;

namespace UserManagementApp.IRepositories
{
    public interface IUserRepository
    {
        public Task<bool> LoadUsers();
        public Task AddUser(User user);
        public void AllUsers();
        public Task UpdateUser(int index, User user);
        public Task DeleteUser(int index);
    }
}
