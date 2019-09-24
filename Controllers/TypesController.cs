using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspCoreEmpty.Models;

namespace aspCoreEmpty.Controllers
{
    public class TypesController : Controller
    {
        private readonly tasksContext _context;

        public TypesController(tasksContext context)
        {
            _context = context;
        }

        // GET: Types
        public IActionResult Index()
        {
            var ListTypes = _context.Types.ToList();
            var NewList = new List<TypesExtended>();
            foreach (var type in ListTypes)
            {
                var NameCategory = _context.Categories.Where(cat => cat.Idcategories == type.Idcategory).FirstOrDefault().Name;
                var nitem = new TypesExtended(type);
                
                nitem.NameCategory = NameCategory;
                NewList.Add(nitem);
            }
            return View(NewList);
        }

        // GET: Types/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types = await _context.Types
                .FirstOrDefaultAsync(m => m.Idtypes == id);
            if (types == null)
            {
                return NotFound();
            }

            var NameCategory = _context.Categories.Where(cat => cat.Idcategories == types.Idcategory).First().Name;
            var typesExt = new TypesExtended(types);

            typesExt.NameCategory = NameCategory;
            return View(typesExt);
        }

        // GET: Types/Create
        public IActionResult Create()
        {
            var typeExt = new TypesExtended();
            //заполняем выпадающий список для создания
            var allCategories = _context.Categories.ToList();
            foreach (var item in allCategories)
            {
                //добавляем категории в список

                typeExt.NameCategoriesAll.Add(item.Name);
            }
            return View(typeExt);
        }

        // POST: Types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TypesExtended types)
        {
            
                //проверим ИД типа
                if (types.Idtypes == 0)
                {
                    //generate id
                    int maxid = 0;
                    
                    foreach (var cat in _context.Types)
                    {
                        if (cat.Idtypes > maxid)
                        {
                            maxid = cat.Idtypes;
                        }
                    }
                    types.Idtypes = maxid + 1;
                }
                var typeOrd = new Types(types);
                //получаем имя выбранной категорию из модели TypesExtended
                var SelectedCategoryName = types.NameCategoriesAll[0];
                //получаем саму категорию из базы по имени
                var Category = _context.Categories.Where(c => c.Name == SelectedCategoryName).FirstOrDefault();
                if (Category != null)
                {
                    //сохраняем ИД категории в типе
                    typeOrd.Idcategory = Category.Idcategories;
                    //Обновляем БД
                    _context.Add(typeOrd);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            
            return View(types);
        }

        // GET: Types/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types = await _context.Types.FindAsync(id);
            if (types == null)
            {
                return NotFound();
            }
            
            //получаем имя категории которая задана в типе по ИД
            var NameCategory = _context.Categories.Where(cat => cat.Idcategories == types.Idcategory).First().Name;
            var typesExt = new TypesExtended(types);
            //добавляем имя категории в выпадающий список
            typesExt.NameCategoriesAll.Add(NameCategory);
            var allCategories = _context.Categories.ToList();
            foreach (var item in allCategories)
            {
                //добавляем остальные категории в список
                if (item.Idcategories!=types.Idcategory)
                    typesExt.NameCategoriesAll.Add(item.Name);
            }
            typesExt.NameCategory = NameCategory;
            return View(typesExt);
        }

        // POST: Types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TypesExtended types)
        {
            if (id != types.Idtypes)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var typeOrd = new Types(types);
                    //получаем имя выбранной категорию из модели TypesExtended
                    var SelectedCategoryName = types.NameCategoriesAll[0];
                    //получаем саму категорию из базы по имени
                    var Category = _context.Categories.Where(c => c.Name == SelectedCategoryName).FirstOrDefault();
                    if (Category != null)
                    {
                        //сохраняем ИД категории в типе
                        typeOrd.Idcategory = Category.Idcategories;
                        //Обновляем БД
                        _context.Update(typeOrd);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypesExists(types.Idtypes))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(types);
        }

        // GET: Types/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var types = await _context.Types
                .FirstOrDefaultAsync(m => m.Idtypes == id);
            if (types == null)
            {
                return NotFound();
            }

            return View(types);
        }

        // POST: Types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var types = await _context.Types.FindAsync(id);
            _context.Types.Remove(types);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypesExists(int id)
        {
            return _context.Types.Any(e => e.Idtypes == id);
        }
    }
}
