using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Concrete
{
    [Table("pet")]
    public class Pet : IEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("birthday")]
        public DateTime BirthDay { get; set; }
        [Column("weight_gr")]
        public double WeightGr { get; set; }
        [Column("pet_type_id")]
        public int PetTypeId { get; set; }
        [Column("owner_id")]
        public int OwnerId { get; set; }
        [Column("genus_id")]
        public int GenusId { get; set; }
        [Column("image_url")]
        public string ImageUrl { get; set; }
        [Column("is_deleted")]
        public bool isDeleted { get; set; }
    }
}
