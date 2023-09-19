using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using ProyectoRedes.Models;
using System;
using System.Net;
using System.Numerics;

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
                //    WebClient webClient = new WebClient();
                //    webClient.QueryString.Add("name", getGame.name);
                //    webClient.QueryString.Add("status", getGame.status);
                //    webClient.QueryString.Add("page", getGame.page.ToString());
                //    webClient.QueryString.Add("limit", getGame.limit.ToString());
                //    string result = webClient.DownloadString("https://contaminados.meseguercr.com/api/games");

                //client.BaseAddress = new Uri();
                var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name="+getGame.name+"&status="+getGame.status);

                responseTask.Wait();

                var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                        {
                            //var data = JsonConvert.DeserializeObject<Data>(result);
                            var readTask = result.Content.ReadFromJsonAsync<Data>();
                            readTask.Wait();
                            
                            var data = readTask.Result;
                            ViewBag.response = data.ToJson();


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
        public ActionResult GetGameById(GetGameId getGame)
        {
            using (var client = new HttpClient())
            {
                //    WebClient webClient = new WebClient();
                //    webClient.QueryString.Add("name", getGame.name);
                //    webClient.QueryString.Add("status", getGame.status);
                //    webClient.QueryString.Add("page", getGame.page.ToString());
                //    webClient.QueryString.Add("limit", getGame.limit.ToString());
                //    string result = webClient.DownloadString("https://contaminados.meseguercr.com/api/games");

                //client.BaseAddress = new Uri();
                //var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name=" + getGame.gameId);

                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + getGame.gameId;

               

                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

                request.Headers.Add("password", getGame.password);

                request.Headers.Add("player", getGame.player);

               
                // Solicitud HTTP
                var responseTask = client.SendAsync(request);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var data = JsonConvert.DeserializeObject<Data>(result);
                    var readTask = result.Content.ReadFromJsonAsync<DataGet>();
                    readTask.Wait();
                    var data = readTask.Result;
                    ViewBag.response = data.ToJson();





                    return View();

                }
                else
                {
                    return View();
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
                //string baseUrl = "https://contaminados.meseguercr.com/api/games/" + getGame.gameId;

                //var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);

                var postTask = cliente.PostAsJsonAsync<CreateGame>("https://contaminados.meseguercr.com/api/games", game);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {


                    var readTask = result.Content.ReadFromJsonAsync<DataGet>();
                    readTask.Wait();
                    var data = readTask.Result;
                    ViewBag.message = data.ToJson();

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

        public ActionResult StartGame()
        {
            return View();
        }
        // POST: GameController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StartGame(GetGameId getGame)
        {
            using (var client = new HttpClient())
            {
                

                string baseUrl = "https://contaminados.meseguercr.com/api/games" + getGame.gameId;

                var request = new HttpRequestMessage(HttpMethod.Head, baseUrl);

                request.Headers.Add("password", getGame.password);

                request.Headers.Add("player", getGame.player);


                // Solicitud HTTP
                var responseTask = client.SendAsync(request);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var data = JsonConvert.DeserializeObject<Data>(result);
                   
                    ViewBag.response = "El juego ha comenzado!" + result.StatusCode;

                    return View();

                }
                else
                {
                    ViewBag.response = result.StatusCode;
                    return View();
                }


            }
        }
        public ActionResult GetRounds()
        {
            return View();

        }

        // GET
        [HttpPost]
        public ActionResult GetRounds(GetGameId getGame)
        {
            using (var client = new HttpClient())
            {
                //    WebClient webClient = new WebClient();
                //    webClient.QueryString.Add("name", getGame.name);
                //    webClient.QueryString.Add("status", getGame.status);
                //    webClient.QueryString.Add("page", getGame.page.ToString());
                //    webClient.QueryString.Add("limit", getGame.limit.ToString());
                //    string result = webClient.DownloadString("https://contaminados.meseguercr.com/api/games");

                //client.BaseAddress = new Uri();
                //var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name=" + getGame.gameId);

                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + getGame.gameId + "/rounds";



                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

                request.Headers.Add("password", getGame.password);

                request.Headers.Add("player", getGame.player);


                // Solicitud HTTP
                var responseTask = client.SendAsync(request);

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    //var data = JsonConvert.DeserializeObject<Data>(result);
                    var readTask = result.Content.ReadFromJsonAsync<GetRounds>();
                    readTask.Wait();
                    var data = readTask.Result;
                    ViewBag.response = data.ToJson();





                    return View();

                }
                else
                {
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
