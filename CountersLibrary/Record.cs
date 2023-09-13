namespace CountersLibrary;

public class Record
{
    public int ID { get; set; }
    public DateOnly Date { get; set; }
    public int Power { get; set; }
    public int Cold { get; set; }
    public int Hot  { get; set; }
    public int Gas { get; set; }
    public virtual User User { get; set; }
    private static ApplContext db = Context.db;
    
    public Record() { }
    public Record(DateOnly date, int power, int cold, int hot, int gas, User user)
    {
        Date = date;
        Power = power;
        Cold = cold;
        Hot = hot;
        Gas = gas;
        User = user;
    }

    public static Record GetLastRecord()
    {
        return db.Record.OrderBy(r=>r.ID).LastOrDefault();
    }
    public static Record GetLastRecordWithUserID(int id)
    {
        return db.Record.Where(u => u.User.ID==id).OrderBy(r=>r.ID).LastOrDefault();
    }

    public static Record GetBeforeLastRecord()
    {
        List<Record> list = db.Record.OrderBy(r=>r.ID).ToList();
        return list[list.Count - 2];
    }
    
    public static Record GetBeforeLastRecordWithUserID(int id)
    {
        List<Record> list = db.Record.Where(u => u.User.ID==id).OrderBy(r=>r.ID).ToList();
        return list[list.Count - 2];
    }

    public static List<Record> GetAll()
    {
        return db.Record.ToList();
    }
    public static List<Record> GetAllWithID(int id)
    {
        return db.Record.Where(u=>u.User.ID==id).ToList();
    }

    public static void Add(Record record)
    {
        db.Record.Add(record);
        db.SaveChanges();
    }

    
}