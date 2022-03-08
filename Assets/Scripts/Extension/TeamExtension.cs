public static class TeamExtension
{
    public static void SetName(this Team team, string name)
    {
        team.Name = name;
    }

    public static void EarnOrSpend(this Team team, int money)
    {
        team.Money += money;
    }

    public static void EmployManager(this Team team, int manager)
    {
        team.Manager = manager;
    }

    public static void FireManager(this Team team)
    {
        team.Manager = -1;
    }

    public static void AddPlayer(this Team team, ref Player player)
    {
        if (!team.Players.Contains(player.IDNumber))
        {
            if (GameManager.Instance.Players.ContainsKey(player.IDNumber))
            {
                team.Players.Add(player.IDNumber);
            }
            else
            {
                throw new NotExistsInCollectionException();
            }
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddPlayer(this Team team, int playerId)
    {
        if (!team.Players.Contains(playerId))
        {
            if (GameManager.Instance.Players.ContainsKey(playerId))
            {
                team.Players.Add(playerId);
            }
            else
            {
                throw new NotExistsInCollectionException();
            }
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }


    public static void RemovePlayer(this Team team, ref Player player)
    {
        if (team.Players.Contains(player.IDNumber))
        {
            team.Players.Remove(player.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemovePlayer(this Team team, int playerId)
    {
        if (team.Players.Contains(playerId))
        {
            team.Players.Remove(playerId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddContract(this Team team, ref Contract contract)
    {
        if (!team.Contracts.Contains(contract.IDNumber))
        {
            if (GameManager.Instance.Contracts.ContainsKey(contract.IDNumber))
            {
                team.Contracts.Add(contract.IDNumber);
            }
            else
            {
                throw new NotExistsInCollectionException();
            }
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddContract(this Team team, int contractId)
    {
        if (!team.Contracts.Contains(contractId))
        {
            if (GameManager.Instance.Contracts.ContainsKey(contractId))
            {
                team.Contracts.Add(contractId);
            }
            else
            {
                throw new NotExistsInCollectionException();
            }
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void RemoveContract(this Team team, ref Contract contract)
    {
        if (team.Contracts.Contains(contract.IDNumber))
        {
            team.Contracts.Remove(contract.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveContract(this Team team, int contractId)
    {
        if (team.Contracts.Contains(contractId))
        {
            team.Contracts.Remove(contractId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddMatch(this Team team, ref Match match)
    {
        if (!team.Matches.Contains(match.IDNumber))
        {
            team.Matches.Add(match.IDNumber);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddMatch(this Team team, int matchId)
    {
        if (!team.Matches.Contains(matchId))
        {
            team.Matches.Add(matchId);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void RemoveMatch(this Team team, ref Match match)
    {
        if (team.Matches.Contains(match.IDNumber))
        {
            team.Matches.Remove(match.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveMatch(this Team team, int matchId)
    {
        if (team.Matches.Contains(matchId))
        {
            team.Matches.Remove(matchId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddOffer(this Team team, ref Contract contract)
    {
        if (!team.Offers.Contains(contract.IDNumber))
        {
            team.Offers.Add(contract.IDNumber);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddOffer(this Team team, int contractId)
    {
        if (!team.Offers.Contains(contractId))
        {
            team.Offers.Remove(contractId);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void RemoveOffer(this Team team, ref Contract contract)
    {
        if (team.Offers.Contains(contract.IDNumber))
        {
            team.Offers.Remove(contract.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveOffer(this Team team, int contractId)
    {
        if (team.Offers.Contains(contractId))
        {
            team.Offers.Remove(contractId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddLeague(this Team team, ref League league)
    {
        if (!team.Leagues.Contains(league.IDNumber))
        {
            team.Leagues.Add(league.IDNumber);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddLeague(this Team team, int leagueId)
    {
        if (team.Leagues.Contains(leagueId))
        {
            team.Leagues.Add(leagueId);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void RemoveLeague(this Team team, ref League league)
    {
        if (team.Leagues.Contains(league.IDNumber))
        {
            team.Leagues.Remove(league.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveLeague(this Team team, int leagueId)
    {
        if (team.Leagues.Contains(leagueId))
        {
            team.Leagues.Remove(leagueId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }
}
