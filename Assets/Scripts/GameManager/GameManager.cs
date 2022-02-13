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
    #endregion

    Date nowDate = new(2022, 1, 1);
    
    List<Contract> contracts = new();
    List<Player> players = new();
    List<Manager> managers = new();
    List<Team> teams = new();

    void DateProgress()
    {
        nowDate++;
    }
}
