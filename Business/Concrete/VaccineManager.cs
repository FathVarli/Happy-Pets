using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Dtos.VaccineDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class VaccineManager : IVaccineService
    {
        IVaccineDal _vaccineDal;

        public VaccineManager(IVaccineDal vaccineDal)
        {
            _vaccineDal = vaccineDal;
        }

        public IDataResult<List<Vaccine>> GetVaccineListByPetType(int petType)
        {
            var vaccineList = _vaccineDal.GetList(v => v.PetTypeId == petType);
            return new SuccessDataResult<List<Vaccine>>(vaccineList);
        }
    }
}
