using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.DataAccess.Context;
using DataAccess.DataAccess.Entityframework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPetTypeDal : EfEntityRepositoryBase<PetType>, IPetTypeDal
    {
        private readonly PostgresqlContext _dataContext;
        public EfPetTypeDal(PostgresqlContext context, PostgresqlContext dataContext) : base(context)
        {
            _dataContext = dataContext;
        }
    }
}
