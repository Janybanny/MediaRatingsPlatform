// See https://aka.ms/new-console-template for more information

using MediaRatingsPlatform.DataAccessLayer;
using MediaRatingsPlatform.PresentationLayer;

// Setup Database
DatabaseCredentials.ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=mrpdb";
DatabaseSetup.EnsureTablesExist();

// Start Http Server
var server = new HttpServer("http://localhost:9080/api/");
Console.WriteLine("Server setup");
new Task(server.StartServer).Start();
Console.WriteLine("Server started");
Console.ReadKey();
server.StopServer();
await Task.CompletedTask;
Console.WriteLine("Server stopped");