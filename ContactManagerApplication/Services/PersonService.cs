using AutoMapper;
using ContactManager.DataAccess;
using ContactManager.DataAccess.Models;
using ContactManagerApplication.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ContactManagerApplication.Services
{
    public class PersonService : IPersonService
    {
        private readonly ContactManagerDbContext dbContext;
        private readonly IMapper mapper;
        public PersonService(ContactManagerDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }
        public async Task<Person> Create(PersonCreate personCreate)
        {
            var person = mapper.Map<Person>(personCreate);
            await dbContext.AddAsync(person);
            await dbContext.SaveChangesAsync();
            return person;
        }

        public async Task Delete(Guid id)
        {
            var person = await GetById(id);

            if (person == null)
                throw new ArgumentException(nameof(id));

            dbContext.Remove(person);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Person>> GetAll()
        {
            return await dbContext.Persons.ToListAsync();
        }

        public async Task<Person?> GetById(Guid id)
        {
            return await dbContext.Persons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Person> Update(Person personUpdate)
        {
            var personToUpdate = await GetById(personUpdate.Id);

            if (personToUpdate == null)
                throw new ArgumentException(nameof(personUpdate));

            personToUpdate.Name = personUpdate.Name;
            personToUpdate.DateOfBirth = personUpdate.DateOfBirth;
            personToUpdate.Married = personUpdate.Married;
            personToUpdate.PhoneNumber = personUpdate.PhoneNumber;
            personToUpdate.Salary = personUpdate.Salary;

            await dbContext.SaveChangesAsync();
            return personToUpdate;

        }
    }
}
