using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.PetDtos
{
    public class PetDetailDto
    {
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Genus { get; set; }
        public string Weight { get; set; }
        public string ImageUrl { get; set; }
        public List<PetVaccineDto> petVaccineDtos { get; set; }

    }
}
