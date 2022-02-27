public class Manager
{
    readonly int idNumber;
    string name;
    int team;

    public int IDNumber => idNumber;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int Team
    {
        get { return team; }
        set { team = value; }
    }

    public Manager(int id, string name)
    {
        idNumber = id;
        this.name = name;
    }
}
