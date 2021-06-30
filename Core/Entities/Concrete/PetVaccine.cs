using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Concrete
{
    [Table("pet_vaccine")]
    public class PetVaccine : IEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("pet_id")]
        public int PetId { get; set; }
        [Column("vaccine_id")]
        public int VaccineId { get; set; }
    }
}
