using Core.Entities.Concrete;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IVaccineDal : IEntityRepository<Vaccine>
    {
    }
}
