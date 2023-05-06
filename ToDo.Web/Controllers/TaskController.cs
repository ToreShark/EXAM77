using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDo.BLL.uow;
using ToDo.DAL.Datas;
using ToDo.DAL.Entities;
using ToDo.DAL.Enum;


namespace ToDo.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public TaskController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Task
        public IActionResult Index()
        {
            var tasks = _unitOfWork.TaskRepository.GetAllTasks();
            return View(tasks);
        }

        // GET: Task/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _unitOfWork.TaskRepository.GetTask(id.Value);
            if (taskEntity == null)
            {
                return NotFound();
            }

            return View(taskEntity);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Task/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Priority,Action, ExpirationDate")] TaskEntity taskEntity)
        {
            if (ModelState.IsValid)
            {
                taskEntity.Status = Status.New; 
                _unitOfWork.TaskRepository.AddTask(taskEntity);
                return RedirectToAction(nameof(Index));
            }
            return View(taskEntity);
        }

        // GET: Task/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _unitOfWork.TaskRepository.GetTask(id.Value);
            if (taskEntity == null)
            {
                return NotFound();
            }
            return View(taskEntity);
        }

        // POST: Task/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Priority,Status,Action")] TaskEntity taskEntity)
        {
            if (id != taskEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.TaskRepository.UpdateTask(taskEntity);
                }
                catch
                {
                    if (!_unitOfWork.TaskRepository.Exists(taskEntity.Id))
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
            return View(taskEntity);
        }

        // GET: Task/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _unitOfWork.TaskRepository.GetTask(id.Value);
            if (taskEntity == null)
            {
                return NotFound();
            }

            return View(taskEntity);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var taskEntity = _unitOfWork.TaskRepository.GetTask(id);

            if (taskEntity == null)
            {
                return NotFound();
            }

            // Проверьте, не открыта ли задача перед удалением
            if (taskEntity.Status != Status.Open)
            {
                _unitOfWork.TaskRepository.DeleteTask(id);
            }

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Open(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _unitOfWork.TaskRepository.GetTask(id.Value);
            if (taskEntity == null)
            {
                return NotFound();
            }

            taskEntity.Status = Status.Open;
            _unitOfWork.TaskRepository.UpdateTask(taskEntity);

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult Close(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskEntity = _unitOfWork.TaskRepository.GetTask(id.Value);
            if (taskEntity == null)
            {
                return NotFound();
            }

            taskEntity.Status = Status.Closed;
            _unitOfWork.TaskRepository.UpdateTask(taskEntity);

            return RedirectToAction(nameof(Index));
        }
    }
}
