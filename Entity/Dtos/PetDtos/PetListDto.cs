using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.PetDtos
{
    public class PetListDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PetType { get; set; }
        public string Age { get; set; }
        public string Weight { get; set; }
    }
}
