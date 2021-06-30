using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("user_operation_claim")]
    public class UserOperationClaim : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("operation_claim_id")]
        public int OperationClaimId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("OperationClaimId")]
        public OperationClaim OperationClaim { get; set; }
    }
}
