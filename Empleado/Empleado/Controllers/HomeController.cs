using Empleado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empleado.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetEmpleado()
        {
            using (DatabaseEmpleadosEntities1 dc = new DatabaseEmpleadosEntities1())
            {
                var Empelado = dc.Empleados.OrderBy(a => a.Nombre).ToList();
                return Json(new { data = Empelado }, JsonRequestBehavior.AllowGet); 
                
            }

        }

        [HttpGet]
        public ActionResult Save(int id) {

            using (DatabaseEmpleadosEntities1 dc = new DatabaseEmpleadosEntities1())
            {
                var v = dc.Empleados.Where(a => a.Clave_Emp == id).FirstOrDefault();
                return View(v);
            }
          
        }

        [HttpPost]
        public ActionResult save(Empleados emp) {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (DatabaseEmpleadosEntities1 dc = new DatabaseEmpleadosEntities1())
                {
                    if (emp.Clave_Emp > 0)
                    {
                        //Edit
                        var v = dc.Empleados.Where(a => a.Clave_Emp == emp.Clave_Emp).FirstOrDefault();
                        if (v != null)
                        {
                            v.Nombre = emp.Nombre;
                            v.ApPaterno = emp.ApPaterno;
                            v.ApMaterno = emp.ApMaterno;
                            v.FecNac = emp.FecNac;
                            v.Departamento = emp.Departamento;
                            v.sueldo = emp.sueldo;
                        }
                    }
                    else
                    {
                        int valor = 0;
                        valor = dc.Empleados.Max(a => a.Clave_Emp);
                        emp.Clave_Emp = valor + 1;
                        //save 
                        dc.Empleados.Add(emp);
                    }

                    dc.SaveChanges();
                    status = true;

                }
            }
           // return new JsonResult { Data = new { stauts = status } };
           return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Delete(int id) {
            using (DatabaseEmpleadosEntities1 dc = new DatabaseEmpleadosEntities1())
            {
                var v = dc.Empleados.Where(a => a.Clave_Emp == id).FirstOrDefault();

                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmpleado(int id) {

            bool status = false;
            using (DatabaseEmpleadosEntities1 dc = new DatabaseEmpleadosEntities1())
            {
                var V = dc.Empleados.Where(a => a.Clave_Emp == id).FirstOrDefault();
                if ( V != null )
                {
                    dc.Empleados.Remove(V);
                    dc.SaveChanges();
                    status = true; 
                }
            }
            return RedirectToAction("Index");
            //return new JsonResult { Data = new { status = status } }; 
        }
    }
}