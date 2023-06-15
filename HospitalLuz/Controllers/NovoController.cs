using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HospitalLuz.Models;
using PagedList;
namespace HospitalLuz.Controllers
{
    public class NovoController : Controller
    {
        public ActionResult DetailCliente(int? id)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    Cliente este = db.Clientes.Find(id ?? 0);
                    if (este != null)
                    {
                        return View(este);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Cliente não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }



        public ActionResult DeleteCliente(int? id)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    Cliente este = db.Clientes.Find(id ?? 0);
                    if (este != null)
                    {
                        db.Clientes.Remove(este);
                        db.SaveChanges();
                        return RedirectToAction("Index", "Novo", new { msg = "Apagado com sucesso" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Del: id não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }

        }


        [HttpGet]
        public ActionResult EditCliente(int? id)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    Cliente este = db.Clientes.Find(id ?? 0);
                    if (este != null)
                    {
                        return View(este);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Id não existe" });
                    }
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }

        [HttpPost]
        public ActionResult EditCliente(Cliente editado, HttpPostedFileBase fich)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    Cliente este = db.Clientes.Find(editado.Idcli);
                    if (este != null)
                    {
                        este.nome = editado.nome;
                        este.datanasc = editado.datanasc;
                        if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                        {

                            string caminho = "~/fotos/" + este.Idcli.ToString() + System.IO.Path.GetExtension(fich.FileName);
                            if (System.IO.File.Exists(Server.MapPath(caminho))) System.IO.File.Delete(Server.MapPath(caminho));
                            este.fotopath = caminho;
                            fich.SaveAs(Server.MapPath(caminho));

                        }
                        db.SaveChanges();
                        return RedirectToAction("Index", "Novo", new { msg = "Ok Registo editado com sucesso" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Novo", new { msg = "Id não existe" });

                    }

                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }



        public ActionResult AddCliente()
        {
            try
            {
                Cliente novo = new Cliente();
                return View(novo);
            }
            catch (Exception erro)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult AddCliente(Cliente novo, HttpPostedFileBase fich)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    db.Clientes.Add(novo);
                    db.SaveChanges();
                    if (fich.ContentLength > 0 && fich.ContentType.Contains("image"))
                    {
                        string caminho = "~/fotos/" + novo.Idcli.ToString() + System.IO.Path.GetExtension(fich.FileName);
                        novo.fotopath = caminho;
                        fich.SaveAs(Server.MapPath(caminho));
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index", "Novo", new { msg = "Registo inserido com sucesso" });
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Novo", new { msg = erro.Message });
            }
        }


        public ActionResult Index(String msg, string ordem, string search, int? page)
        {
            ViewBag.msg = msg;
            int pagesize = 2;
            int pagina = page ?? 1;
            ViewBag.idcli = (String.IsNullOrEmpty(ordem)) ? "idclidesc" : "";
            ViewBag.nome = (ordem == "nomeasc") ? "nomedesc" : "nomeasc";
            ViewBag.ordem = ordem;
            ViewBag.pagina = pagina;
            using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
            {
                List<Cliente> clientes = db.Clientes.ToList();
               
                ViewBag.filtro = search;
                if (search != null)
                {
                    clientes = clientes.Where(x => x.nome.Contains(search)).ToList();
                }
                switch (ordem)
                {
                    case "idclidesc":
                        clientes = clientes.OrderByDescending(x => x.Idcli).ToList();
                        break;
                    case "nomeasc":
                        clientes = clientes.OrderBy(x => x.nome).ToList();
                        break;
                    case "nomedesc":
                        clientes = clientes.OrderByDescending(x => x.nome).ToList();
                        break;
                    default:
                        clientes = clientes.OrderBy(x => x.Idcli).ToList();
                        break;

                }
                return View(clientes.ToPagedList(pagina, pagesize));
            }

        }


    }
}