using System;
using System.Linq;
using AutoMapper;
using BulletinBoard.Entities.Models;
using BulletinBoard.Entities.ViewModels;

namespace BulletinBoard.Services
{
    public class TopicsCountResolver : IValueResolver<Forum, ForumListViewModel, int>
    {
        public int Resolve(Forum source, ForumListViewModel destination, int member, ResolutionContext context)
        {
            return source.Topic.Count(t => t.ForumId == source.id && t.ReplyToTopicId == null);
        }
    }

    public class TopicRepliesCountResolver : IValueResolver<Topic, TopicListViewModel, int>
    {
        public int Resolve(Topic source, TopicListViewModel destination, int member, ResolutionContext context)
        {
            return source.InverseRootTopic.Count() - 1;
        }
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map User to UserViewModel
            CreateMap<User, UserViewModel>();

            CreateMap<User, UserForUpdateViewModel>();

            CreateMap<UserForRegistrationViewModel, User>();

            // Map ForumForCrearionViewModel to Forum
            CreateMap<ForumForCreationViewModel, Forum>()
                // Use the mapping value from the option value (DateTime.UtcNow) for CreatedDateTime member in Forum
                .ForMember(dest => dest.CreatedDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                // Use the mapping value from the option value (context's dictionary member) for OwnerId member in Forum
                // The option value is passed as parameter when calling Map() method
                // e.g. var forum = _mapper.Map<ForumForCreationViewModel, Forum>(model, option => option.Items["OwnerId"] = owner.Id);
                .ForMember(dest => dest.OwnerId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["OwnerId"]));
            
            CreateMap<Forum, ForumForUpdateViewModel>();

            CreateMap<ForumForUpdateViewModel, Forum>();

            CreateMap<Forum, ForumViewModel>();

            CreateMap<Forum, ForumListViewModel>()
                .ForMember(dest => dest.TopicsCount, option => option.MapFrom(new TopicsCountResolver()));

            CreateMap<TopicForCreationViewModel, Topic>()
                .ForMember(dest => dest.PostDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.OwnerId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["OwnerId"]));
            
            CreateMap<Topic, TopicForReplyViewModel>()
                .ForMember(dest => dest.ReplyToTopicId, option => option.MapFrom((src, dest, destMember, context) => context.Items["ToTopicId"]))
                .ForMember(dest => dest.ReplyToTopic, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["ToTopic"]));
            
            CreateMap<TopicForReplyViewModel, Topic>()
                .ForMember(dest => dest.PostDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.OwnerId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["OwnerId"]));

            CreateMap<Topic, TopicForUpdateViewModel>();

            CreateMap<TopicForUpdateViewModel, Topic>()
                .ForMember(dest => dest.ModifiedDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.ModifiedByUserId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["UserId"]));
            
            CreateMap<Topic, TopicViewModel>();

            CreateMap<Topic, TopicListViewModel>()
                .ForMember(dest => dest.RepliesCount, option => option.MapFrom(new TopicRepliesCountResolver()));

            CreateMap<Message, MessageViewModel>()
                .ForMember(dest => dest.FromUserName, option => option.MapFrom(src => src.FromUser.UserName))
                .ForMember(dest => dest.ToUserName, option => option.MapFrom(src => src.ToUser.UserName));

            CreateMap<MessageViewModel, Message>()
                .ForMember(dest => dest.SendDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FromUserId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["FromUserId"]));
            
            CreateMap<Message, MessageReplyViewModel>()
                .ForMember(dest => dest.ReplyToUserId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["ReplyToUserId"]))
                .ForMember(dest => dest.ReplyToUserName, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["ReplyToUserName"]))
                .ForMember(dest => dest.Title, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["Title"]));
            
            CreateMap<MessageReplyViewModel, Message>()
                .ForMember(dest => dest.SendDateTime, option => option.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.FromUserId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["FromUserId"]))
                .ForMember(dest => dest.ToUserId, option => 
                    option.MapFrom((src, dest, destMember, context) => context.Items["ToUserId"]));
        }
    }
}