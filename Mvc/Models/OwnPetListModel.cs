using Entity.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Models
{
    public class OwnPetListModel
    {
        public List<PetListDto> petListDto { get; set; }
    }
}
