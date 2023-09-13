namespace CountersLibrary;

public class Cost
{
    public int ID { get; set; }
    public virtual Record RRecord { get; set; }
    public int PowerSpend { get; set; }
    public int ColdSpend { get; set; }
    public int HotSpend { get; set; }
    public int GasSpend { get; set; }
    public double PowerCost { get; set; }
    public double WaterCost { get; set; }
    public double GasCost { get; set; }
    public virtual User User { get; set; }
    private static ApplContext db = Context.db;
    
    public Cost(){}

    public Cost(Record rrecord, int powerSpend, int coldSpend, int hotSpend, int gasSpend, double powerCost, double waterCost, double gasCost, User user)
    {
        RRecord = rrecord;
        PowerSpend = powerSpend;
        ColdSpend = coldSpend;
        HotSpend = hotSpend;
        GasSpend = gasSpend;
        PowerCost = powerCost;
        WaterCost = waterCost;
        GasCost = gasCost;
        User = user;
    }
    
    public static Cost GetLastCost()
    {
        return db.Cost.OrderBy(r=>r.ID).LastOrDefault();
    }

    public static Cost GetBeforeLastCost()
    {
        List<Cost> list = db.Cost.OrderBy(r=>r.ID).ToList();
        return list[list.Count - 2];
    }
    
    public static Cost GetLastCostWithUserID(int id)
    {
        return db.Cost.Where(u => u.User.ID==id).OrderBy(r=>r.ID).LastOrDefault();
    }

    public static Cost GetBeforeLastCostWithUserID(int id)
    {
        List<Cost> list = db.Cost.Where(u => u.User.ID==id).OrderBy(r=>r.ID).ToList();
        return list[list.Count - 2];
    }

    public static List<Cost> GetAll()
    {
        return db.Cost.ToList();
    }
    public static List<Cost> GetAllWithID(int id)
    {
        return db.Cost.Where(u=>u.User.ID==id).ToList();
    }

    public static void Add(Cost cost)
    {
        db.Cost.Add(cost);
        db.SaveChanges();
    }

    public override string ToString()
    {
        string str = $"Дата - {RRecord.Date}  " +
                     $"Электричества потрачено - {PowerSpend}; " +
                     $"Холодной воды потрачено - {ColdSpend}; " +
                     $"Горячей воды потрачено - {HotSpend}; " +
                     $"Газа потрачено - {GasSpend};\n" +
                     $"Стоимость электричества - {PowerCost}; " +
                     $"Стоимость воды - {WaterCost}; " +
                     $"Стоимость газа - {GasCost};\n\n";
        return str;
    }
}