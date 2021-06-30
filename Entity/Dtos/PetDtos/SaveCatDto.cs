using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.PetDtos
{
    public class SaveCatDto : IDto
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
        public bool isLosemiI { get; set; }
        public bool isLosemiII { get; set; }
    }
}
