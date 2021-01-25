using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Topic
    {
        public Topic()
        {
            InverseReplyToTopic = new HashSet<Topic>();
            InverseRootTopic = new HashSet<Topic>();
        }
        
        [Column("ID")]
        public int id { get; set; }                 // A
        [Column("OwnerID")]
        public string OwnerId { get; set; }            // B
        [Column("ForumID")]
        public int ForumId { get; set; }            // C
        [Column("RootTopicID")]
        // top level topics will have a null value for the RootTopicId and ReplyToTopicId field
        public virtual int? RootTopicId { get; set; }       // D
        [Column("ReplyToTopicID")]
        public virtual int? ReplyToTopicId { get; set; }    // E
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        public DateTime PostDateTime { get; set; }
        [Column("ModifiedByUserID")]
        public string ModifiedByUserId { get; set; }  // F
        public DateTime? ModifiedDateTime { get; set; }
        public bool IsLocked { get; set; }

        /* Relationship */        
        public Forum Forum { get; set; }            // G
        public User ModifiedByUser { get; set; }    // H
        public User Owner { get; set; }             // I
        public Topic ReplyToTopic { get; set; }     // J
        public Topic RootTopic { get; set; }        // K
        public virtual ICollection<Topic> InverseReplyToTopic { get; set; } // L
        public virtual ICollection<Topic> InverseRootTopic { get; set; }    // M

        /******************************************************************************
        #
        # A: Principal key
        # B: Foreign key, constrained by User.Id
        # C: Foreign key, constrained by Forum.Id
        # D: Self-referencing relationship, constrained by Topic.Id ()
        # E: Self-referencing relationship, constrained by Topic.Id ()
        # F: Foreign key, constrained by User.Id
        # G: Refefence naviigation property, inverse navigation property of Forum.Topic
        # H: Refefence navigation property, inverse navigation property of User.TopicsModifiedByUser
        # I: Refefence naviigation property, inverse navigation property of User.TopicsOwner
        # J: Refefence naviigation property, inverse navigation property of Topic.InverseReplyToTopic
        # K: Refefence naviigation property, inverse navigation property of Topic.InverseRootTopic
        # L: Collection naviigation property, inverse navigation property of Topic.ReplyToTopic
        # M: Collection naviigation property, inverse navigation property of Topic.RootTopic
        #
        ******************************************************************************/
    }
}