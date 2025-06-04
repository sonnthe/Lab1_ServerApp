using System;
using System.Net;
using System.Net.Sockets;
using DataAccessLayer;
using Lab1_ServerApp.Handlers;
using Lab1_ServerApp;
using Microsoft.Extensions.DependencyInjection;
using Services;
using BusinessObjects;

public class Program
{
    private static int numberOfClient = 0;
    private static readonly List<IRequestHandler> handlers =
[
    new CourseListHandler(),
    new LoadSchedulesHandler(),
    new LoadStudentHandler(),
    new UpdateAttendanceHandler(),
];
    public static void Main(string[] args)
    {
        SetUpDependency();
        StartServer();
       
    }

    private static void SetUpDependency()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();
        DIContainer.ServiceProvider = serviceProvider;
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        services.AddSingleton<IStudentService, StudentService>();
        services.AddSingleton<IStudentRepository, StudentFromSqlServer>();
        services.AddSingleton<IStudentScheduleRepository, StudentScheduleFromSqlServer>();
        services.AddSingleton<IStudentScheduleService, StudentScheduleService>();
        services.AddSingleton<ICourseService, CourseService>();
        services.AddSingleton<ICourseRepository, CourseFromSqlServer>();
        services.AddSingleton<IScheduleService, ScheduleService>();
        services.AddSingleton<IScheduleRepository, ScheduleFromSqlServer>();

    }


    private static void StartServer()
    {
        string host = "127.0.0.1";
        int port = 1500;
        Console.WriteLine("Server App");
        IPAddress localAddr = IPAddress.Parse(host);
        TcpListener server = new(localAddr, port);
        server.Start();

        Console.WriteLine("************************");
        Console.WriteLine("waiting....");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.Write("*************************");
            Console.WriteLine($"Number of client connected: {++numberOfClient}");
            Thread thread = new(new ParameterizedThreadStart(SendDataToClient));
            thread.Start(client);

        }

    }

    private static void SendDataToClient(object parameter)
    {
        TcpClient client = (TcpClient)parameter;
        string data;
        string response = String.Empty;
        int count;
        NetworkStream stream = client.GetStream();
        Byte[] bytes = new Byte[1024];
        try
        {
            while ((count = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                data = System.Text.Encoding.ASCII.GetString(bytes, 0, count);
                Console.WriteLine($"Received: {data} at {DateTime.Now}");

                foreach (var handler in handlers)
                {
                    if (handler.CanHandle(data))
                    {
                        response = handler.Handle(data);
                        break;
                    }
                }


                Byte[] msg = System.Text.Encoding.ASCII.GetBytes(response);
                stream.Write(msg, 0, msg.Length);
            }

        }
        catch (IOException e)
        { Console.WriteLine(e.Message); }

        client.Close();
        Console.WriteLine($"Number of client connected: {--numberOfClient}");

    }
}


