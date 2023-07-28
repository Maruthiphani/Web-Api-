using Microsoft.AspNetCore.Mvc;
using ConsumeWebAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Reflection;
using System.Net;

namespace ConsumeWebAPI.Controllers
{
    public class ProductController : Controller
    {
       

        
        Uri baseAddress = new Uri("http://localhost:5234/api");
      
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Product/Get").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(baseAddress + "/Product/Post", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View();
        }


       
        public IActionResult Edit(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Product/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View("Create", model);
        }


        [HttpPost] 
        public IActionResult Edit(ProductViewModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PutAsync(baseAddress + "/Product/Put", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View("Create", model);
        }


        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = _client.DeleteAsync(baseAddress + "/Product/Delete/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public IActionResult Details(int id)
        {
            ProductViewModel model = new ProductViewModel();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "/Product/Get/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<ProductViewModel>(data);
            }
            return View(model);
        }





    }
}

