using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager
{
    #region Singleton
    private static readonly Lazy<GameManager> _instance = new(() => new GameManager());

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
    public int nextPlayerId = 0;
    public int nextManagerId = 0;
    public int nextTeamId = 0;
    public int nextContractId = 0;

    Date nowDate = new(2022, 1, 1);
    
    readonly Dictionary<int, Contract> contracts = new();
    readonly Dictionary<int, Player> players = new();
    readonly Dictionary<int, Manager> managers = new();
    readonly Dictionary<int, Team> teams = new();
    #endregion Variables

    #region Properties
    public Date NowDate => nowDate;

    public Dictionary<int, Contract> Contracts => contracts;
    public Dictionary<int, Player> Players => players;
    public Dictionary<int, Manager> Managers => managers;
    public Dictionary<int, Team> Teams => teams;
    #endregion Properties

    /// <summary>
    /// Initialize GameManager for New Game.
    /// </summary>
    public void Initialize()
    {
        nextPlayerId = 0;
        nextManagerId = 0;
        nextTeamId = 0;
        nextContractId = 0;

        nowDate = new(2022, 1, 1);

        Players.Clear();
        Teams.Clear();
        Contracts.Clear();
        Managers.Clear();
    }

    public void AddContract(Contract contract)
    {
        if (!contracts.ContainsKey(contract.IDNumber))
        {
            contracts.Add(contract.IDNumber, contract);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public void DeleteContract(Contract contract)
    {
        if (!contracts.ContainsKey(contract.IDNumber))
        {
            contracts.Remove(contract.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DeleteContract(int contractId)
    {
        if (contracts.ContainsKey(contractId))
        {
            contracts.Remove(contractId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void AddPlayer(Player player)
    {
        if (!players.ContainsKey(player.IDNumber))
        {
            players.Add(player.IDNumber, player);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public void DeletePlayer(Player player)
    {
        if (players.ContainsKey(player.IDNumber))
        {
            players.Remove(player.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DeletePlayer(int playerId)
    {
        if (players.ContainsKey(playerId))
        {
            players.Remove(playerId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void AddManager(Manager manager)
    {
        if (!managers.ContainsKey(manager.IDNumber))
        {
            managers.Add(manager.IDNumber, manager);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }
    public void DeleteManager(Manager manager)
    {
        if (managers.ContainsKey(manager.IDNumber))
        {
            managers.Remove(manager.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DeleteManager(int managerId)
    {
        if (managers.ContainsKey(managerId))
        {
            managers.Remove(managerId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void AddTeam(Team team)
    {
        if (!teams.ContainsKey(team.IDNumber))
        {
            teams.Add(team.IDNumber, team);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public void DeleteTeam(Team team)
    {
        if (teams.ContainsKey(team.IDNumber))
        {
            teams.Remove(team.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DeleteTeam(int teamId)
    {
        if (teams.ContainsKey(teamId))
        {
            teams.Remove(teamId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DateProgress()
    {
        nowDate++;
    }
}
