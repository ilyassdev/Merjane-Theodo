
using Merjane.Business.Interfaces;
using Merjane.Business.Services;
using Merjane.DataAccess.Repositories;
using Merjane.DataAccess.UnitOfWork;
using Merjane.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Merjane.API.Testing.Services
{
    public class MyUnitTests
    {

        private readonly Mock<INotificationService> _mockNotificationService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IRepository<Product>> _mockProductRepository; 

        //private readonly Mock<DbSet<Product>> _mockDbSet;
        private readonly IProductService _productService;

        public MyUnitTests()
        {
            _mockNotificationService = new Mock<INotificationService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockProductRepository = new Mock<IRepository<Product>>();

            // Setup the mock repository to return an empty list of products
            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Setup the unit of work to return the mock repository
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Product>()).Returns(_mockProductRepository.Object);

            // Instantiate the ProductService with the mocked dependencies
            _productService = new ProductService(_mockNotificationService.Object, _mockUnitOfWork.Object);
        }

        [Fact]
        public void Test()
        {
            // GIVEN
            Product product = new()
            {
                LeadTime = 15,
                Available = 0,
                Type = "NORMAL",
                Name = "RJ45 Cable"
            };

            // WHEN
            _productService.NotifyDelay(product.LeadTime, product);

            // THEN
            Assert.Equal(0, product.Available);
            Assert.Equal(15, product.LeadTime);
            _mockUnitOfWork.Verify(ctx => ctx.SaveChangesAsync(), Times.Once());
            _mockNotificationService.Verify(service => service.SendDelayNotification(product.LeadTime, product.Name), Times.Once());
        }
    }
}
