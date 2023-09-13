namespace CountersLibrary;

public class User
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    private static ApplContext db = Context.db;
    
    public User(){}

    public User(string name, string address)
    {
        Name = name;
        Address = address;
    }

    public static void Add(User user)
    {
        db.User.Add(user);
        db.SaveChanges();
    }

    public static void Update()
    {
        db.SaveChanges();
    }
    
    public static List<User> GetAll()
    {
        return db.User.ToList();
    }

    public static User FindByName(string name)
    {
        return db.User.FirstOrDefault(u => u.Name == name);
    }
    public static User FindByID(int id)
    {
        return db.User.FirstOrDefault(u => u.ID == id);
    }
}