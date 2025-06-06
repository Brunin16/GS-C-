using GS_Model;
using GS_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_Service
{
    public class EnergyConsumeService
    {
        private readonly EnergyConsumeRepository _energyConsumeRepository;
        private readonly EquipamentRepository _equipamentRepository;

        public EnergyConsumeService(EnergyConsumeRepository energyConsumeRepository, EquipamentRepository equipamentRepository)
        {
            _energyConsumeRepository = energyConsumeRepository;
            _equipamentRepository = equipamentRepository;
        }

        public void Create(EnergyConsume consume)
        {
            _energyConsumeRepository.Insert(consume);
        }

        public EnergyConsume GetById(long id)
        {
            return _energyConsumeRepository.GetById(id);
        }

        public List<EnergyConsume> GetAll()
        {
            return new List<EnergyConsume>(_energyConsumeRepository.GetAll());
        }

        public void Update(EnergyConsume consume)
        {
            _energyConsumeRepository.Update(consume);
        }

        public void Delete(long id)
        {
            _energyConsumeRepository.Delete(id);
        }

        public double CalculateMonthlyTotal(long personId, int month, int year)
        {
            var allEquipaments = _equipamentRepository.ListAll();
            var equipamentsFromPerson = allEquipaments.Where(e => e.PersonId == personId);

            double total = 0;
            foreach (var equip in equipamentsFromPerson)
            {
                var consumes = _energyConsumeRepository.GetByEquipamentId(equip.Id)
                    .Where(ec => ec.Month == month && ec.Year == year);

                foreach (var ec in consumes)
                {
                    total += (ec.PricePerHour * equip.HourUsedPerDay * 30) + ec.FixPrice;
                }
            }

            return total;
        }
    }
}
