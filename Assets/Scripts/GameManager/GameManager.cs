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
    private int nextPlayerId = 0;
    private int nextManagerId = 0;
    private int nextTeamId = 0;
    private int nextContractId = 0;
    private int nextMatchId = 0;
    private int nextLeagueId = 0;

    Date nowDate = new(2022, 1, 1);
    
    readonly Dictionary<int, Contract> contracts = new();
    readonly Dictionary<int, Player> players = new();
    readonly Dictionary<int, Manager> managers = new();
    readonly Dictionary<int, Team> teams = new();
    Dictionary<int, Match> matches = new();
    readonly Dictionary<int, League> leagues = new();
    readonly Dictionary<int, Contract> offers = new();
    #endregion Variables

    #region Properties
    public int NextPlayerId { get => nextPlayerId; set => nextPlayerId = value; }
    public int NextManagerId { get => nextManagerId; set => nextManagerId = value; }
    public int NextTeamId { get => nextTeamId; set => nextTeamId = value; }
    public int NextContractId { get => nextContractId; set => nextContractId = value; }
    public int NextMatchId { get => nextMatchId; set => nextMatchId = value; }
    public int NextLeagueId { get => nextLeagueId; set => nextLeagueId = value; }

    public Date NowDate { get => nowDate; set => nowDate = value; }

    public Dictionary<int, Contract> Contracts => contracts;
    public Dictionary<int, Player> Players => players;
    public Dictionary<int, Manager> Managers => managers;
    public Dictionary<int, Team> Teams => teams;
    public Dictionary<int, Match> Matches
    {
        get => matches;
        set => matches = value;
    }
    public Dictionary<int, League> Leagues => leagues;
    public Dictionary<int, Contract> Offers => offers;
    #endregion Properties

    /// <summary>
    /// Initialize GameManager for New Game.
    /// </summary>
    public void Initialize()
    {
        NextPlayerId = 0;
        NextManagerId = 0;
        NextTeamId = 0;
        NextContractId = 0;
        NextMatchId = 0;
        NextLeagueId = 0;

        nowDate = new(2022, 1, 1);

        Players.Clear();
        Teams.Clear();
        Contracts.Clear();
        Managers.Clear();
        Leagues.Clear();
        Matches.Clear();
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

    public void AddMatch(Match match)
    {
        if (!matches.ContainsKey(match.IDNumber))
        {
            matches.Add(match.IDNumber, match);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public void DeleteMatch(int matchId)
    {
        if (matches.ContainsKey(matchId))
        {
            matches.Remove(matchId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void DeleteMatch(Match match)
    {
        if (matches.ContainsKey(match.IDNumber))
        {
            matches.Remove(match.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public void AddLeague(League league)
    {
        if (!leagues.ContainsKey(league.IDNumber))
        {
            leagues.Add(league.IDNumber, league);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public void DeleteLeague(League league)
    {
        if (leagues.ContainsKey(league.IDNumber))
        {
            leagues.Remove(league.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }
    
    public void DeleteLeague(int leagueId)
    {
        if (leagues.ContainsKey(leagueId))
        {
            leagues.Remove(leagueId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }
}
