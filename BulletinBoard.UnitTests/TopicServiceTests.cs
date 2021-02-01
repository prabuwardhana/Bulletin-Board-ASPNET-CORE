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
    public class TopicServiceTests
    {
        private readonly TopicService _sut;
        private readonly Mock<IRepositoryManager> _repoManagerMock = new Mock<IRepositoryManager>();

        public TopicServiceTests()
        {
            // Configure mapping profile
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new MappingProfile());
            });

            // Create mapper
            var mapper = mapperConfig.CreateMapper();

            // Instantiate system under test
            _sut = new TopicService(_repoManagerMock.Object, null, mapper);
        }

        [Fact]
        public async Task GetTopicDetailAsync_ShouldReturnTopicViewModel_WhenTopicIdExist()
        {
            // Arrange
            int topicId = 1;
            string topicTitle = "Topic for testing";

            var topic = new Topic
            {
                id = topicId,
                Title = topicTitle
            };

            _repoManagerMock.Setup(repo => repo.Topic.GetTopicByIdAsync(topicId, false))
                .ReturnsAsync(topic);

            // Act
            var result = await _sut.GetTopicDetailAsync<TopicViewModel>(topicId);

            // Assert
            Assert.IsType<TopicViewModel>(result);
            Assert.Equal(topicId, result.id);
            Assert.Equal(topicTitle, result.Title);
        }

        [Fact]
        public async Task GetTopicDetailAsync_ShouldReturnTopicForUpdateViewModel_WhenTopicIdExist()
        {
            // Arrange
            int topicId = 1;
            string topicTitle = "Topic for testing";

            var topic = new Topic
            {
                id = topicId,
                Title = topicTitle
            };

            _repoManagerMock.Setup(repo => repo.Topic.GetTopicByIdAsync(topicId, false))
                .ReturnsAsync(topic);

            // Act
            var result = await _sut.GetTopicDetailAsync<TopicForUpdateViewModel>(topicId);

            // Assert
            Assert.IsType<TopicForUpdateViewModel>(result);
            Assert.Equal(topicId, result.id);
            Assert.Equal(topicTitle, result.Title);
        }

        [Fact]
        public async Task GetTopicDetailAsync_ShouldReturnNothing_WhenForumIdDoesNotExist()
        {
            _repoManagerMock.Setup(repo => repo.Topic.GetTopicByIdAsync(100, false))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetTopicDetailAsync<TopicForUpdateViewModel>(10);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetTopicForReplyingAsync_ShouldReturnTopicForReplyViewModel_WhenToTopicIdExist()
        {
            // Arrange
            int toTopicId = 1;
            string toTopicTitle = "Topic for testing";

            var topic = new Topic
            {
                id = toTopicId,
                Title = toTopicTitle
            };

            _repoManagerMock.Setup(repo => repo.Topic.GetTopicByIdAsync(toTopicId, false))
                .ReturnsAsync(topic);

            // Act
            var result = await _sut.GetTopicForReplyingAsync(toTopicId);

            // Assert
            Assert.IsType<TopicForReplyViewModel>(result);
            Assert.Equal(toTopicId, result.id);
            Assert.Equal(toTopicTitle, result.Title);
        }

        [Fact]
        public async Task GetPagedAndTopTopicsFromForumAsync_ShouldReturnTopicListViewModel_WhenForumIdExist()
        {
            // Arrange
            int forumId = 1;
            string forumTitle = "Forum for testing";

            var forum = new Forum
            {
                id = forumId,
                Name = forumTitle
            };

            var topics = new List<Topic>();
            for (int i = 1; i <= 10; i++)
            {
                topics.Add(new Topic
                {
                    id = i,
                    Title = $"Test Title {i}",
                    ForumId = forumId
                });
            }

            _repoManagerMock.Setup(repo => repo.Forum.GetForumDetailByIdAsync(forumId, false))
                .ReturnsAsync(forum);

            _repoManagerMock.Setup(repo => repo.Topic.GetTopicsForForumWithReplyAsync(forumId, false))
                .ReturnsAsync(topics.Where(t => t.ForumId.Equals(forumId)));
            
            _repoManagerMock.Setup(repo => repo.Topic.GetTopTopicsAsync(false))
                .ReturnsAsync(forum.Topic.Where(t => t.InverseRootTopic.Count() > 1));

            // Act
            var result = await _sut.GetPagedAndTopTopicsFromForumAsync(1);

            // Assert
            Assert.IsType<ForumViewModel>(result.Item1);
            Assert.Equal(10, result.Item1.Topic.Count);
            Assert.IsType<List<TopicListViewModel>>(result.Item2);
            Assert.Empty(result.Item2);
        }
    }
}