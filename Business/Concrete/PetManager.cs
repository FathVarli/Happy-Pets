using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Dtos.PetDto;
using Entity.Dtos.PetDtos;
using Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class PetManager : IPetService
    {
        IPetDal _petDal;
        IPetVaccineDal _petVaccineDal;
        IVaccineDal _vaccineDal;
        IUserDal _userDal;
        IPetTypeDal _petTypeDal;
        public PetManager(IPetDal petDal, IPetVaccineDal petVaccineDal, IUserDal userDal, IPetTypeDal petTypeDal, IVaccineDal vaccineDal)
        {
            _petDal = petDal;
            _petVaccineDal = petVaccineDal;
            _userDal = userDal;
            _petTypeDal = petTypeDal;
            _vaccineDal = vaccineDal;
        }

        public IResult SaveCat(SaveCatDto catDto)
        {
            User existUser = _userDal.Get(u => u.Id == catDto.OwnerId);
            if (existUser == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }
            var existPet = _petDal.Get(p => p.OwnerId == catDto.OwnerId && p.PetTypeId == (int)EnmType.Cat && p.Name == catDto.Name.ToUpper() && p.isDeleted == false);
            if (existPet != null)
            {
                return new ErrorResult("Bu özelliklere sahip bir kediniz bulunmaktadır.");
            }


            if (catDto.GenusId > 0)
            {
                // geneus check
            }

            var newPet = new Pet
            {
                Name = catDto.Name.ToUpper(),
                BirthDay = ConvertStringToDate(catDto.Birthday),
                PetTypeId = (int)EnmType.Cat,
                OwnerId = catDto.OwnerId,
                GenusId = 1,
                WeightGr = catDto.Weight,
                isDeleted = false,
                ImageUrl = "/metronic/theme/demo1/dist/assets/media/custom/cat.jpg"
            };

            _petDal.Add(newPet);
            var addedPet = _petDal.Get(p => p.OwnerId == catDto.OwnerId && p.PetTypeId == (int)EnmType.Cat && p.Name == catDto.Name.ToUpper() && p.isDeleted == false);
            AddCatVaccine(catDto, addedPet.Id);
            return new SuccessResult("Kediniz başarıyla eklenmiştir.");
        }

        public IResult SaveDog(SaveDogDto dogDto)
        {
            User existUser = _userDal.Get(u => u.Id == dogDto.OwnerId);
            if (existUser == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı");
            }
            var existPet = _petDal.Get(p => p.OwnerId == dogDto.OwnerId && p.PetTypeId == (int)EnmType.Cat && p.Name == dogDto.Name.ToUpper() && p.isDeleted == false);
            if (existPet != null)
            {
                return new ErrorResult("Bu özelliklere sahip bir köpeğiniz bulunmaktadır.");
            }


            if (dogDto.GenusId > 0)
            {
                // geneus check
            }

            var newPet = new Pet
            {
                Name = dogDto.Name.ToUpper(),
                BirthDay = ConvertStringToDate(dogDto.Birthday),
                PetTypeId = (int)EnmType.Cat,
                OwnerId = dogDto.OwnerId,
                GenusId = 1,
                WeightGr = dogDto.Weight,
                isDeleted = false,
                ImageUrl = "/metronic/theme/demo1/dist/assets/media/custom/dog.jpg"
            };

            _petDal.Add(newPet);
            var addedPet = _petDal.Get(p => p.OwnerId == dogDto.OwnerId && p.PetTypeId == (int)EnmType.Cat && p.Name == dogDto.Name.ToUpper() && p.isDeleted == false);
            AddDogVaccine(dogDto, addedPet.Id);
            return new SuccessResult("Köpeğiniz başarıyla eklenmiştir.");
        }

        public IResult UpdatePet(SavePetDto petDto, int id)
        {
            throw new NotImplementedException();
        }
        public IResult DeletePet(int id)
        {
            var existPet = _petDal.Get(p => p.Id == id && p.isDeleted == false);
            if (existPet != null)
            {
                existPet.isDeleted = true;
                _petDal.Update(existPet);
                return new SuccessResult("Evcil hayvanınız başarıyla silinmiştir.");
            }
            return new ErrorResult("Evcil hayvanınız bulunamadı.");
        }

        public IDataResult<List<PetListDto>> GetListPetByOwnerId(int ownerId)
        {
            var petList = new List<PetListDto>();
            var petListEntity = _petDal.GetList(p => p.OwnerId == ownerId);
            if (petListEntity.Count > 0)
            {
                foreach (var pet in petListEntity)
                {
                    var petType = _petTypeDal.Get(p => p.Id == pet.PetTypeId);

                    var petListDto = new PetListDto
                    {
                        Id = pet.Id,
                        Name = pet.Name,
                        PetType = petType.Name,
                        Age = PetAge(pet.BirthDay),
                        Weight = Math.Round(pet.WeightGr / 1000, 2).ToString()
                    };

                    petList.Add(petListDto);
                }
            }
            return new SuccessDataResult<List<PetListDto>>(petList);
        }

        public IDataResult<PetDetailDto> GetPetDetailById(int petId)
        {
            var pet = _petDal.Get(p => p.Id == petId);
            if (pet == null)
            {
                return new ErrorDataResult<PetDetailDto>("Pet bulunamadı!");
            }

            var petAgeArr = CalculatePetAgeArr(pet.BirthDay);
            int petAgeYear = petAgeArr[2];
            var petDetailDto = new PetDetailDto
            {
                Name = pet.Name,
                Age = PetAge(pet.BirthDay),
                Weight = Math.Round(pet.WeightGr / 1000, 2).ToString(),
                TypeId = pet.PetTypeId,
                Genus = "TEST",
                ImageUrl = pet.ImageUrl,
                petVaccineDtos = petAgeYear < 1 ? GetPetVaccineLowerOneAge(petId, pet.PetTypeId, pet.BirthDay) : GetPetVaccineUpperOneAge(petId, pet.PetTypeId, pet.BirthDay)

            };
            return new SuccessDataResult<PetDetailDto>(petDetailDto);
        }

        private List<PetVaccineDto> GetPetVaccineLowerOneAge(int petId, int petTypeId, DateTime birthday)
        {

            var petVaccineDtoList = new List<PetVaccineDto>();

            var petDontVaccineList = new List<Vaccine>();

            var vaccineList = _vaccineDal.GetList(v => v.PetTypeId == petTypeId || v.PetTypeId == (int)EnmType.Common);

            var petVaccineList = _petVaccineDal.GetList(p => p.PetId == petId);

            foreach (var vaccine in vaccineList)
            {
                bool isUse = false;
                foreach (var petVaccine in petVaccineList)
                {
                    if (petVaccine.VaccineId == vaccine.Id)
                    {
                        isUse = true;
                        break;
                    }
                }
                if (!isUse)
                {
                    petDontVaccineList.Add(vaccine);
                }

            }

            foreach (var vaccine in petDontVaccineList)
            {
                var date = birthday.AddDays(Convert.ToDouble(vaccine.MaxWeek * 7));
                if (date >= DateTime.Now && date <= DateTime.Now.AddMonths(2))
                {
                    var vaccinePetDto = new PetVaccineDto
                    {
                        VaccineName = vaccine.Name,
                        VaccineDate = $"{date.Day}/{date.Month}/{date.Year}"

                    };
                    petVaccineDtoList.Add(vaccinePetDto);
                }
            }

            petVaccineDtoList = petVaccineDtoList.OrderBy(t => Convert.ToDateTime(t.VaccineDate)).ToList();

            return petVaccineDtoList;
        }

        private List<PetVaccineDto> GetPetVaccineUpperOneAge(int petId, int petTypeId, DateTime birthday)
        {
            var petVaccineDtoList = new List<PetVaccineDto>();

            var petDontVaccineList = new List<Vaccine>();

            var vaccineList = _vaccineDal.GetList(v => v.PetTypeId == petTypeId || v.PetTypeId == (int)EnmType.Common);

            var petVaccineList = _petVaccineDal.GetList(p => p.PetId == petId);

            foreach (var vaccine in vaccineList)
            {
                if (vaccine.isRepetitive)
                {
                    petDontVaccineList.Add(vaccine);
                }
            }

            foreach (var vaccine in petDontVaccineList)
            {
                var date = birthday.AddDays(Convert.ToDouble(vaccine.MaxWeek * 7) + Convert.ToDouble(vaccine.RepetitiveMonthTime * 30));
                if (date >= DateTime.Now && date <= DateTime.Now.AddMonths(2))
                {
                    var vaccinePetDto = new PetVaccineDto
                    {
                        VaccineName = vaccine.Name,
                        VaccineDate = $"{date.Day}/{date.Month}/{date.Year}"

                    };
                    petVaccineDtoList.Add(vaccinePetDto);
                }
            }
            petVaccineDtoList = petVaccineDtoList.OrderBy(t => Convert.ToDateTime(t.VaccineDate)).ToList();
            return petVaccineDtoList;
        }

        private void AddCatVaccine(SaveCatDto catDto, int petId)
        {
            var vaccinations = new List<int>();
            if (catDto.isKarmaI)
            {
                vaccinations.Add((int)EnmVaccine.KarmaI);
            }

            if (catDto.isKarmaII)
            {
                vaccinations.Add((int)EnmVaccine.KarmaII);
            }
            if (catDto.isKuduz)
            {
                vaccinations.Add((int)EnmVaccine.Kuduz);
            }
            if (catDto.isLosemiI)
            {
                vaccinations.Add((int)EnmVaccine.LosemiI);
            }
            if (catDto.isLosemiII)
            {
                vaccinations.Add((int)EnmVaccine.LosemiII);
            }
            if (catDto.isParazit)
            {
                vaccinations.Add((int)EnmVaccine.IcDisParazit);
            }

            foreach (var item in vaccinations)
            {
                var vaccine = _vaccineDal.Get(v => v.Id == item);
                if (vaccine != null)
                {
                    var petVaccine = new PetVaccine
                    {
                        PetId = petId,
                        VaccineId = vaccine.Id,
                    };
                    _petVaccineDal.Add(petVaccine);
                }
            }
        }

        private void AddDogVaccine(SaveDogDto dogDto, int petId)
        {
            var vaccinations = new List<int>();
            if (dogDto.isKarmaI)
            {
                vaccinations.Add((int)EnmVaccine.KarmaI);
            }

            if (dogDto.isKarmaII)
            {
                vaccinations.Add((int)EnmVaccine.KarmaII);
            }
            if (dogDto.isKuduz)
            {
                vaccinations.Add((int)EnmVaccine.Kuduz);
            }
            if (dogDto.isBronshineI)
            {
                vaccinations.Add((int)EnmVaccine.BronshineI);
            }
            if (dogDto.isBronshineII)
            {
                vaccinations.Add((int)EnmVaccine.BronshineII);
            }
            if (dogDto.isLyme)
            {
                vaccinations.Add((int)EnmVaccine.Lyme);
            }
            if (dogDto.isParazit)
            {
                vaccinations.Add((int)EnmVaccine.IcDisParazit);
            }
            if (dogDto.isCoronaI)
            {
                vaccinations.Add((int)EnmVaccine.CoronaI);
            }
            if (dogDto.isCoronaII)
            {
                vaccinations.Add((int)EnmVaccine.CoronaII);
            }

            foreach (var item in vaccinations)
            {
                var vaccine = _vaccineDal.Get(v => v.Id == item);
                if (vaccine != null)
                {
                    var petVaccine = new PetVaccine
                    {
                        PetId = petId,
                        VaccineId = vaccine.Id,
                    };
                    _petVaccineDal.Add(petVaccine);
                }
            }
        }

        private DateTime ConvertStringToDate(string date)
        {
            var strArr = date.Split(' ');
            var dateArr = strArr[0].Split('/');
            int year = Convert.ToInt32(dateArr[2]);
            int month = Convert.ToInt32(dateArr[0]);
            int day = Convert.ToInt32(dateArr[1]);
            return new DateTime(year, month, day);
        }
        private string PetAge(DateTime date)
        {
            var ageArr = CalculatePetAgeArr(date);
            int farkGun = ageArr[0];
            int farkAy = ageArr[1];
            int farkYil = ageArr[2];

            if (farkYil > 0)
            {
                var result = $"{farkYil} Yıl ";
                if (farkAy > 0)
                {
                    result += $"{farkAy} Ay";
                    return result;
                }
                return result;
            }
            else if (farkAy > 0)
            {
                var result = $"{farkAy} Ay ";
                if (farkGun > 0)
                {
                    result += $"{farkGun} Gün";
                    return result;
                }
                return result;
            }
            else
            {
                if (farkGun > 7)
                {
                    return $"{farkGun / 7} Hafta";
                }
                return $"{farkGun} Gün";
            }
        }

        private int[] CalculatePetAgeArr(DateTime date)
        {
            var ageArr = new int[3];

            int ilkGun, ilkAy, ilkYil;

            int sonGun, sonAy, sonYil;

            int farkYil, farkAy, farkGun;

            farkYil = 0; farkAy = 0; farkGun = 0;

            var ilkTarih = date;
            var sonTarih = DateTime.Now;

            ilkYil = ilkTarih.Year;

            ilkAy = ilkTarih.Month;

            ilkGun = ilkTarih.Day;



            sonGun = sonTarih.Day;

            sonAy = sonTarih.Month;

            sonYil = sonTarih.Year;



            if (sonGun < ilkGun)

            {

                sonGun += DateTime.DaysInMonth(sonYil, sonAy);

                farkGun = sonGun - ilkGun;

                sonAy--;

                if (sonAy < ilkAy)

                {

                    sonAy += 12;

                    sonYil--;

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

                else

                {

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

            }

            else

            {

                farkGun = sonGun - ilkGun;

                if (sonAy < ilkAy)

                {

                    sonAy += 12;

                    sonYil--;

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }

                else

                {

                    farkAy = sonAy - ilkAy;

                    farkYil = sonYil - ilkYil;

                }
            }

            ageArr[0] = farkGun;
            ageArr[1] = farkAy;
            ageArr[2] = farkYil;

            return ageArr;
        }


    }
}