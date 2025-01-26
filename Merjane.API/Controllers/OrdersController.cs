using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Merjane.DataAccess.UnitOfWork;
using Merjane.Business;
using Merjane.Business.Services;
using Merjane.Entities;
using Merjane.Dtos.Services;
using Microsoft.IdentityModel.Tokens;
using Merjane.Business.Interfaces;
using static Merjane.Dtos.Services.ProcessOrderResponseDtos;

namespace Merjane.API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IProductService _nService;
        private readonly IUnitOfWork _uoWork;


        public OrdersController(IProductService nService, IUnitOfWork uoWork)
        {
            _nService = nService;
            _uoWork = uoWork;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        public ActionResult<Dtos.Services.ProcessOrderResponseDtos> ProcessOrder(long orderId)
        {
            

            Entities.Order? order = _uoWork.GetRepository<Order>().GetQuery()
                .Include(o => o.Products)
                .SingleOrDefault(o => o.Id == orderId);
            
            List<long> ids = new() { orderId };
            ICollection<Entities.Product>? products = order?.Products;

            foreach (Entities.Product product in products)
            {
                if (product.Type == "NORMAL")
                {
                    if (product.Available > 0)
                    {
                        product.Available -= 1;
                        _uoWork.GetRepository<Product>().UpdateEntityState(product, EntityState.Modified);
                        _uoWork.SaveChangesAsync();
                    }
                    else
                    {
                        int leadTime = product.LeadTime;
                        if (leadTime > 0)
                        {
                            _nService.NotifyDelay(leadTime, product);
                        }
                    }
                }
                else if (product.Type == "SEASONAL")
                {
                    if (DateTime.Now.Date > product.SeasonStartDate && DateTime.Now.Date < product.SeasonEndDate && product.Available > 0)
                    {
                        product.Available -= 1;
                        _ = _uoWork.SaveChangesAsync();
                    }
                    else
                    {
                        _nService.HandleSeasonalProduct(product);
                    }
                }
                else if (product.Type == "EXPIRABLE")
                {
                    if (product.Available > 0 && product.ExpiryDate > DateTime.Now.Date)
                    {
                        product.Available -= 1;
                        _ = _uoWork.SaveChangesAsync();
                    }
                    else
                    {
                        _nService.HandleExpiredProduct(product);
                    }
                }
            }

            return new Dtos.Services.ProcessOrderResponseDtos(order.Id);
            ;
        }

    }
}