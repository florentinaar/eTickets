using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;

        public ProducersController(IProducersService service) => _service = service;
        public async Task<IActionResult> Index()
        {
            var producers = await _service.GetAllAsync();

            return View(producers);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producer = await _service.GetByIdAsync(id);

            if (producer == null)
                return View("NotFound");

            return View(producer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producent = await _service.GetByIdAsync(id);

            if (producent == null)
                return View("NotFound");

            return View(producent);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return View("NotFound");

            return View(item);
        }

        #region Post Actions
        
        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(Producer input)
        {
            if (!ModelState.IsValid)
                return View(input);

            await _service.AddAsync(input);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producer input)
        {
            if (!ModelState.IsValid)
                return View(input);

            await _service.UpdateAsync(input);
            return RedirectToAction(nameof(Index));
            //return Json("Saved");
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
