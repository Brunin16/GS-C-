using GS_Model;
using GS_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Service
{
    public class EquipamentService
    {
        private readonly EquipamentRepository _equipamentRepository;

        public EquipamentService(EquipamentRepository equipamentRepository, PersonRepository personRepository)
        {
            _equipamentRepository = equipamentRepository;
        }

        public EquipamentService(EquipamentRepository equipamentRepository)
        {
            _equipamentRepository = equipamentRepository;
        }

        public void Create(Equipament equipament)
        {
            _equipamentRepository.Create(equipament);
        }

        public Equipament GetById(long id)
        {
            return _equipamentRepository.GetById(id);
        }

        public List<Equipament> GetAll()
        {
            return new List<Equipament>(_equipamentRepository.ListAll());
        }

        public void Update(Equipament equipament)
        {
            _equipamentRepository.Update(equipament);
        }

        public void Delete(long id)
        {
            _equipamentRepository.Delete(id);
        }
    }
}
