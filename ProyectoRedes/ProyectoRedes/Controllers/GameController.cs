using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoRedes.Models;
using System;

namespace ProyectoRedes.Controllers
{
    public class GameController : Controller
    {
        // GET: GameController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GameController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        // GET: GameController/Create
        public ActionResult GetGame()
        {
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri();
                var responseTask = client.GetAsync("https://virtserver.swaggerhub.com/UCR-SA/contaminaDOS/1.0.0/api/games?name=Game1&status=lobby&page=0&limit=50");

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Data>();
                    readTask.Wait();

                    return View();

                }
                else
                {
                    return View(null);
                }

                

            }
            
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: GameController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGame game)
        {
            using (var cliente = new HttpClient())
            {
                //cliente.BaseAddress = new Uri("https://virtserver.swaggerhub.com/UCR-SA/contaminaDOS/1.0.0");
                var postTask = cliente.PostAsJsonAsync<CreateGame>("https://virtserver.swaggerhub.com/UCR-SA/contaminaDOS/1.0.0/api/games", game);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.message = result.Content.ReadAsStreamAsync();
                    return View(); ;
                }
                else
                {
                    // Manejo de errores aquí, por ejemplo, puedes registrar el código de estado y el mensaje de error.
                    ViewBag.message = $"Error: {postTask.Status}";
                    return View();
                }
            }
        }

        // GET: GameController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
