using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Contracts;
using BulletinBoard.Entities.Models;
using BulletinBoard.Entities.RequestFeatures;
using Moq;
using Xunit;

namespace BulletinBoard.UnitTests
{
    public class ForumRepositoryTests
    {
        public PagedList<Forum> GetForums()
        {
            var mockForums = new PagedList<Forum>();
            for (int i = 1; i <= 10; i++)
            {
                mockForums.Add(new Forum
                {
                    id = i,
                    OwnerId = $"owner-{i}",
                    Name = $"Test name {i}",
                    Description = $"Test description {i}",
                    ForumImageUri = $"Test URI {i}",
                    IsLocked = false,
                    CreatedDateTime = DateTime.Now,
                    Owner = new User(),
                    Topic = new List<Topic>()
                });
            }

            return mockForums;
        }

        [Fact]
        public void GetForumsAsync_ReturnOnly5Forums_WhenPassingDefaultForumParams()
        {
            // Arrange            
            var forumParameters = new ForumParameters();

            var mockForumRepository = new Mock<IForumRepository>();
            mockForumRepository
                    .Setup(repo => repo.GetForumsAsync(forumParameters, false))
                    .Returns(Task.FromResult(
                        PagedList<Forum>.ToPagedList(GetForums(), forumParameters.PageNumber, forumParameters.PageSize)
                    ));

            // Act
            var result = mockForumRepository.Object.GetForumsAsync(forumParameters, false)
                            .GetAwaiter()
                            .GetResult();

            // Assert
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetForumsAsync_Return10Forums_WhenPageSizeIs10()
        {
            // Arrange            
            var forumParameters = new ForumParameters
            {
                PageSize = 10
            };

            var mockForumRepository = new Mock<IForumRepository>();
            mockForumRepository
                    .Setup(repo => repo.GetForumsAsync(forumParameters, false))
                    .Returns(Task.FromResult(
                        PagedList<Forum>.ToPagedList(GetForums(), forumParameters.PageNumber, forumParameters.PageSize)
                    ));

            // Act
            var result = mockForumRepository.Object.GetForumsAsync(forumParameters, false)
                            .GetAwaiter()
                            .GetResult();

            // Assert
            Assert.Equal(10, result.Count);
        }

        [Fact]
        public void GetForumsAsync_Return2ndPageForums_WhenPageNumberIs2WithDefaultPageSize()
        {
            // Arrange            
            var forumParameters = new ForumParameters
            {
                PageNumber = 2
            };

            var mockForumRepository = new Mock<IForumRepository>();
            mockForumRepository
                    .Setup(repo => repo.GetForumsAsync(forumParameters, false))
                    .Returns(Task.FromResult(
                        PagedList<Forum>.ToPagedList(GetForums(), forumParameters.PageNumber, forumParameters.PageSize)
                    ));

            // Act
            var result = mockForumRepository.Object.GetForumsAsync(forumParameters, false)
                            .GetAwaiter()
                            .GetResult();

            // Assert
            Assert.Contains("Test name 6", result[0].Name);
            Assert.Contains("Test name 7", result[1].Name);
            Assert.Contains("Test name 8", result[2].Name);
            Assert.Contains("Test name 9", result[3].Name);
            Assert.Contains("Test name 10", result[4].Name);
        }

        [Fact]
        public void GetForumByIdAsync_ReturnExactForumWithSpecifiedId_WhenPassingForumId()
        {
            // Arrange
            var mockForum = new Forum
            {
                id = 1,
                OwnerId = $"owner-1",
                Name = $"Test name 1",
                Description = $"Test description 1",
                ForumImageUri = $"Test URI 1",
                IsLocked = false,
                CreatedDateTime = DateTime.Now,
                Owner = new User(),
                Topic = new List<Topic>()
            };

            var mockForumRepository = new Mock<IForumRepository>();
            mockForumRepository
                    .Setup(repo => repo.GetForumByIdAsync(1, false))
                    .Returns(Task.FromResult(mockForum));

            // Act
            var result = mockForumRepository.Object.GetForumByIdAsync(1, false)
                            .GetAwaiter()
                            .GetResult();
            
            // Assert
            Assert.Equal(result, mockForum);
        }
    }
}
