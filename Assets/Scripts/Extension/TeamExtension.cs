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
    }

    public static void RemoveContract(this Team team, int contractId)
    {
        if (team.Contracts.Contains(contractId))
        {
            team.Contracts.Remove(contractId);
        }
    }
}
