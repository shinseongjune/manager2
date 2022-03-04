using System;
using System.Collections.Generic;
using System.Linq;

public static class Maker
{
    public static Player MakePlayer(string name)
    {
        Random random = new(DateTime.Now.Millisecond + GameManager.Instance.NextPlayerId);
        sbyte age = (sbyte)random.Next(16, 30);
        Player player = new(GameManager.Instance.NextPlayerId++, name, age);
        GameManager.Instance.AddPlayer(player);
        return player;
    }

    public static Manager MakeManager(string name)
    {
        Manager manager = new(GameManager.Instance.NextManagerId++, name);
        GameManager.Instance.AddManager(manager);
        return manager;
    }

    public static Team MakeTeam(string name)
    {
        Team team = new(GameManager.Instance.NextTeamId++, name);
        GameManager.Instance.AddTeam(team);
        return team;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="money"></param>
    /// <param name="manager">-1 = �Ŵ��� ����</param>
    /// <returns></returns>
    public static Team MakeTeam(string name, int money, int manager = -1)
    {
        Team team = new(GameManager.Instance.NextTeamId++, name, money, manager);
        GameManager.Instance.AddTeam(team);
        return team;
    }

    public static Contract MakeContract(int team, int player, Date dDay, int salery)
    {
        int remainingPeriod = dDay.GetHashCode() - GameManager.Instance.NowDate.GetHashCode();
        Contract contract = new(GameManager.Instance.NextContractId++, team, player, remainingPeriod, salery);
        GameManager.Instance.AddContract(contract);
        return contract;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="team1">-1 = ����</param>
    /// <param name="team2">-1 = ����</param>
    /// <param name="dDay"></param>
    /// <param name="league">-1 = ģ�����</param>
    /// <returns></returns>
    public static Match MakeMatch(int team1, int team2, Date dDay, int league = -1)
    {
        Match match = new(GameManager.Instance.NextMatchId++, team1, team2, dDay, league);
        GameManager.Instance.AddMatch(match);
        return match;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="regularSeason">LeagueSystem.None�� ��� ���� �߻�</param>
    /// <param name="playOff"></param>
    /// <returns></returns>
    public static League MakeLeague(string name, int entryMin, int entryMax, LeagueSystem regularSeason, LeagueSystem playOff = LeagueSystem.None, int playOffTeamCount = 0)
    {
        League league = new(GameManager.Instance.NextLeagueId++, 1, name, entryMin, entryMax, regularSeason, playOff, playOffTeamCount);
        GameManager.Instance.AddLeague(league);
        return league;
    }

    /// <summary>
    /// ��ȸ �ش� ȸ�� ��� ��ü ����
    /// </summary>
    /// <param name="leagueId"></param>
    public static void MakeAllMatches(int leagueId)
    {
        //TODO: match�� ���� ���� �߰�: ����, ������/������, �ذ��/���, 1���/2���/... ���. ���� �ٽ� �����
        League league = GameManager.Instance.Leagues[leagueId];

        Date date = league.StartDate;
        int playOffTeamCount = league.PlayOffTeamCount;
        int matchCount;

        List<int> tempTeams = new(league.Entry);
        int tempTeamsCount = tempTeams.Count;
        tempTeams.Shuffle();

        int team1;
        int team2;

        switch (league.RegularSeason)
        {
            case LeagueSystem.RoundRobin:

                int pointTeam;

                if (tempTeams.Count % 2 != 0)
                {
                    pointTeam = -100;
                }
                else
                {
                    pointTeam = tempTeams[0];
                    tempTeams.RemoveAt(0);
                }

                for (int i = 0; i < tempTeams.Count; i++)
                {
                    for (int j = 0; j < tempTeams.Count / 2; j++)
                    {
                        team1 = tempTeams[(i + j) % tempTeams.Count];
                        team2 = tempTeams[(i + (tempTeams.Count - 1) - j) % tempTeams.Count];
                        Match m = MakeMatch(team1, team2, date + i, leagueId);
                        league.AddMatch(m.IDNumber);
                    }
                    if (pointTeam != -100)
                    {
                        Match m = MakeMatch(pointTeam, tempTeams[tempTeams.Count / 2 + 1], date + i, leagueId);
                        league.AddMatch(m.IDNumber);
                    }
                }
                date += tempTeamsCount + 4;
                break;
            case LeagueSystem.SingleElimination:
                int basicNum = 1;
                int roundCount = 1;
                while (basicNum < tempTeamsCount)
                {
                    basicNum *= 2;
                    roundCount++;
                }
                int unearnedCount = basicNum - tempTeamsCount;
                
                int matchCountInARound = 1; ;
                while (roundCount > 0)
                {
                    switch (roundCount)
                    {
                        case 2:
                            for (int i = 0; i < unearnedCount; i++)
                            {
                                Match m = Maker.MakeMatch(-1, tempTeams[0], date += (int)Math.Round(roundCount + (Math.Log((double)roundCount))), leagueId);
                                tempTeams.RemoveAt(0);
                                league.AddMatch(m);
                            }
                            for (int i = 0; i < matchCountInARound - unearnedCount; i++)
                            {
                                Match m = Maker.MakeMatch(-1, -1, date += (int)Math.Round(roundCount + (Math.Log((double)roundCount))), leagueId);
                                league.AddMatch(m);
                            }
                            break;
                        case 1:
                            for (int i = 0; i < tempTeams.Count / 2; i++)
                            {
                                team1 = tempTeams[0];
                                team2 = tempTeams[1];
                                tempTeams.RemoveAt(0);
                                tempTeams.RemoveAt(0);
                                Match m = Maker.MakeMatch(team1, team2, date += (int)Math.Round(roundCount + (Math.Log((double)roundCount))), leagueId);
                                league.AddMatch(m);
                            }
                            break;
                        default:
                            for (int i = 0; i < matchCountInARound; i++)
                            {
                                Match m = Maker.MakeMatch(-1, -1, date += (int)Math.Round(roundCount + (Math.Log((double)roundCount))), leagueId);
                                league.AddMatch(m);
                            }
                            break;
                    }
                    matchCountInARound *= 2;
                    roundCount--;
                }

                date += tempTeamsCount + 4;

                break;
            case LeagueSystem.DoubleElimination:
                break;
        }

        switch (league.PlayOff)
        {
            case LeagueSystem.RoundRobin:
                matchCount = playOffTeamCount * (playOffTeamCount - 1) / 2;
                for (int i = 0; i < matchCount; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddPlayOffMatch(m.IDNumber);
                }
                break;
            case LeagueSystem.SingleElimination:
                matchCount = playOffTeamCount - 1;
                for (int i = 0; i < matchCount; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddPlayOffMatch(m.IDNumber);
                }
                break;
            case LeagueSystem.DoubleElimination:
                matchCount = playOffTeamCount * 2 - 1;
                for (int i = 0; i < matchCount; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddPlayOffMatch(m.IDNumber);
                }
                break;
            case LeagueSystem.None:
                break;
        }

        var tempMatchesList = GameManager.Instance.Matches.ToList();
        tempMatchesList.Sort(new MatchDicComparer());
        GameManager.Instance.Matches = tempMatchesList.ToDictionary(x => x.Key, x => x.Value);
    }
}
