using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoAPI.API.Models;//For access to the vm's (Data Transfer Objects)
using ToDoAPI.DATA.EF;
using System.Web.Http.Cors;//This allows us to modify CORS permissions to what we need in this app

namespace ToDoAPI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ToDoController : ApiController
    {
        ToDoEntities db = new ToDoEntities();
        public IHttpActionResult GetToDos()
        {
            List<ToDoViewModel> toDos = db.ToDoItems.Include("Categories").Select(t => new ToDoViewModel()
            {
                ToDoId = t.Todoid,
                Action = t.Action,
                Done = t.Done,
                CategoryId = t.CategoryId,
                Description = t.Description,
                Category = new CategoryViewModel()
                {
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category.Name,
                    CategoryDescription = t.Category.Description
                }
            }).ToList<ToDoViewModel>();

            if (toDos.Count == 0)
            {
                return NotFound();
            }
            return Ok(toDos);
        }//end GetToDos()

        public IHttpActionResult GetToDos(int id)
        {
            ToDoViewModel toDo = db.ToDoItems.Include("Categories").Where(t => t.CategoryId == id).Select(t => new ToDoViewModel()
            {

                ToDoId = t.Todoid,
                Action = t.Action,
                Done = t.Done,
                CategoryId = t.CategoryId,
                Description = t.Description,
                Category = new CategoryViewModel()
                {
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category.Name,
                    CategoryDescription = t.Category.Description
                }
            }).FirstOrDefault();

            if (toDo == null)
                return NotFound();

            return Ok(toDo);
        }//end GetToDos

        public IHttpActionResult PostToDos(ToDoViewModel toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }//end if

            ToDoItem newTodoItem = new ToDoItem()
            {
                Todoid = toDo.ToDoId,
                Action = toDo.Action,
                Done = toDo.Done,
                CategoryId = toDo.CategoryId,
                Description = toDo.Description
            };

            //add the record and save changes
            db.ToDoItems.Add(newTodoItem);
            db.SaveChanges();

            return Ok(newTodoItem);

        }//end PostResource

        public IHttpActionResult PutToDo(ToDoViewModel toDo)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            //Get the resource from the db so we can modify it
            ToDoItem existingResource = db.ToDoItems.Where(t => t.Todoid == toDo.ToDoId).FirstOrDefault();

            //modify the resource
            if (existingResource != null)
            {
                existingResource.Todoid = toDo.ToDoId;
                existingResource.Action = toDo.Action;
                existingResource.Done = toDo.Done;
                existingResource.CategoryId = toDo.CategoryId;
                existingResource.Description = toDo.Description;
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//end Put

        public IHttpActionResult DeleteResource(int id)
        {
            ToDoItem resource = db.ToDoItems.Where(t => t.Todoid == id).FirstOrDefault();

            if (resource != null)
            {
                db.ToDoItems.Remove(resource);
                db.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }//end Delete

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
