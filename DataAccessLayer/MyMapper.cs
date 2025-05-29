using AutoMapper;
using BusinessObjects.MyProfile;

public sealed class MyMapper
{
    private static MyMapper? instance;
    private static readonly object lockObj = new object();

    public IMapper? Mapper { get; private set; }

    private MyMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ScheduleProfile>();
            cfg.AddProfile<StudentProfile>();
        });

        Mapper = config.CreateMapper();
    }

    public static MyMapper Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObj)
                {
                    if (instance == null)
                    {
                        instance = new MyMapper();
                    }
                }
            }

            return instance;
        }
    }
}
