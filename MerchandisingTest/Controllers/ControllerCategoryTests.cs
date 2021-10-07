using Merchandising.Controllers;
using Merchandising.Entities;
using Merchandising.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Merchandising.Repositories.Contracts;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MerchandisingTest.Controllers
{
	public class ControllerCategoryTests
	{
        [Fact]
        public void Get_SingleProdut_Success()
        {
            Product mockProduct = new Product();
            mockProduct.Id = 1;
            mockProduct.Title = "Test";
            mockProduct.Description = "Description";
            mockProduct.Stock = 10;
            mockProduct.Category = null;
            mockProduct.IsActive = false;
            var mockRepository = new Mock<IRepository<Product>>();
            mockRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Product, bool>>>(), It.IsAny<Func<IQueryable<Product>, IOrderedQueryable<Product>>>(), It.IsAny<string>()))
                .Returns(new List<Product>() { mockProduct });
            var mockService = new ServiceProduct(mockRepository.Object, null);
            var controller = new ControllerProducts(mockService);

            var result = controller.Get(1, "Test", null, null);

            ContentResult negResult = Assert.IsType<ContentResult>(result);
            Assert.Equal((int)HttpStatusCode.OK, negResult.StatusCode);
            Assert.Equal("application/json", negResult.ContentType);
            Assert.Equal("{\"Id\":1,\"Title\":\"Test\",\"Description\":\"Description\",\"Stock\":10,\"Category\":null,\"IsActive\":false}", negResult.Content);
        }
    }
}
