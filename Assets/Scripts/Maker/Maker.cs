using System;
using System.Collections.Generic;

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
    /// <param name="manager">-1 = 매니저 없음</param>
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
    /// <param name="team1">-1 = 미정</param>
    /// <param name="team2">-1 = 미정</param>
    /// <param name="dDay"></param>
    /// <param name="league">-1 = 친선경기</param>
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
    /// <param name="regularSeason">LeagueSystem.None일 경우 예외 발생</param>
    /// <param name="playOff"></param>
    /// <returns></returns>
    public static League MakeLeague(string name, int entryMin, int entryMax, LeagueSystem regularSeason, LeagueSystem playOff = LeagueSystem.None, int playOffTeamCount = 0)
    {
        League league = new(GameManager.Instance.NextLeagueId++, 1, name, entryMin, entryMax, regularSeason, playOff, playOffTeamCount);
        GameManager.Instance.AddLeague(league);
        return league;
    }

    /// <summary>
    /// 대회 해당 회차 경기 전체 생성
    /// </summary>
    /// <param name="leagueId"></param>
    public static void MakeAllMatches(int leagueId)
    {
        League league = GameManager.Instance.Leagues[leagueId];

        Date date = league.StartDate;
        int entryCount = league.Entry.Count;
        int playOffTeamCount = league.PlayOffTeamCount;
        int matchCount;

        List<int> tempTeams = new(league.Entry);
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
                        team2 = tempTeams[(tempTeams.Count - 1) - j];
                        Match m = MakeMatch(team1, team2, date + i, leagueId);
                        league.AddMatch(m.IDNumber);
                    }
                    if (pointTeam != -100)
                    {
                        Match m = MakeMatch(pointTeam, tempTeams[tempTeams.Count / 2 + 1], date + i, leagueId);
                        league.AddMatch(m.IDNumber);
                    }
                }
                date += tempTeams.Count + 4;
                break;
            case LeagueSystem.SingleElimination:
                //TODO: 라운드 수를 세서 위에서 아래로 만들어가는게 나을듯?
                int basicNum = 1;
                if (basicNum < entryCount)
                {
                    basicNum *= 2;
                }
                int unearnedCount = basicNum - entryCount;
                int firstRoundTeamCount = (entryCount - unearnedCount) / 2;

                int[] firstRoundTeams = new int[firstRoundTeamCount];

                for (int i = 0; i < firstRoundTeamCount; i++)
                {
                    int reverseIndex = (firstRoundTeamCount - 1) - i;
                    firstRoundTeams[reverseIndex] = tempTeams[reverseIndex];
                    tempTeams.RemoveAt(reverseIndex);
                }

                for (int i = 0; i < firstRoundTeams.Length; i += 2)
                {
                    team1 = firstRoundTeams[i];
                    team2 = firstRoundTeams[i + 1];
                    Match m = MakeMatch(team1, team2, date + i, leagueId);
                    league.AddMatch(m.IDNumber);
                }
                date += firstRoundTeamCount / 2 + 1;

                for (int i = 0; i < unearnedCount; i++)
                {
                    team1 = -1;
                    team2 = tempTeams[i];
                    tempTeams.RemoveAt(i);
                    Match m = MakeMatch(team1, team2, date, leagueId);
                    league.AddMatch(m.IDNumber);
                }

                for (int i = 0; i < tempTeams.Count; i += 2)
                {
                    team1 = tempTeams[i];
                    team2 = tempTeams[i + 1];
                    Match m = MakeMatch(team1, team2, date, leagueId);
                    league.AddMatch(m.IDNumber);
                }
                date += 1;

                for (int i = 0; i < (unearnedCount + tempTeams.Count / 2) / 2; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddMatch(m.IDNumber);
                }
                date += (unearnedCount + tempTeams.Count / 2) + 4;

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
    }
}
