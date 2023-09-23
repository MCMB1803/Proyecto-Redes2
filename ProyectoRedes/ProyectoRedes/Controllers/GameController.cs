using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Protocol;
using ProyectoRedes.Models;
using ProyectoRedes.Models.Global;
using System;
using System.Net;
using System.Numerics;
using System.Text;

namespace ProyectoRedes.Controllers
{
    public class GameController : Controller
    {

        public Globals Globals = new Globals();
        public GlobalData GlobalData = new GlobalData();

        // GET: GameController

        public ActionResult Index(
            List<string> players, 
            List<string> enemies, 
            string leader, 
            string result, 
            string phase, 
            string status, 
            List<string> group, 
            List<bool> votes)
        {
            //return RedirectToAction(nameof(Index),
              //          new { players, enemies, resp.leader, resp.result, resp.phase, resp.status, resp.group, resp.votes });
            var globalPlayers = Globals.players;
            var globalEnemies = Globals.enemies;
            //playersGame.playersIn = players;

            if (players.Count > 0)
            {
                if (enemies.Count > 0)
                {

                    var player = new Player();
                    var others = new List<PlayerCheck>();
                    var delete = new List<string>();
                    delete = players;
                    foreach (var p in players)
                    {

                        foreach (var enemy in enemies)
                        {
                            if (enemy == p)
                            {
                                var other = new PlayerCheck();
                                other.name = enemy;
                                other.isEnemy = true;
                                others.Add(other);

                            }

                        }

                    }


                    foreach (var enemy in others)
                    {
                        delete.Remove(enemy.name);

                    }

                    foreach (var p in players)
                    {
                        var other = new PlayerCheck();
                        other.name = p;
                        other.isEnemy = false;
                        others.Add(other);
                    }


                    player.player = Globals.playerName;
                    player.otherPlayers = others;
                    if(leader != null && 
                        result != null &&
                         phase != null &&
                         status != null &&
                         group != null &&
                         votes != null){

                        player.leader = leader;
                        player.group = group;
                        player.status = status;
                        player.votes = votes;

                    }
                    return View(player);



                }
                else
                {
                    var player = new Player();
                    var otherPlayers = new List<PlayerCheck>();
                    foreach (var p in players)
                    {
                        var other = new PlayerCheck();
                        other.name = p;
                        other.isEnemy = false;
                        otherPlayers.Add(other);
                    }
                    player.player = Globals.playerName;
                    player.otherPlayers = otherPlayers;
                    if (leader != null &&
                       result != null &&
                        phase != null &&
                        status != null &&
                        group != null &&
                        votes != null)
                    {

                        player.leader = leader;
                        player.group = group;
                        player.status = status;
                        player.votes = votes;

                    }
                    return View(player);
                }


            }
            else if (globalPlayers != null)
            {
                if (globalEnemies.Count > 0)
                {

                    var player = new Player();
                    var others = new List<PlayerCheck>();
                    var delete = new List<string>(globalPlayers);
                    foreach (var p in globalPlayers)
                    {

                        foreach (var enemy in globalEnemies)
                        {
                            if (enemy == p)
                            {
                                var other = new PlayerCheck();
                                other.name = enemy;
                                other.isEnemy = true;
                                others.Add(other);

                            }

                        }

                    }


                    foreach (var enemy in others)
                    {
                        delete.Remove(enemy.name);

                    }

                    foreach (var p in delete)
                    {
                        var other = new PlayerCheck();
                        other.name = p;
                        other.isEnemy = false;
                        others.Add(other);
                    }


                    player.player = Globals.playerName;
                    player.otherPlayers = others;
                    if (Globals.leader != null &&
                       Globals.result != null &&
                        Globals.phase != null &&
                        Globals.status != null &&
                        Globals.group != null &&
                        Globals.votes != null)
                    {

                        player.leader = Globals.leader;
                        player.group = Globals.group;
                        player.status = Globals.status;
                        player.votes = Globals.votes;

                    }

                    return View(player);



                }
                else
                {
                    var player = new Player();
                    var otherPlayers = new List<PlayerCheck>();
                    foreach (var p in globalPlayers)
                    {
                        var other = new PlayerCheck();
                        other.name = p;
                        other.isEnemy = false;
                        otherPlayers.Add(other);
                    }
                    player.player = Globals.playerName;
                    player.otherPlayers = otherPlayers;
                    if (Globals.leader != null &&
                       Globals.result != null &&
                        Globals.phase != null &&
                        Globals.status != null &&
                        Globals.group != null &&
                        Globals.votes != null)
                    {

                        player.leader = Globals.leader;
                        player.group = Globals.group;
                        player.status = Globals.status;
                        player.votes = Globals.votes;

                    }
                    return View(player);
                }
            }
            else { return View(); }
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

                    //var players = data.data.ToList();


                    return View();




                }
                else
                {
                    return View();
                }



            }

        }
        public void ShowRoundsMethod(IndexReturn resp)
        {
            using (var client = new HttpClient())
            {
                // Set the base address
                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + Globals.gameId + "/rounds/" + Globals.roundId;


                // Crear una solicitud HTTP POST con el cuerpo JSON
                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

                request.Headers.Add("password", Globals.password);
                request.Headers.Add("player", Globals.playerName);


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
                    var leader = data.data.leader;
                    var status = data.data.status;  
                    var resultRound = data.data.result;  
                    var phase = data.data.phase;
                    var group = data.data.group;
                    var votes = data.data.votes;

                    resp.phase = phase;
                    resp.group = group; 
                    resp.result = resultRound;
                    resp.status = status;
                    resp.leader = leader;
                    resp.votes = votes;

                    Globals.votes = resp.votes;
                    Globals.phase = resp.phase;
                    Globals.group = resp.group;
                    Globals.result = resp.result;
                    Globals.status = resp.status;
                    Globals.leader = resp.leader;

                    ViewBag.Message = data.ToJson();

                }
                else
                {
                    // Handle the error response here

                }
            }
        }
        public ActionResult GetGameMethod()
        {
            using (var client = new HttpClient())
            {
                //    WebClient webClient = new WebClient();


                string baseUrl = "https://contaminados.meseguercr.com/api/games/" + Globals.gameId;



                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);

                request.Headers.Add("password", Globals.password);

                request.Headers.Add("player", Globals.playerName);


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

                    var players = data.data.players.ToList();
                    var enemies = data.data.enemies.ToList();
                    var roundId = data.data.currentRound;


                    Globals.enemies = enemies;
                    Globals.players = players;
                    Globals.roundId = roundId;

                    IndexReturn resp = new IndexReturn();

                    ShowRoundsMethod(resp);
         

                    return RedirectToAction(nameof(Index), 
                        new {players, enemies, resp.leader, resp.result, resp.phase, resp.status, resp.group, resp.votes });



                }
                else
                {
                    return RedirectToAction(nameof(Index));
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

                    var players = data.data.players.ToList();
                    var enemies = data.data.enemies.ToList();
                    
                    Globals.enemies = enemies;
                    Globals.players = players;


                    return RedirectToAction(nameof(Index), new { players, enemies });


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

                    Globals.playerName = game.owner;
                    Globals.password=game.password;
                   
                    var readTask = result.Content.ReadFromJsonAsync<DataGet>();
                    readTask.Wait();
                    var data = readTask.Result;
                    ViewBag.message = data.ToJson();
                    GlobalData.success1 = true;
                    GlobalData.success2 = true;
                    Globals.gameId = data.data.id;

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
                    GlobalData.success3 = true;
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
                Globals.playerName = game.player;
                Globals.password = game.password;
                Globals.gameId = game.id;

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

                    var players = data.data.players.ToList();
                    var enemies = data.data.enemies.ToList();
                    var name = game.player;
                    Globals.enemies = enemies;
                    Globals.players = players;
                    Globals.roundId = data.data.currentRound;
                    GlobalData.success = true;
                    GlobalData.success2 = true;

                    IndexReturn resp = new IndexReturn();

                    return RedirectToAction(nameof(Index),
                        new { players, enemies});
                }
                else
                {
                    ViewBag.Message = result.Content.ReadAsStringAsync();
                    // Handle the error response here
                    return View();
                }
            }
        }
        public ActionResult Options()
        {
            return View(GlobalData);
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
