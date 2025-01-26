using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merjane.DataAccess.Repositories;
using Merjane.Entities;

namespace Merjane.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {

        IRepository<T> GetRepository<T>() where T : BaseEntity;
        Task SaveChangesAsync();

    }
}

