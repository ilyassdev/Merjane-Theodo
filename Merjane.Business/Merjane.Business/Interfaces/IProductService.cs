using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Merjane.Entities;

namespace Merjane.Business.Interfaces
{
    public interface IProductService
    {
        void NotifyDelay(int leadTime, Product product);
        void HandleSeasonalProduct(Product product);
        void HandleExpiredProduct(Product product);
    }
}
