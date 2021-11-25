using EntityFramework1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkOfUnitsPattern.Data
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SchoolContext _schoolContext;

        public UnitOfWork(SchoolContext schoolContext)
        {
            _schoolContext = schoolContext;
        }

        public void Commit()
        {
            _schoolContext.SaveChanges();
        }

        public void Rollback()
        {
            //throw new NotImplementedException();
        }
    }
}
