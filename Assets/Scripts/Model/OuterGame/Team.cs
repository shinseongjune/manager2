using System.Collections.Generic;

public class Team
{
    public int idNumber { get; }

    public int Money { get; set; }
    public int Manager { get; set; }
    public List<int> Players { get; }
    public List<Contract> Contracts { get; }
}
