using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Services.RepositoryServices
{
    public class TopicService : ITopicService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public TopicService(IRepositoryManager repositoryManager, UserManager<User> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<(ForumViewModel, IEnumerable<TopicListViewModel>)> GetPagedAndTopTopicsFromForumAsync(int forumId)
        {
            var forum = await _repositoryManager.Forum.GetForumDetailByIdAsync(forumId, trackChanges: false);
            forum.Topic = await _repositoryManager.Topic.GetTopicsForForumWithReplyAsync(forumId, trackChanges: false);            
            var model = _mapper.Map<ForumViewModel>(forum);

            var topTopics = await _repositoryManager.Topic.GetTopTopicsAsync(trackChanges: false);
            var topTopicsModel = _mapper.Map<IEnumerable<TopicListViewModel>>(topTopics);


            return (model, topTopicsModel);
        }

        public async Task CreateTopicAsync(TopicForCreationViewModel model, string userName)
        {
            var forum = await _repositoryManager.Forum.GetForumByIdAsync(model.ForumId, trackChanges: false);
            var owner = await _userManager.FindByNameAsync(userName);
            

            var topic = _mapper.Map<TopicForCreationViewModel, Topic>(model, opts => opts.Items["OwnerId"] = owner.Id);

            _repositoryManager.Topic.CreateTopicForForum(topic);
            await _repositoryManager.SaveAsync();

            topic.RootTopicId = topic.id;

            _repositoryManager.Topic.UpdateTopic(topic);
            await _repositoryManager.SaveAsync();
        }

        public async Task<T> GetTopicDetailAsync<T>(int id)
        {
            var topic = await _repositoryManager.Topic.GetTopicByIdAsync(id, trackChanges: false);
            var model = _mapper.Map<T>(topic);

            return model;
        }

        public async Task<TopicViewModel> GetTopicWithAllRepliesAsync(int id)
        {
            var rootTopic = await _repositoryManager.Topic.GetTopicDetailByIdAsync(id, false);

            rootTopic.InverseReplyToTopic = await _repositoryManager.Topic.GetInverseReplyToTopicAsync(id, trackChanges: false);

            var model = _mapper.Map<TopicViewModel>(rootTopic);  

            foreach (var item in model.InverseReplyToTopic)
            {
                item.OwnerRoles = await _userManager.GetRolesAsync(item.Owner);
            }                      

            model.OwnerRoles = await _userManager.GetRolesAsync(rootTopic.Owner);

            return model;
        }

        public async Task<TopicForReplyViewModel> GetTopicForReplyingAsync(int toId)
        {
            var toTopic = await _repositoryManager.Topic.GetTopicByIdAsync(toId, trackChanges: false);

            var model = _mapper.Map<Topic, TopicForReplyViewModel>(toTopic, opts =>
            {
                opts.Items["ToTopicId"] = toTopic.id;
                opts.Items["ToTopic"] = toTopic;
            });

            return model;
        }

        public async Task ReplyToTopicAsync(TopicForReplyViewModel model, string userName)
        {
            var owner = await _userManager.FindByNameAsync(userName);
            var topic = _mapper.Map<TopicForReplyViewModel, Topic>(model, opts => opts.Items["OwnerId"] = owner.Id);

            _repositoryManager.Topic.CreateTopicForForum(topic);
            await _repositoryManager.SaveAsync();
        }        

        public async Task EditTopicAsync(TopicForUpdateViewModel model, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var topic = await _repositoryManager.Topic.GetTopicByIdAsync(model.id, trackChanges: true);

            _mapper.Map(model, topic, opts => opts.Items["UserId"] = user.Id);

            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteTopicAsync(TopicViewModel model)
        {
            var topic = await _repositoryManager.Topic.GetTopicByIdAsync(model.id, trackChanges: false);            

            if (topic.id == topic.RootTopicId)
            {
                var descendants = await _repositoryManager.Topic.GetDescendantTopicsAsync(topic.id, trackChanges: false);
                _repositoryManager.Topic.CascadeDeleteTopic(descendants);
            }
            else
            {
                _repositoryManager.Topic.DeleteTopic(topic);
            }

            await _repositoryManager.SaveAsync();
        }
    }
}