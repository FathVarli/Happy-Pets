using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.DataAccess.Context;
using DataAccess.DataAccess.Entityframework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPetVaccineDal : EfEntityRepositoryBase<PetVaccine>, IPetVaccineDal
    {
        private readonly PostgresqlContext _dataContext;
        public EfPetVaccineDal(PostgresqlContext context, PostgresqlContext dataContext) : base(context)
        {
            _dataContext = dataContext;
        }
    }
}
