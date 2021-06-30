using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Concrete
{
    [Table("reset_password")]
    public class ResetPassword:IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("token")]
        public string Token { get; set; }
        [Column("expritaion_time")]
        public DateTime ExpirationTime { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
