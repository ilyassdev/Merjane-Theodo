using Merjane.Business.Interfaces;
using Merjane.DataAccess.UnitOfWork;
using Merjane.Entities;

namespace Merjane.Business.Services
{
    public class ProductService :IProductService
    {
        private readonly INotificationService _nService;
        private readonly IUnitOfWork _Unwork;

        public ProductService(INotificationService _nService, IUnitOfWork _Unwork)
        {
            this._nService = _nService;
            this._Unwork = _Unwork;
        }

        public void NotifyDelay(int leadTime, Product product)
        {
            product.LeadTime = leadTime;
            _Unwork.SaveChangesAsync();
            _nService.SendDelayNotification(leadTime, product.Name);
        }

        public void HandleSeasonalProduct(Product product)
        {
            if (DateTime.Now.AddDays(product.LeadTime) > product.SeasonEndDate)
            {
                _nService.SendOutOfStockNotification(product.Name);
                product.Available = 0;
                _Unwork.SaveChangesAsync();
            }
            else if (product.SeasonStartDate > DateTime.Now)
            {
                _nService.SendOutOfStockNotification(product.Name);
                _Unwork.SaveChangesAsync();
            }
            else
            {
                NotifyDelay(product.LeadTime, product);
            }
        }

        public void HandleExpiredProduct(Product product)
        {
            if (product.Available > 0 && product.ExpiryDate > DateTime.Now)
            {
                product.Available -= 1;
                _Unwork.SaveChangesAsync();
            }
            else
            {
                _nService.SendExpirationNotification(product.Name, (DateTime)product.ExpiryDate);
                product.Available = 0;
                _Unwork.SaveChangesAsync();
            }
        }
    }
}
