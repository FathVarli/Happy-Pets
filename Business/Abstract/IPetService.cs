using Core.Utilities.Results;
using Entity.Dtos.PetDto;
using Entity.Dtos.PetDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IPetService
    {
        IResult SaveCat(SaveCatDto catDto);
        IResult SaveDog(SaveDogDto dogDto);
        IResult UpdatePet(SavePetDto petDto, int id);
        IResult DeletePet(int id);
        IDataResult<List<PetListDto>> GetListPetByOwnerId(int ownerId);
        IDataResult<PetDetailDto> GetPetDetailById(int petId);
    }
}
