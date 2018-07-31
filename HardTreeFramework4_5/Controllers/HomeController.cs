using HardTree.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HardTree.Controllers
{
    public class HomeController : Controller
    {
        private List<ValkaType> _valkaTypes = new List<ValkaType>
        {
            new ValkaType { Id = 1, Name = "Спил целиком", Coefficient = 10 },
            new ValkaType { Id = 2, Name = "Спил с оттяжкой", Coefficient = 20 },
            new ValkaType { Id = 3, Name = "Спил частями", Coefficient = 30 },
            new ValkaType { Id = 4, Name = "Спил с завешиванием", Coefficient = 40 }
        };

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Calculate()
        {
            var typesSelectList = new SelectList(_valkaTypes, "Id", "Name");
            return View(typesSelectList);
        }

        [HttpPost]
        public ActionResult Calculate(int? treeDiameter, int? typeId, int? treesCount)
        {
            if (treeDiameter == null || typeId == null || treesCount == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Parameters cannot be null");


            if (treeDiameter < 0 || typeId < 0 || treesCount < 0)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Parameters cannot be negative numbers");

            var valkaType = _valkaTypes.FirstOrDefault(type => type.Id == typeId);
            if (valkaType == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Не проходит");

            var price = treeDiameter * valkaType.Coefficient * treesCount;

            var result = new CalculatingResult
            {
                ValkaTypeName = valkaType.Name,
                TreesCount = treesCount.Value,
                Price = price.Value,
                TreeDiameter = treeDiameter.Value
            };

            return View("CalculatingResult", result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}