namespace CountersLibrary;

public class Tarif
{
    public int ID { get; set; }
    public virtual User User { get; set; }
    public double Power { get; set; }
    public double ColdWater { get; set; }
    public double Disposal { get; set; }
    public double HotWater { get; set; }
    public double Gas { get; set; }
    private static ApplContext db = Context.db;
    
    public Tarif(){}

    public Tarif(User user, double power, double coldWater, double disposal, double hotWater, double gas)
    {
        User = user;
        Power = power;
        ColdWater = coldWater;
        Disposal = disposal;
        HotWater = hotWater;
        Gas = gas;
    }
    
    public static void Add(Tarif tarif)
    {
        db.Tarif.Add(tarif);
        db.SaveChanges();
    }
    
    public static List<Tarif> GetAll()
    {
        return db.Tarif.ToList();
    }
}