using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.Concrete
{
    [Table("vaccine")]
    public class Vaccine : IEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("pet_type_id")]
        public int PetTypeId { get; set; }
        [Column("is_repetitive")]
        public bool isRepetitive { get; set; }
        [Column("min_week")]
        public int MinWeek { get; set; }
        [Column("max_week")]
        public int MaxWeek { get; set; }
        [Column("repetitive_month_time")]
        public int RepetitiveMonthTime { get; set; }
    }
}
