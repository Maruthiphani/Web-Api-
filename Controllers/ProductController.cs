using Microsoft.AspNetCore.Mvc;
using ConsumeWebAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Reflection;
using System.Net;
using Microsoft.AspNetCore.Cors;

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
            //string modifiedData = data.Replace("null", "\"\"");
            string modifiedJson = data.Replace("\"ModifiedBy\":null", "\"ModifiedBy\":\"\"");
            StringContent content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(baseAddress + "/Product/Post", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorMessage = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
                }
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
            else
            {
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string errorMessage = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        ModelState.AddModelError(string.Empty, errorMessage);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
                }
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

