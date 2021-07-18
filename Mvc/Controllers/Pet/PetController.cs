using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mvc.Extension;
using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.Controllers.Pet
{
    public class PetController : Controller
    {
        IPetService _petService;
        IHttpContextAccessor _httpContext;

        public PetController(IPetService petService, IHttpContextAccessor httpContext)
        {
            _petService = petService;
            _httpContext = httpContext;
        }

        public IActionResult Index()
        {
            var userId = (int)_httpContext.HttpContext.Session.GetInt32("userId");
            var result = _petService.GetListPetByOwnerId(userId);
            if (result.Success)
            {
                var model = new OwnPetListModel
                {
                    petListDto = result.Data
                };
                return View(model);
            }
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePetModel createPetModel)
        {
            var userId = (int)_httpContext.HttpContext.Session.GetInt32("userId");

            if (createPetModel.typeId == 1)
            {
                createPetModel.saveCatDto.OwnerId = userId;
                var result = _petService.SaveCat(createPetModel.saveCatDto);
                if (result.Success)
                {
                    return Json(new { success = true, message = result.Message });
                }
                return Json(new { success = false, message = result.Message });
            }
            else
            {
                createPetModel.saveDogDto.OwnerId = userId;
                var result = _petService.SaveDog(createPetModel.saveDogDto);
                if (result.Success)
                {
                    return Json(new { success = true, message = result.Message });
                }
                return Json(new { success = false, message = result.Message });
            }

        }

        public IActionResult Detail(int id)
        {
            var result = _petService.GetPetDetailById(id);
            if (result.Success)
            {
                var model = new PetDetailModel
                {
                    petDetailDto = result.Data
                };
                return View(model);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _petService.DeletePet(id);
            if (result.Success)
            {
                return RedirectToAction("Index", "Pet");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
