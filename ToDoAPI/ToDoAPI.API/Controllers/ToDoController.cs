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

    }
}
