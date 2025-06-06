using GS_Model;
using GS_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Service
{
    public class PersonService
    {
        private readonly PersonRepository _personRepository;

        public PersonService(PersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public PersonService()
        {
        }

        public void Create(Person person)
        {
            _personRepository.Add(person);
        }

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public List<Person> GetAll()
        {
            return new List<Person>(_personRepository.ListAll());
        }

        public void Update(Person person)
        {
            _personRepository.Update(person);
        }

        public void Delete(int id)
        {
            _personRepository.Delete(id);
        }
    }
}
