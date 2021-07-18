using Entity.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models
{
    public class CreatePetModel
    {
        public int typeId { get; set; }
        public SaveCatDto saveCatDto { get; set; }
        public SaveDogDto saveDogDto { get; set; }

    }
}
