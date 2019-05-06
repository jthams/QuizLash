using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Concrete;
using Domain.Entities;
using Domain.Abstract;
using WebUI.ViewModels;
using WebUI.Controllers;
using Xunit;
using Moq;

namespace XTestProject
{
    public class ControllerUnitTests
    {
        public static string _currentUser = "CurrentUser";
        public static List<Question> GetTest_Questions()
        {
            List<Question> questions = new List<Question>();
            for (int i = 0; i < 5; i++)
            {
                Question question = new Question
                {
                    QuestionID = i + 1,
                    Creator = _currentUser,
                    TopicID = i + 10,
                    Body = $"body{i}",
                    Answer = $"answer{i}"
                };
                questions.Add(question);
            }
            return questions;
        }
        public static List<Quiz> GetTest_Quizzes()
        {
            List<Quiz> quizzes = new List<Quiz>();
            for (int i = 0; i < 5; i++)
            {
                Quiz quiz = new Quiz
                {
                    QuizID = i + 1,
                    Owner = _currentUser,
                    TopicID = i + 10,
                    NumberOfQuestions = i + 2,
                    Score = i + 70
                };
                quizzes.Add(quiz);
            }
            return quizzes;
        }
        public static List<Topic> GetTest_Topics()
        {
            List<Topic> topics = new List<Topic>();
            for (int i = 0; i < 5; i++)
            {
                Topic question = new Topic
                {
                    TopicID = i + 1,
                    Description = $"text{i}"
                };
                topics.Add(question);
            }
            return topics;
        }
        public static Dictionary<int,decimal> GetTest_TopicPerfomance()
        {
            var testDictionary = new Dictionary<int, decimal>()
            {
                {1,65},{2,75},{3,85},{4,95}
            };
            return testDictionary;

        }
        public static Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
        public static Mock<IUserDataRepository> GetMockUserRepoStub()
        {
            var mockRepo = new Mock<IUserDataRepository>().SetupAllProperties();

            return mockRepo;
        }
        public static Mock<IDataRepository<Question>> GetMockQuestionRepoStub()
        {
            var mockRepo = new Mock<IDataRepository<Question>>();
           

            return new Mock<IDataRepository<Question>>(mockRepo.Object as Mock<QuestionRepository>);
        }
        public static Mock<IDataRepository<Quiz>> GetMockQuizRepoStub()
        {
            var mockRepo = new Mock<IDataRepository<Quiz>>().SetupAllProperties();

            return mockRepo;
        }
        public class UserAccountTests
        {
            [Fact]
            public void Index_ReturnsAViewResult_WithAUserAccountVM()
            {
                // Arrange
                var mockUserManager = GetMockUserManager();
                var mockUserDataRepository = GetMockUserRepoStub();

                var controller = new UserAccountController(GetMockUserRepoStub().Object, GetMockUserManager().Object,null,null);

                // Act
                var result =  controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<UserAccountViewModel>(
                    viewResult.ViewData.Model);
            }
        }
        public class QuestionsTests
        {
            [Fact]
            public void Index_ReturnsAViewResult_WithAQuestionObject()
            {
                //Arrange
                var controller = new QuestionsController(GetMockQuestionRepoStub().Object, GetMockUserManager().Object);

                // Act
                var result = controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Question>>(
                    viewResult.ViewData.Model);
            }
        }
    }
}
