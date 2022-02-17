public class Manager
{
    int idNumber;
    string name;

    public int IDNumber => IDNumber;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public Manager(int id, string name)
    {
        idNumber = id;
        this.name = name;
    }
}
