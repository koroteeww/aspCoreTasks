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
    public class TasksController : Controller
    {
        private readonly tasksContext _context;

        public TasksController(tasksContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public IActionResult Index()
        {
            var ListTasks = _context.Tasks.ToList();

            var NewList = new List<TasksExtended>();
            foreach (var task in ListTasks)
            {
                var nitem = new TasksExtended(task);
                var NameType = _context.Types.Where(tp => tp.Idtypes == task.Idtype).FirstOrDefault().Name;
                
                var NameStatus = _context.Statuses.Where(stat => stat.Idstatuses == task.Idstatus).FirstOrDefault().Name;

                nitem.NameType = NameType;
                nitem.NameStatus = NameStatus;
                NewList.Add(nitem);
            }
            return View(NewList);
            
        }

        // GET: Tasks/Details/5
        public  IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = _context.Tasks.FirstOrDefault(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }
            var nitem = new TasksExtended(tasks);
            var NameType = _context.Types.Where(tp => tp.Idtypes == tasks.Idtype).FirstOrDefault().Name;

            var NameStatus = _context.Statuses.Where(stat => stat.Idstatuses == tasks.Idstatus).FirstOrDefault().Name;

            nitem.NameType = NameType;
            nitem.NameStatus = NameStatus;
            return View(nitem);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            var taskExt = new TasksExtended();
            //заполняем выпадающий список для создания
            var allTypes = _context.Types.ToList();
            foreach (var item in allTypes)
            {
                //добавляем типы в список

                taskExt.NameTypesAll.Add(item.Name);
            }
            //заполняем выпадающий список для создания
            var allStatuses = _context.Statuses.ToList();
            foreach (var item in allStatuses)
            {
                //добавляем статусы в список

                taskExt.NameStatusesAll.Add(item.Name);
            }
            return View(taskExt);
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TasksExtended tasks)
        {
            //проверим ИД 
            if (tasks.Id == 0)
            {
                //generate id
                int maxid = 0;

                foreach (var cat in _context.Tasks)
                {
                    if (cat.Id > maxid)
                    {
                        maxid = cat.Id;
                    }
                }
                tasks.Id = maxid + 1;
            }
            var taskOrd = new Tasks(tasks);

            //получаем имя выбранной  из модели TypesExtended
            var SelectedTypeName = tasks.NameTypesAll[0];
            //получаем саму категорию из базы по имени
            var Type = _context.Types.Where(c => c.Name == SelectedTypeName).FirstOrDefault();
            //получаем имя выбранной  из модели TypesExtended
            var SelectedSatatusName = tasks.NameStatusesAll[0];
            //получаем саму категорию из базы по имени
            var Status = _context.Statuses.Where(c => c.Name == SelectedSatatusName).FirstOrDefault();

            if (Type != null && Status!=null)
            {
                //сохраняем ИД 
                taskOrd.Idstatus = Status.Idstatuses;
                taskOrd.Idtype = Type.Idtypes;

                //Обновляем БД
                _context.Add(taskOrd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks.FindAsync(id);
            if (tasks == null)
            {
                return NotFound();
            }

            //получаем имя type которая задана в типе по ИД
            var NameStatus= _context.Statuses.Where(cat => cat.Idstatuses == tasks.Idstatus).First().Name;
            var NameType = _context.Types.Where(cat => cat.Idtypes == tasks.Idtype).First().Name;

            var tasksExt = new TasksExtended(tasks);

            //добавляем имя type в выпадающий список
            tasksExt.NameTypesAll.Add(NameType);
            var allCategories = _context.Types.ToList();
            foreach (var item in allCategories)
            {
                //добавляем остальные категории в список
                if (item.Idtypes != tasks.Idtype)
                    tasksExt.NameTypesAll.Add(item.Name);
            }
            tasksExt.NameType = NameType;
            //добавляем имя status в выпадающий список
            tasksExt.NameStatusesAll.Add(NameStatus);
            var allStats = _context.Statuses.ToList();
            foreach (var item in allStats)
            {
                //добавляем остальные категории в список
                if (item.Idstatuses != tasks.Idstatus)
                    tasksExt.NameStatusesAll.Add(item.Name);
            }
            tasksExt.NameStatus = NameStatus;
            return View(tasksExt);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TasksExtended tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var typeOrd = new Tasks(tasks);
                    //получаем имя выбранной type из модели TasksExtended
                    var SelectedTypeName = tasks.NameTypesAll[0];
                    //получаем имя выбранной status из модели TasksExtended
                    var SelectedStatusName = tasks.NameStatusesAll[0];
                    //получаем саму type из базы по имени
                    var Type = _context.Types.Where(c => c.Name == SelectedTypeName).FirstOrDefault();
                    //получаем саму status из базы по имени
                    var Status = _context.Statuses.Where(c => c.Name == SelectedStatusName).FirstOrDefault();
                    if (Type != null && Status != null)
                    {
                        //сохраняем ИД 
                        typeOrd.Idstatus = Status.Idstatuses;
                        typeOrd.Idtype = Type.Idtypes;
                        //Обновляем БД
                        _context.Update(typeOrd);
                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TasksExists(tasks.Id))
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
            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tasks = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tasks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
