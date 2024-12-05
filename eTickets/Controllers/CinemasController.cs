using eTickets.Data.Services;
using eTickets.Data.Static;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var cinemas = await _service.GetAllAsync();

            return View(cinemas);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return View("NotFound");

            return View(item);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                return View("NotFound");
            }

            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
            {
                return View("NotFound");
            }

            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        #region Post actions

        [HttpPost]
        public async Task<IActionResult> Edit(Cinema input)
        {
            if (!ModelState.IsValid)
                return View(input);

            await _service.UpdateAsync(input);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            await _service.AddAsync(input);

            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
