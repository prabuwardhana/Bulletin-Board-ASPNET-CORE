using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulletinBoard.Entities.Models
{
    public class Forum
    {
        public Forum()
        {
            Topic = new HashSet<Topic>();
        }

        [Column("ID")]
        public int id { get; set; }                     // A   
        [Column("OwnerID")]
        public string OwnerId { get; set; }             // B
        public string Name { get; set; }
        public string Description { get; set; }
        public string ForumImageUri { get; set; }
        public bool IsLocked { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public User Owner { get; set; }                 // C
        public ICollection<Topic> Topic { get; set; }   // D

        /******************************************************************************
        #
        # A: Principal key
        # B: Foreign key, constrained by User.Id
        # C: Refefence navigation property, inverse navigation property of User.Forums
        # D: Collection naviigation property, inverse navigation property of Topic.Forum
        #
        ******************************************************************************/
    }
}