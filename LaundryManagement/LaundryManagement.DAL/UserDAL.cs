using LaundryManagement.Domain.Entities;
using LaundryManagement.Services;

namespace LaundryManagement.DAL
{
    public class UserDAL : ICrud<User>
    {
        private IList<User> dbContext;

        public UserDAL()
        {
            dbContext = new List<User>();
            dbContext.Add(new User()
            {
                Email = "jcavallo11@gmail.com",
                Id = 1,
                Name = "Julian",
                Password = Encryptor.Hash("1234"),
            });
        }

        public void Delete(User entity)
        {
            this.dbContext.Remove(entity);
        }

        public IList<User> GetAll()
        {
            return this.dbContext;
        }

        public User GetById(int id)
        {
            return this.dbContext.SingleOrDefault(x => x.Id == id);
        }

        public void Save(User entity)
        {
            if(dbContext.Any(x => x.Id == entity.Id)) 
            {
                this.dbContext.Remove(entity);
                this.dbContext.Add(entity);
            }
            else
                this.dbContext.Add(entity);
        }
    }
}