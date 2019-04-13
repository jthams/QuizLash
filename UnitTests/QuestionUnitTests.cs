using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Moq;
using Domain.Entities;
using WebUI.Controllers;
using Domain.Abstract;
using WebUI.ViewModels;
using WebUI.Services;


namespace UnitTests
{
    [TestClass]
    public class QuestionUnitTests
    {
        
        [TestMethod]
        public void Question_Index()
        {
            // Arrange
            QuestionsController controller = new QuestionsController(null,null);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Question_ONGET_Create()
        {
            // Arrange
            QuestionsController controller = new QuestionsController(null, null);
            QuestionViewModel mock = new QuestionViewModel{};

            // Act
            var result = controller.Create() as ViewResult;

            // Assert 
            Assert.AreEqual("Create",result.ViewName);
        }

        [TestMethod]
        public void Question_ONPOST_Create()
        {
            // Arrange
            QuestionsController controller = new QuestionsController(null, null);
            QuestionViewModel mock = new QuestionViewModel { };

            // Act
            var result = controller.Create(mock);

            // Assert 
            Assert.AreEqual("Index", result.Result);
        }
        [TestMethod]
        public void Question_Details()
        {
            // Arrange
            QuestionsController controller = new QuestionsController(null, null);

            // Act
            var result = controller.Details(2);

            // Assert
            Assert.AreEqual("Details",result.Result);
        }
    }
}
