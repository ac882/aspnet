using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using HospitalLuz.Models;
namespace HospitalLuz.Controllers
{
    [Authorize]
    public class CarrosController : Controller
    {
        // GET: Carros
        public ActionResult Index(String msg)
        {
            using(StandTintIII_MBEntities db= new StandTintIII_MBEntities())
            {
                ViewBag.msg = msg;
                List<carro> carros = db.carros.ToList();
                return View(carros);
            }
           
        }

        public ActionResult CreateCarro()
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    List<marca> marcas = db.marcas.ToList();
                    ViewBag.marcas = new SelectList(marcas, "marca1", "marca1");
                    carro novo= new carro();
                    return View(novo);
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Carros", new { msg = erro.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateCarro(carro novo, string xpto,HttpPostedFileBase fich)
        {
            try
            {
                decimal pu;
                xpto = xpto.Replace(".", ",");
                if (decimal.TryParse(xpto.ToString(), out pu))
                {
                    novo.pu = pu;
                }
                else {
                    novo.pu = 0;

                }
                using(StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    db.carros.Add(novo);
                    if (fich != null && fich.ContentLength > 0 && fich.ContentType.Contains("image")) {
                        System.Drawing.Image imagem = System.Drawing.Image.FromStream(fich.InputStream);
                        System.IO.MemoryStream memo = new System.IO.MemoryStream();
                        imagem.Save(memo, System.Drawing.Imaging.ImageFormat.Png);
                        novo.foto = memo.ToArray();
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index", "Carros", new { msg = "ok" });
                }
            }
            catch (Exception erro)
            {

                return RedirectToAction("Index", "Carros", new { msg = erro.Message });
            }
        }


        public ActionResult DeleteCarro(int? id)
        {
            try
            {
                using (StandTintIII_MBEntities db = new StandTintIII_MBEntities())
                {
                    carro este = db.carros.Find(id ?? 0);
                    if (este != null)
                    {
                        db.carros.Remove(este);
                        db.SaveChanges();
                        return Json(new { msg = "deleted" }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        return Json(new { msg = "erro" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception)
            {
                return Json(new { msg = "Registo não pode ser apagado" }, JsonRequestBehavior.AllowGet);

            }
        }

    }
}