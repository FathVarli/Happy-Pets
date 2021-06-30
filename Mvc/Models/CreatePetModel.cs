using Entity.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models
{
    public class CreatePetModel
    {
        public SaveCatDto saveCatDto { get; set; }
        public SaveDogDto saveDogDto { get; set; }

    }
}
