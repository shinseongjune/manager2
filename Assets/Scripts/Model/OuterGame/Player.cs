using System.Collections.Generic;

public class Player
{
    readonly int idNumber;

    string name;
    sbyte age;

    int team = -1;
    readonly List<int> contracts = new();
    readonly List<int> offers = new();

    public int IDNumber => idNumber;
    public string Name
    { 
        get { return name; }
        set { name = value; }
    }
    public sbyte Age
    {
        get { return age; }
        set { age = value; }
    }
    public int Team
    {
        get { return team; }
        set { team = value; }
    }
    public List<int> Contracts => contracts;
    public List<int> Offers => offers;

    public Player(int id, string name, sbyte age)
    {
        idNumber = id;
        this.name = name;
        this.age = age;
    }
}
