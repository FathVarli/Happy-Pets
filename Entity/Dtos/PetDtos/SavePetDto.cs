using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.PetDto
{
    public class SavePetDto : IDto
    {
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public double WeightGr { get; set; }
        public int PetTypeId { get; set; }
        public int OwnerId { get; set; }
        public int GenusId { get; set; }
        public List<int> Vaccinations { get; set; }
    }
}
