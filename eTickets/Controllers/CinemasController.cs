using eTickets.Data.Static;
using eTickets.Models;
using eTickets.Models.Data;
using eTickets.Models.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController (ICinemasService service)
        {
            _service = service; 
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //GET: cinemas/create

        public IActionResult Create(int id)
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("Not Found");
            }
            else
            {
                return View(cinemaDetails); 
            }
        }

        //Get CInemas/edit

        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("Not Found");
            }
            else
            {
                return View(cinemaDetails);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        //Get CInemas/delete

        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
            {
                return View("Not Found");
            }
            return View(cinemaDetails);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);
            if (cinemaDetails == null)
            {
                return View("Not Found");
            }


            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
