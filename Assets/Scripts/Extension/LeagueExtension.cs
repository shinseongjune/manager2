using System.Collections;
using System.Collections.Generic;

public static class LeagueExtension
{
    public static void SetStartDate(this League league, int i)
    {
        league.StartDate = GameManager.Instance.NowDate + i;
    }

    public static void NextOrdinalNumber(this League league)
    {
        league.OrdinalNumber++;
    }

    public static bool SetName(this League league, string name)
    {
        foreach(League l in GameManager.Instance.Leagues.Values)
        {
            if (l.Name == name) return false;
        }
        league.Name = name;
        return true;
    }

    public static bool SetEntryMin(this League league, int min)
    {
        if (min > league.EntryMax) return false;
        league.EntryMin = min;
        return true;
    }

    public static bool SetEntryMax(this League league, int max)
    {
        if (max < league.EntryMin) return false;
        league.EntryMax = max;
        return true;
    }

    public static bool SetRegularSeason(this League league, LeagueSystem leagueSystem)
    {
        if (leagueSystem == LeagueSystem.None) return false;
        league.RegularSeason = leagueSystem;
        return true;
    }

    public static void SetPlayOff(this League league, LeagueSystem leagueSystem)
    {
        league.PlayOff = leagueSystem;
        if (leagueSystem == LeagueSystem.None) league.PlayOffTeamCount = 0;
    }

    public static bool SetPlayOffTeamCount(this League league, int count)
    {
        if (league.PlayOff == LeagueSystem.None || count < 2) return false;
        league.PlayOffTeamCount = count;
        return true;
    }

    public static void AddMatch(this League league, Match match)
    {
        if (!league.Matches.Contains(match.IDNumber))
        {
            league.Matches.Add(match.IDNumber);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddMatch(this League league, int matchId)
    {
        if (!league.Matches.Contains(matchId))
        {
            league.Matches.Add(matchId);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void DeleteMatch(this League league, Match match)
    {
        if (league.Matches.Contains(match.IDNumber))
        {
            league.Matches.Remove(match.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void DeleteMatch(this League league, int matchId)
    {
        if (league.Matches.Contains(matchId))
        {
            league.Matches.Remove(matchId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void AddPlayOffMatch(this League league, Match match)
    {
        if (!league.PlayOffs.Contains(match.IDNumber))
        {
            league.PlayOffs.Add(match.IDNumber);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void AddPlayOffMatch(this League league, int matchId)
    {
        if (!league.PlayOffs.Contains(matchId))
        {
            league.PlayOffs.Add(matchId);
        }
        else
        {
            throw new AlreadyExistsInCollectionException();
        }
    }

    public static void DeletePlayOffMatch(this League league, Match match)
    {
        if (league.PlayOffs.Contains(match.IDNumber))
        {
            league.PlayOffs.Remove(match.IDNumber);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }

    public static void DeletePlayOffMatch(this League league, int matchId)
    {
        if (league.PlayOffs.Contains(matchId))
        {
            league.PlayOffs.Remove(matchId);
        }
        else
        {
            throw new NotExistsInCollectionException();
        }
    }
}
