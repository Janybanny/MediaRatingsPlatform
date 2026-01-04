// See https://aka.ms/new-console-template for more information

using MediaRatingsPlatform.PresentationLayer;
using MediaRatingsPlatform.DataAccessLayer;

// Setup Database

//DatabaseSetup dbsetup = new Setup();
string _databaseConnection = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=mrpdb";

// Start Http Server
HttpServer server = new HttpServer("http://localhost:9080/api/");
Console.WriteLine("Server setup");
new Task(server.StartServer).Start();
Console.WriteLine("Server started");
Console.ReadKey();
server.StopServer();
await Task.CompletedTask;
Console.WriteLine("Server stopped");
