using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Message
    {
        public Message()
        {
            
        }
        
        [Column("ID")]
        public int id { get; set; }                 // A
        [Column("FromUserID")]
        public string FromUserId { get; set; }         // B
        [Column("ToUserID")]
        public string ToUserId { get; set; }           // C
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public User FromUser { get; set; }          // D
        public User ToUser { get; set; }            // E

        /******************************************************************************
        #
        # A: Principal key
        # B: Foreign key, constrained by User.Id
        # C: Foreign key, constrained by User.Id
        # D: Reference navigation property, inverse navigation property of User.MessagesFromUser
        # E: Reference navigation property, inverse navigation property of User.MessagesToUser
        #
        ******************************************************************************/
    }
}