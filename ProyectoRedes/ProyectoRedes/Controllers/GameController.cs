using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoRedes.Models;
using System;
using System.Net;

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


        public ActionResult GetGame()
        {
            return View();

        }

        // GET
        [HttpPost]
        public ActionResult GetGame(GetGame getGame)
        {
            using (var client = new HttpClient())
                {
                //    string result = webClient.DownloadString("https://contaminados.meseguercr.com/api/games");

                var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name="+getGame.name+"&status="+getGame.status);

                responseTask.Wait();

                var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                            //var data = JsonConvert.DeserializeObject<Data>(result);
                            var readTask = result.Content.ReadFromJsonAsync<Data>();
                            readTask.Wait();
                            var data = readTask.Result;
                            

                            return View();

                    }
                    else
                    {
                        return View();
                    }

            }
            
        }
         //Get game by id.
        public ActionResult GetGameById()
        {
            return View();

        }

        // GET
        [HttpPost]
        public ActionResult GetGameById(GetGame getGame)
        {
            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name=" + getGame.name);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var data = JsonConvert.DeserializeObject<Data>(result);
                    var readTask = result.Content.ReadFromJsonAsync<Data>();
                    readTask.Wait();
                    var data = readTask.Result;
                    return View();

                }
                else
                {
                    return View();
                }



            }

        }
        [HttpPost]
        public ActionResult Welcome(IFormCollection form)
        {
            string username = form["username"]; // Obtiene el valor del campo "username" del formulario

            TempData["Username"] = username;

            return RedirectToAction("Create"); // Redirige a la vista "Create" para mostrar el mensaje de bienvenida
        }


        public ActionResult Lobby()
        {
            return View();
        }

        public ActionResult Create(IFormCollection form)
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
                var postTask = cliente.PostAsJsonAsync<CreateGame>("https://contaminados.meseguercr.com/api/games", game);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.message = result.Content.ReadAsStreamAsync();
                    return View("Lobby");
                }
                else
                {
                    // Manejo de errores aquí, por ejemplo, puedes registrar el código de estado y el mensaje de error.
                    // En este caso como siempre es un error 400 se responde con el siguiente mensaje
                    ViewBag.message = "The server cannot or will not process the request due to something that is perceived to be a client error";
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
