using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager
{
    #region Singleton
    private static readonly Lazy<GameManager> _instance = new Lazy<GameManager>(() => new GameManager());

    public static GameManager Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    private GameManager() { }
    #endregion Singleton

    #region Variables
    Date nowDate = new(2022, 1, 1);
    
    Dictionary<int, Contract> contracts = new();
    Dictionary<int, Player> players = new();
    Dictionary<int, Manager> managers = new();
    Dictionary<int, Team> teams = new();
    #endregion Variables

    #region Properties
    public Date NowDate
    {
        get
        {
            return nowDate;
        }
    }

    public Dictionary<int, Contract> Contracts
    {
        get
        {
            return contracts;
        }
    }
    public Dictionary<int, Player> Players
    {
        get
        {
            return players;
        }
    }
    public Dictionary<int, Manager> Managers
    {
        get
        {
            return managers;
        }
    }
    public Dictionary<int, Team> Teams
    {
        get
        {
            return teams;
        }
    }
    #endregion Properties



    void DateProgress()
    {
        nowDate++;
        Team a = new();
    }
}
