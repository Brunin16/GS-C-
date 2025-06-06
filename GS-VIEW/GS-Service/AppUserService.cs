using GS_Model;
using GS_Model.Model;
using GS_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Service
{
    public class AppUserService
    {
        private readonly AppUserRepository _userRepository;
        private readonly PersonRepository _personRepository;

        public AppUserService(AppUserRepository userRepository, PersonRepository personRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
        }

        public AppUserService()
        {
        }

        public bool Login(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user != null && user.Password == password)
                return true;
            return false;
        }
        public bool testarUsuario(string usuario, string senha)
        {
            return _userRepository.validarUsuario(usuario, senha);
        }
        public AppUser CreateUser(string username, string password, Person person)
        {
            var user = new AppUser
            {
                Username = username,
                Password = password,
                PersonId = null
            };  

            _userRepository.Insert(user);
            return user;
        }
        public AppUser GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public List<AppUser> GetAll()
        {
            return _userRepository.GetAll();
        }

        public AppUser GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}
