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
    public Date NowDate => nowDate;

    public Dictionary<int, Contract> Contracts => contracts;
    public Dictionary<int, Player> Players => players;
    public Dictionary<int, Manager> Managers => managers;
    public Dictionary<int, Team> Teams => teams;
    #endregion Properties

    void AddContract(Contract contract)
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

    void DeleteContract(Contract contract)
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

    void DeleteContract(int contractId)
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

    void AddPlayer(Player player)
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

    void DeletePlayer(Player player)
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

    void DeletePlayer(int playerId)
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

    void AddManager(Manager manager)
    {
        if (managers.ContainsKey(manager.IDNumber))
        {
            managers.Add(manager.IDNumber, manager);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }
    void DeleteManager(Manager manager)
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

    void DeleteManager(int managerId)
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


    void DateProgress()
    {
        nowDate++;
    }
}
