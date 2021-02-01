using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BulletinBoard.Contracts;
using BulletinBoard.Entities.Models;
using BulletinBoard.Entities.RequestFeatures;
using BulletinBoard.Entities.ViewModels;
using BulletinBoard.Services;
using BulletinBoard.Services.RepositoryServices;
using Moq;
using Xunit;

namespace BulletinBoard.UnitTests
{
    public class ForumServiceTests
    {
        private readonly ForumService _sut;
        private readonly ForumParameters _forumParameters;
        private readonly Mock<IRepositoryManager> _repoManagerMock = new Mock<IRepositoryManager>();

        public ForumServiceTests()
        {
            // Configure mapping profile
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });

            // Create mapper
            var mapper = mapperConfig.CreateMapper();

            // Instantiate system under test
            _sut = new ForumService(_repoManagerMock.Object, null, mapper);

            _forumParameters = new ForumParameters();
        }

        public List<Forum> GetForums()
        {
            var mockForums = new List<Forum>();
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
        public async Task GetForumDetailAsync_ShouldReturnForumViewModel_WhenForumIdExist()
        {
            // Arrange
            int forumId = 1;
            string forumTitle = "Forum for testing";

            var forum = new Forum
            {
                id = forumId,
                Name = forumTitle
            };

            _repoManagerMock.Setup(repo => repo.Forum.GetForumDetailByIdAsync(forumId, false))
                .ReturnsAsync(forum);

            // Act
            var result = await _sut.GetForumDetailAsync(forumId);

            // Assert
            Assert.IsType<ForumViewModel>(result);
            Assert.Equal(forumId, result.id);
            Assert.Equal(forumTitle, result.Name);
        }

        [Fact]
        public async Task GetForumDetailAsync_ShouldReturnNothing_WhenForumIdDoesNotExist()
        {
            _repoManagerMock.Setup(repo => repo.Forum.GetForumDetailByIdAsync(100, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetForumDetailAsync(10);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetForumDetailForEditingAsync_ShouldReturnForumForUpdateViewModel_WhenForumIdExist()
        {
            // Arrange
            int forumId = 1;
            string forumTitle = "Forum for testing";

            var forum = new Forum
            {
                id = forumId,
                Name = forumTitle
            };

            _repoManagerMock.Setup(repo => repo.Forum.GetForumDetailByIdAsync(forumId, false))
                .ReturnsAsync(forum);

            // Act
            var result = await _sut.GetForumDetailForEditingAsync(forumId);

            // Assert
            Assert.IsType<ForumForUpdateViewModel>(result);
            Assert.Equal(forumId, result.id);
            Assert.Equal(forumTitle, result.Name);
        }

        [Fact]
        public async Task GetPagedAndTopForumsAsync_ShouldReturnForumListViewModels_WhenDefaultForumParamIsPassed()
        {
            // Arrange
            _repoManagerMock.Setup(repo => repo.Forum.GetForumsAsync(_forumParameters, false))
                .ReturnsAsync(
                    PagedList<Forum>.ToPagedList(GetForums(), _forumParameters.PageNumber, _forumParameters.PageSize)
                );
            
            _repoManagerMock.Setup(repo => repo.Forum.GetTopForumsAsync(false))
                .ReturnsAsync(GetForums().Where(f => f.Topic.Count() > 0));

            // Act
            var result = await _sut.GetPagedAndTopForumsAsync(_forumParameters);

            // Assert
            Assert.IsType<PagedList<ForumListViewModel>>(result.Item1);
            Assert.Equal(5, result.Item1.Count);
            Assert.IsType<List<ForumListViewModel>>(result.Item2);
            Assert.Empty(result.Item2);
        }
    }
}