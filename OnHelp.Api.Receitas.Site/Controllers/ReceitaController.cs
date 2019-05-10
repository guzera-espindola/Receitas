using Newtonsoft.Json;
using OnHelp.Api.Receitas.Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace OnHelp.Api.Receitas.Site.Controllers
{
    public class ReceitaController : Controller
    {
        public ActionResult GetAllReceitas()
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.GetResponse("api/receita");
                response.EnsureSuccessStatusCode();
                List<Models.Receita> rec = response.Content.ReadAsAsync<List<Models.Receita>>().Result;
                ViewBag.Title = "All Receitas";
                return View(rec);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EditReceita(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/receita?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Receita rec = response.Content.ReadAsAsync<Models.Receita>().Result;
            ViewBag.Title = "All Receitas";


            HttpResponseMessage response2 = serviceObj.GetResponse("API/categoria");
            response.EnsureSuccessStatusCode();
            List<Models.Categoria> categ = response2.Content.ReadAsAsync<List<Models.Categoria>>().Result;
            ViewBag.CategoriasList = categ.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            return View(rec);
        }

        public ActionResult Update(Models.Receita receita)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("api/receita", receita);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllReceitas");
        }

        public ActionResult DetailsReceita(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("api/receita?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Receita rec = response.Content.ReadAsAsync<Models.Receita>().Result;
            ViewBag.Title = "All Receitas";
            return View(rec);
        }

        [HttpGet]
        public ActionResult CreateReceita()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("API/categoria");
            response.EnsureSuccessStatusCode();
            List<Models.Categoria> categ = response.Content.ReadAsAsync<List<Models.Categoria>>().Result;
            ViewBag.CategoriasList = categ.Select(x => new SelectListItem { Text = x.Title, Value = x.Id.ToString() }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateReceita(Models.Receita receita)
        {
            var x = Request["CategoriaId"];
            receita.Categoria = new Categoria();
            receita.Categoria.Id = Convert.ToInt32(x);
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PostResponse("api/receita", receita);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllReceitas");
        }

        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("api/receita?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllReceitas");
        }
    }
}
