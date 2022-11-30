using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers
{
    public class ItemController : Controller
    {
        protected IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(createItemModel.Description);
                await repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }  

            return View(createItemModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await repository.FindById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItem([Bind("Id,Description,Done,CreatedAt")] EditItemModel editItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(editItemModel.Id, editItemModel.Description, editItemModel.Done, editItemModel.CreatedAt);
                await repository.EditAsync(item);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(string Id)
        {

            if (Id == null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(Id);
            
            return RedirectToAction(nameof(Index));
        }
    }
}
