public class Manager
{
    int idNumber;
    string name;
    int team;

    public int IDNumber => IDNumber;
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
