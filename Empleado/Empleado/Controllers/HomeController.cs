using Empleado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Empleado.Controllers;

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
            using (lunarytix_1Entities dc = new lunarytix_1Entities())
            {

                var Empelado = dc.Empleados.OrderBy(a => a.Nombre).ToList();
                return Json(new { data = Empelado }, JsonRequestBehavior.AllowGet);

            }

        }

        public ActionResult GetDepartamento()
        {
            using (lunarytix_1Entities dc = new lunarytix_1Entities())
            {
                var Departamento = dc.Departamentos.OrderBy(a => a.Departamento).ToList();
                return Json(new { data = Departamento }, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpGet]
        public ActionResult Save(int id)
        {

            Empleados dp = new Empleados();

            if (id != 0) {
                using (lunarytix_1Entities dc = new lunarytix_1Entities())
                {

                    dp = dc.Empleados.Where(a => a.Clave_Emp == id).FirstOrDefault();

                }

            }
            else { 
                using (lunarytix_1Entities dc = new lunarytix_1Entities())
                {

                    var DepartamentoEmp = dc.Departamentos.OrderBy(a => a.Departamento).ToList();
                    dp.EmpleadosDepartamentos = DepartamentoEmp.ToList<Departamentos>();
                }

            }

            return View(dp);


        }

        [HttpPost]
        public ActionResult save(Empleados emp)
        {
            bool status = false;
            if (ModelState.IsValid)
            {
                using (lunarytix_1Entities dc = new lunarytix_1Entities())
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
        public ActionResult Delete(int id)
        {
            using (lunarytix_1Entities dc = new lunarytix_1Entities())
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
        public ActionResult DeleteEmpleado(int id)
        {

            bool status = false;
            using (lunarytix_1Entities dc = new lunarytix_1Entities())
            {
                var V = dc.Empleados.Where(a => a.Clave_Emp == id).FirstOrDefault();
                if (V != null)
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