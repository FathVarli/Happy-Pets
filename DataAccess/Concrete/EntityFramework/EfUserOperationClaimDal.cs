using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.DataAccess.Context;
using DataAccess.DataAccess.Entityframework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim>, IUserOperationClaimDal
    {
        private readonly PostgresqlContext _dataContext;

        public EfUserOperationClaimDal(PostgresqlContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
