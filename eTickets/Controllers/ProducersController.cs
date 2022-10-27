using eTickets.Models;
using eTickets.Models.Data;
using eTickets.Models.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{

    [Authorize]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController (IProducersService service)
        {
            _service = service;    
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        //GET: producers/details/1
        [AllowAnonymous]    
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if(producerDetails == null)
            {
                return View("Not Found");
            }
            else
            {
                return View(producerDetails);
            }
        }

        //GET: producers/create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")]Producer producer)
        {
            if(!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await _service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
        }


        //GET: producers/edit

        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if( producerDetails == null)
            {
                return View("Nout Founnd");
            }
            else
            {
                return View(producerDetails);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            if(id == producer.Id)
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));

            }
            else
            {
                return View(producer);
            }
        }

        //GET: producers/delete

        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("Not Found");
            }
            return View(producerDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null)
            {
                return View("Not Found");
            }
          

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
