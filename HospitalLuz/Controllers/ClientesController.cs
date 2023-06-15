using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalLuz.Models;
namespace HospitalLuz.Controllers
{

    public class ClientesController : Controller
    {
        // GET: Clientes
        List<Aluno> bd()
        {
            List<Aluno> alunos = new List<Aluno>() {
                new Aluno{Num=1,Nome="Tio  Patinhas"},
                  new Aluno{Num=2,Nome="Pato Donald"},
                    new Aluno{Num=3,Nome="Pateta"},
                      new Aluno{Num=4,Nome="Peninha"},
                        new Aluno{Num=5,Nome="Pluto"},
                          new Aluno{Num=6,Nome="Margarida"}
            };
            return alunos;
        }
       
        [HttpGet]
        public ActionResult HomeClientes()
        {

            ViewBag.escola = "ISTEC";
            ViewData["disciplina"] = "Tecnologias de Internet III";
            TempData["professor"] = "José Neves";
            List<Aluno> turma = bd();

            return View(turma);
        }
      
        [HttpPost]
        public ActionResult HomeClientes(int? favorito) {
           List<Aluno> turma = bd();
            ViewBag.escolhido = turma.Where(x => x.Num == ((int)(favorito ?? 1))).FirstOrDefault().Nome;
            return View(turma);
           
        }



    }
}