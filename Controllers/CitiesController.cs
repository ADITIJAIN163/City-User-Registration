using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Models;

namespace MiniProject.Controllers
{
    public class CitiesController : Controller
    {
        // GET: CitiesController
        public ActionResult Index()
        {

            List<Cities> cities = Cities.DisplayAllCities();
            return View(cities);
           
        }

        // GET: CitiesController/Details/5
        public ActionResult Details(int id)
        {
            Cities obj = Cities.GetSingleCity(id);
            return View(obj);

        }

        // GET: CitiesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CitiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cities e)
        {
            try
            {
                Cities.Insert(e);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitiesController/Edit/5
        public ActionResult Edit(int id)
        {

            return View(Cities.GetSingleCity(id));
        }

        // POST: CitiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cities e)
        {
            try
            {
                Cities.Update(e);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CitiesController/Delete/5
        public ActionResult Delete(int id)
        {
         
            return View(Cities.GetSingleCity(id));
        }

        // POST: CitiesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,Cities c)
        {
            try
            {

                Cities.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
