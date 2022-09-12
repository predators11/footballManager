namespace FootballManager
{
    using BasicWebServer.Server;
    using BasicWebServer.Server.Routing;
    using FootballManager.Contracts;
    using FootballManager.Data;
    using FootballManager.Services;
    using System.Threading.Tasks;

    public class StartUp
    {
        public static async Task Main()
        {
            var server = new HttpServer(routes => routes
               .MapControllers()
               .MapStaticFiles());

            server.ServiceCollection
                .Add<FootballManagerDbContext>()
                .Add<IUserService, UserService>()
                .Add<IPlayersService, PlayersService>();

            await server.Start();
        }
    }
}
