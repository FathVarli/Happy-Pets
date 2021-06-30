using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entity.Dtos.VaccineDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IVaccineService
    {
        IDataResult<List<Vaccine>> GetVaccineListByPetType(int petType);
    }
}
