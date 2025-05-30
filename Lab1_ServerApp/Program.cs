using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Nodes;
using AutoMapper;
using BusinessObjects.DTOs;
using BusinessObjects.Models;
using BusinessObjects.MyProfile;
using DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;
using Services;
using static System.Reflection.Metadata.BlobBuilder;

public class Program
{
    public static IServiceProvider? ServiceProvider { get; private set; }
    private static ServiceCollection? services;
    private static int numberOfClient = 0;

    private static IStudentService? _studentService;
    private static ICourseService? _courseService;
    private static  IScheduleService? _scheduleService;
    private static IStudentScheduleService? _studentScheduleService;
    public static void Main(string[] args)
    {
        SetUpDependency();
        StartServer();
       
    }

    private static void SetUpDependency()
    {
        services = new ServiceCollection();

        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();
        DIContainer.ServiceProvider = ServiceProvider;
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
        TcpListener server = new TcpListener(localAddr, port);
        server.Start();

        Console.WriteLine("************************");
        Console.WriteLine("waiting....");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Console.Write("*************************");
            Console.WriteLine($"Number of client connected: {++numberOfClient}");
            Thread thread = new Thread(new ParameterizedThreadStart(SendDataToClient));
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

                if (data.Equals("CourseList"))
                {
                    response = GetCourseList();
                }
                else if (data.StartsWith("LoadSchedulesBySelectedCourse:"))
                {
                    int courseId = int.Parse(data.Split(':')[1]);
                    Console.WriteLine($"Loading schedules for course ID: {courseId}");
                    response = LoadSchedulesBySelectedCourse(courseId);
                }else if(data.StartsWith("LoadStudentsByCIdAndSId:"))
                {
                    var parts = data.Split(':');
                    int courseId = int.Parse(parts[1]);
                    int scheduleId = int.Parse(parts[2]);
                    Console.WriteLine($"Loading students for course ID: {courseId}, schedule ID: {scheduleId}");
                    response = LoadStudentsBySelectedCourseAndScheduleId(courseId, scheduleId);
                }
                else if (data.StartsWith("UpdateAttendance:"))
                {
                  var parts =data.Split(':');
                  string studentIdListStr = parts[1];
                    List<int> studentIdList = JsonSerializer.Deserialize<List<int>>(studentIdListStr) ?? new List<int>();
                    int scheduleIdStr = int.Parse(parts[2]);
                    response = UpdateAttendance(studentIdList, scheduleIdStr) ? "Successful" : "Failed";
                }
                else
                {
                    response = "Invalid command.";
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

    private static String GetCourseList()
    {

        try
        {
            _courseService = ServiceProvider?.GetService<ICourseService>();
            var courseList = _courseService!.GetCourses();
            if (courseList != null)
            {
                return System.Text.Json.JsonSerializer.Serialize(courseList);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading courses: {ex.Message}");
        }
        return String.Empty;
    }

    private static string LoadSchedulesBySelectedCourse(int courseId)
    {
        try
        {
            _scheduleService = ServiceProvider?.GetService<IScheduleService>();
            var schedulesList = _scheduleService!.GetSchedulesByCourseId(courseId);
            if (schedulesList != null)
            {
                return System.Text.Json.JsonSerializer.Serialize(schedulesList);

            }
        }
        catch (Exception ex)
        {
           Console.WriteLine($"Error loading schedules for course: {ex.Message}");
        }
        return String.Empty;
    }

    private static string LoadStudentsBySelectedCourseAndScheduleId(int courseId, int scheduleId)
    {
        try
        {
            _studentService = ServiceProvider?.GetService<IStudentService>();
            var studentsList = _studentService!.GetStudentsByCourseIdAndScheduleId(courseId, scheduleId);
            return System.Text.Json.JsonSerializer.Serialize(studentsList);

        }
        catch (Exception ex)
        {
           Console.WriteLine($"Error loading students for course: {ex.Message}");
        }
        return String.Empty;
    }

    private static bool UpdateAttendance(List<int> studentIdList, int scheduleId)
    {
        _studentScheduleService = ServiceProvider?.GetService<IStudentScheduleService>();
        try
        {
            if (_studentScheduleService != null )
            {
                return _studentScheduleService.UpdateAttendance(studentIdList, scheduleId);
            }
            else
            {
                Console.WriteLine("Cannot update attendance at this time.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating attendance: {ex.Message}");
            return false;
        }
    }

}


