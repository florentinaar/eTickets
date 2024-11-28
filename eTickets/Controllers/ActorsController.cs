using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
                return View("Empty");

            return View(actorDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var details = await _service.GetByIdAsync(id);

            if (details == null)
                return View("NotFound");

            return View(details);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetByIdAsync(id);

            if(actor == null)
                return View("NotFound");

            return View(actor);
        } 

        #region POST

        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePicURL, Bio")]Actor input)
        {
            if (!ModelState.IsValid)
                return View(input);

           await _service.AddAsync(input);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,[Bind("FullName, ProfilePicURL,Bio, Id")] Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);

            await _service.UpdateAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
         {
            var actor = await _service.GetByIdAsync(id);

            if (actor == null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region PrivateUtils
        #endregion

    }
}
