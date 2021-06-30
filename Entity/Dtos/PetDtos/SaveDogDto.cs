using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.PetDtos
{
    public class SaveDogDto : IDto
    {
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public int GenusId { get; set; }
        public double Weight { get; set; }
        public string Birthday { get; set; }
        public bool isParazit { get; set; }
        public bool isKuduz { get; set; }
        public bool isKarmaI { get; set; }
        public bool isKarmaII { get; set; }
        public bool isBronshineI { get; set; }
        public bool isBronshineII { get; set; }
        public bool isLyme { get; set; }
        public bool isCoronaI { get; set; }
        public bool isCoronaII { get; set; }
    }
}
