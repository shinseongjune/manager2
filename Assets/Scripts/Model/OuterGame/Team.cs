using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public int IDNumber { get; }

    public int Money { get; set; }
    public int Manager { get; set; }
    public List<int> Players { get; }
    public List<Contract> Contracts { get; }

    public Team(int id, int money, int manager)
    {
        IDNumber = id;
        Money = money;
        Manager = manager;
    }

    public void AddPlayer(int player)
    {
        Players.Add(player);
    }

    public void DeletePlayer(int player)
    {
        if ( Players.Contains(player) )
        {
            Players.Remove(player);
        }
    }
}
