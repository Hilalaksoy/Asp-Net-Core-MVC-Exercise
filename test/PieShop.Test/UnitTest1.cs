using BethanysPieShop.Controllers;
using BethanysPieShop.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace PieShop.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Pie_List_Check_Current_Category()
        {
            // Arrange

            var pieRepo = new Mock<IPieRepository>();
            pieRepo.Setup(c => c.AllPies).Returns(It.IsAny<List<Pie>>());
            var categoryRepo = new Mock<ICategoryRepository>();

            var controller = new PieController(pieRepo.Object, categoryRepo.Object);
            // Act

            var result = controller.List();

            // Assert
            var viewResult = Assert.IsType<PiesListViewModel>(result.Model);
            var model = Assert.IsAssignableFrom<PiesListViewModel>(
                viewResult);

            //var model= result.Model as PiesListViewModel;

            Assert.Equal("Cheese Cakes", model.CurrentCategory);

        }


        [Fact]
        public void Pie_List_Check_Pies()
        {
            // Arrange

            var pie = new List<Pie>
            {
                new Pie {PieId = 1, Name="Strawberry Pie", Price=15.95M, ShortDescription="Lorem Ipsum"}
            };

            var pieRepo = new Mock<IPieRepository>();
            pieRepo.Setup(c => c.AllPies).Returns(pie);
            var categoryRepo = new Mock<ICategoryRepository>();

            var controller = new PieController(pieRepo.Object, categoryRepo.Object);
            // Act

            var result = controller.List();

            // Assert
            //var viewResult = Assert.IsType<PiesListViewModel>(result.Model);
            var model = Assert.IsAssignableFrom<PiesListViewModel>(
                result.Model);

            //var model= result.Model as PiesListViewModel;

            var pieFromMock = model.Pies.FirstOrDefault();

            Assert.Single(model.Pies);
            Assert.Equal("Strawberry Pie", pieFromMock?.Name);

        }


        [Fact]
        public void Get_Pie_Details()
        {  
            // Arrange

            var pie = new Pie
            {
                PieId = 1,
                Name = "Strawberry Pie",
                Price = 15.95M,
                ShortDescription = "Lorem Ipsum"
            };

            var pieRepo = new Mock<IPieRepository>();
            pieRepo.Setup(c => c.GetPieById(It.IsAny<int>())).Returns(pie);
            var categoryRepo = new Mock<ICategoryRepository>();

            var controller = new PieController(pieRepo.Object, categoryRepo.Object);
            // Act

            var result = controller.Details(It.IsAny<int>());

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Pie>(
                viewResult.Model);

            Assert.Equal("Strawberry Pie", model.Name);

        }
    }
}
