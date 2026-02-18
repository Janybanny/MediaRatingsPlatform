// See https://aka.ms/new-console-template for more information

using MediaRatingsPlatform;
using MediaRatingsPlatform.DataAccessLayer;
using MediaRatingsPlatform.PresentationLayer;

// Setup Database & Dependency Injection
var connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=mrpdb";
DatabaseSetup.EnsureTablesExist(connectionString);
var dependencies = new Dependencies(connectionString);

// Start Http Server
var server = new HttpServer("http://localhost:8080/api/", dependencies);
Console.WriteLine("Server setup");
new Task(server.StartServer).Start();
Console.WriteLine("Server started");
Console.ReadKey();
server.StopServer();
await Task.CompletedTask;
Console.WriteLine("Server stopped");
