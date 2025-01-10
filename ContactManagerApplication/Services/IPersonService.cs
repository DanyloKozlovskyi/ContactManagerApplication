using ContactManager.DataAccess.Models;
using ContactManagerApplication.Dtos;

namespace ContactManagerApplication.Services
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAll();
        Task<Person?> GetById(Guid id);
        Task<Person> Update(Person personUpdate);
        Task Delete(Guid id);
        Task<Person> Create(PersonCreate personCreate);
        Task AddMany(IEnumerable<Person> persons);
    }
}
