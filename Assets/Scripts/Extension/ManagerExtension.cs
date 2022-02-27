public static class ManagerExtension
{
    public static void SetName(this Manager manager, string name)
    {
        manager.Name = name;
    }

    public static void SetTeam(this Manager manager, ref Team team)
    {
        if (GameManager.Instance.Teams.ContainsKey(team.IDNumber))
        {
            manager.Team = team.IDNumber;
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void SetTeam(this Manager manager, int teamId)
    {
        if (GameManager.Instance.Teams.ContainsKey(teamId))
        {
            manager.Team = teamId;
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void RemoveTeam(this Manager manager)
    {
        manager.Team = -1;
    }
}
