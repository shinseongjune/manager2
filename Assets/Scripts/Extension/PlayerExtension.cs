public static class PlayerExtension
{
    public static void SetName(this Player player, string name)
    {
        player.Name = name;
    }

    public static void GrowOld(this Player player)
    {
        player.Age++;
    }

    public static void SetTeam(this Player player, ref Team team)
    {
        if (GameManager.Instance.Teams.ContainsKey(team.IDNumber))
        {
            player.Team = team.IDNumber;
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void SetTeam(this Player player, int teamId)
    {
        if (GameManager.Instance.Teams.ContainsKey(teamId))
        {
            player.Team = teamId;
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveTeam(this Player player)
    {
        player.Team = -1;
    }

    public static void AddContract(this Player player, ref Contract contract)
    {
        if (GameManager.Instance.Contracts.ContainsKey(contract.IDNumber))
        {
            if (!player.Contracts.Contains(contract.IDNumber))
            {
                player.Contracts.Add(contract.IDNumber);
            }
            else
            {
                throw new AlreadyExistsInCollectionException();
            }
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddContract(this Player player, int contractId)
    {
        if (GameManager.Instance.Contracts.ContainsKey(contractId))
        {
            if (!player.Contracts.Contains(contractId))
            {
                player.Contracts.Add(contractId);
            }
            else
            {
                throw new AlreadyExistsInCollectionException();
            }
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveContract(this Player player, ref Contract contract)
    {
        if (GameManager.Instance.Contracts.ContainsKey(contract.IDNumber) && player.Contracts.Contains(contract.IDNumber))
        {
            player.Contracts.Remove(contract.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveContract(this Player player, int contractId)
    {
        if (GameManager.Instance.Contracts.ContainsKey(contractId) && player.Contracts.Contains(contractId))
        {
            player.Contracts.Remove(contractId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }
}
