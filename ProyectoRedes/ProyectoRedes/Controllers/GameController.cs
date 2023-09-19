﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using ProyectoRedes.Models;
using System;
using System.Net;
using System.Numerics;
using System.Text;

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
                var responseTask = client.GetAsync("https://contaminados.meseguercr.com/api/games?name=" + getGame.name + "&status=" + getGame.status);

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


                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + getGame.gameId + "/start";

                var request = new HttpRequestMessage(HttpMethod.Head, baseUrl);

                request.Headers.Add("accept", "*/*");

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
        public ActionResult JoinGame()
        {
            return View();
        }

        // PUT : PlayerController/JoinGame
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinGame(JoinGame game)
        {
            using (var client = new HttpClient())
            {
                // Set the base address
                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + game.id;
                var requestBody = new
                {
                    player = game.player
                };

                // Serializar el objeto a JSON
                string jsonBody = JsonConvert.SerializeObject(requestBody);

                // Crear una solicitud HTTP POST con el cuerpo JSON
                var request = new HttpRequestMessage(HttpMethod.Put, baseUrl)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };

                request.Headers.Add("password", game.password);
                request.Headers.Add("player", game.player);


                // Solicitud HTTP
                var responseTask = client.SendAsync(request);
                // Send the GET request with headers and query parameter

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    // Handle the successful response here
                    var readTask = result.Content.ReadFromJsonAsync<DataGet>();
                    readTask.Wait();
                    var data = readTask.Result;

                    ViewBag.Message = data.ToJson();

                    return View();
                }
                else
                {
                    // Handle the error response here
                    return View();
                }
            }
        }

        public ActionResult ShowRounds()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowRounds(ShowRounds game)
        {
            using (var client = new HttpClient())
            {
                // Set the base address
                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + game.gameId + "/rounds/" + game.roundId;


                // Crear una solicitud HTTP POST con el cuerpo JSON
                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

                request.Headers.Add("password", game.password);
                request.Headers.Add("player", game.player);


                // Solicitud HTTP
                var responseTask = client.SendAsync(request);
                // Send the GET request with headers and query parameter

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    // Handle the successful response here
                    var readTask = result.Content.ReadFromJsonAsync<DataRounds>();
                    readTask.Wait();
                    var data = readTask.Result;

                    ViewBag.Message = data.ToJson();

                    return View();
                }
                else
                {
                    // Handle the error response here
                    return View();
                }
            }
        }


        public ActionResult ProposeGroup()
        {
            return View();
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]
        public ActionResult ProposeGroup(ProposeGroup game)
        {
            using (var client = new HttpClient())
            {
                // Set the base address
                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + game.gameId + "/rounds/" + game.roundId;

                if (game.group != null)
                {
                    var requestBody = new
                    {
                        group = game.group.ToList()
                    };


                    // Serializar el objeto a JSON
                    string jsonBody = JsonConvert.SerializeObject(requestBody);

                    // Crear una solicitud HTTP POST con el cuerpo JSON
                    var request = new HttpRequestMessage(HttpMethod.Patch, baseUrl)
                    {
                        Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                    };

                    request.Headers.Add("password", game.password);
                    request.Headers.Add("player", game.player);


                    // Solicitud HTTP
                    var responseTask = client.SendAsync(request);
                    // Send the GET request with headers and query parameter

                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        // Handle the successful response here
                        var readTask = result.Content.ReadFromJsonAsync<GroupData>();
                        readTask.Wait();
                        var data = readTask.Result;

                        ViewBag.Message = data.ToJson();

                        return View();
                    }
                    else
                    {
                        // Handle the error response here
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
        }

        public ActionResult Vote()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vote(VoteG game)
        {
            using (var client = new HttpClient())
            {
                // Set the base address
                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + game.gameId + "/rounds/" + game.roundId;


                var requestBody = new
                {
                    vote = game.vote
                };


                // Serializar el objeto a JSON
                string jsonBody = JsonConvert.SerializeObject(requestBody);

                // Crear una solicitud HTTP POST con el cuerpo JSON
                var request = new HttpRequestMessage(HttpMethod.Put, baseUrl)
                {
                    Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
                };

                request.Headers.Add("password", game.password);
                request.Headers.Add("player", game.player);


                // Solicitud HTTP
                var responseTask = client.SendAsync(request);
                // Send the GET request with headers and query parameter

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    // Handle the successful response here
                    var readTask = result.Content.ReadFromJsonAsync<GroupData>();
                    readTask.Wait();
                    var data = readTask.Result;

                    ViewBag.Message = data.ToJson();

                    return View();
                }
                else
                {
                    ViewBag.Message = result.Content.ToJson();
                    // Handle the error response here
                    return View();
                }
            }

        }
    

    public ActionResult Action()
    {
        return View();
    }

    [HttpPatch]
    [ValidateAntiForgeryToken]
    public ActionResult Action(ActionData game)
    {
        using (var client = new HttpClient())
        {
            // Set the base address
            string baseUrl = "https://contaminados.meseguercr.com/api/games/" + game.gameId + "/rounds/" + game.roundId;


            var requestBody = new
            {
                action = game.action
            };


            // Serializar el objeto a JSON
            string jsonBody = JsonConvert.SerializeObject(requestBody);

            // Crear una solicitud HTTP POST con el cuerpo JSON
            var request = new HttpRequestMessage(HttpMethod.Post, baseUrl)
            {
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("password", game.password);
            request.Headers.Add("player", game.player);


            // Solicitud HTTP
            var responseTask = client.SendAsync(request);
            // Send the GET request with headers and query parameter

            responseTask.Wait();

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                // Handle the successful response here
                var readTask = result.Content.ReadFromJsonAsync<GroupData>();
                readTask.Wait();
                var data = readTask.Result;

                ViewBag.Message = data.ToJson();

                return View();
            }
            else
            {
                ViewBag.Message = result.Content.ToJson();
                // Handle the error response here
                return View();
            }
        }

    }
}

    
}
