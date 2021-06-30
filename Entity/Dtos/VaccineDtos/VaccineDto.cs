using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.VaccineDtos
{
    public class VaccineDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PetTypeId { get; set; }
        public bool isRepetitive { get; set; }
        public int MinWeek { get; set; }
        public int MaxWeek { get; set; }
    }
}
