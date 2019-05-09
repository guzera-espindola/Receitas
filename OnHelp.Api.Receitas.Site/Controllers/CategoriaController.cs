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
    public class CategoriaController : Controller
    {
        public ActionResult GetAllCategorias()
        {
            try
            {
                HttpClient Client = new HttpClient();
                Client.BaseAddress = new Uri("https://onhelpapireceitas.azurewebsites.net/");
                HttpResponseMessage response = Client.GetAsync("api/categoria").Result;
                response.EnsureSuccessStatusCode();
                string result = response.Content.ReadAsStringAsync().Result;
                IEnumerable<Models.Categoria> categ = JsonConvert.DeserializeObject<List<Models.Categoria>>(result);
                ViewBag.Title = "All Categorias";
                return View(categ);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult EditCategoria(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("API/categoria?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Categoria categ = response.Content.ReadAsAsync<Models.Categoria>().Result;
            ViewBag.Title = "All Receitas";
            return View(categ);
        }

        public ActionResult Update(Models.Categoria categoria)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PutResponse("API/categoria", categoria);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCategorias");
        }

        public ActionResult DetailsCategoria(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.GetResponse("API/categoria?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            Models.Categoria categ = response.Content.ReadAsAsync<Models.Categoria>().Result;
            ViewBag.Title = "All Categorias";
            return View(categ);
        }

        [HttpGet]
        public ActionResult CreateCategoria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategoria(Models.Categoria categoria)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.PostResponse("API/categoria", categoria);
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCategorias");
        }
        public ActionResult Delete(int id)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            HttpResponseMessage response = serviceObj.DeleteResponse("API/categoria?id=" + id.ToString());
            response.EnsureSuccessStatusCode();
            return RedirectToAction("GetAllCategorias");
        }

    }
}