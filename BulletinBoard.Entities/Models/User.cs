using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BulletinBoard.Entities.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Forums = new HashSet<Forum>();
            MessagesFromUser = new HashSet<Message>();
            MessagesToUser = new HashSet<Message>();
            TopicsModifiedByUser = new HashSet<Topic>();
            TopicsOwner = new HashSet<Topic>();
        }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsLocked { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }

        // Navigation Properties
        public ICollection<Forum> Forums { get; set; }                  // B
        public ICollection<Message> MessagesFromUser { get; set; }      // C
        public ICollection<Message> MessagesToUser { get; set; }        // D
        public ICollection<Topic> TopicsModifiedByUser { get; set; }    // E
        public ICollection<Topic> TopicsOwner { get; set; }             // F

        /******************************************************************************
        #
        # A: Principal key
        # B: Collection naviigation property, inverse navigation property of Forum.Owner
        # C: Collection navigation property, inverse navigation property of Message.FromUser
        # D: Collection naviigation property, inverse navigation property of Message.ToUser
        # E: Collection naviigation property, inverse navigation property of Topic.ModifiedByUser
        # F: Collection naviigation property, inverse navigation property of Topic.Owner
        #
        ******************************************************************************/
    }
}