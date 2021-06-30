using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.DataAccess.Context;
using DataAccess.DataAccess.Entityframework;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPasswordResetDal : EfEntityRepositoryBase<ResetPassword>, IPasswordResetDal
    {
        private readonly PostgresqlContext _dataContext;
        public EfPasswordResetDal(PostgresqlContext dataContext)
            : base(dataContext)
        {
            _dataContext = dataContext;
        }
    }
}
