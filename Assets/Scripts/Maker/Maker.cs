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
    /// <param name="manager">-1 = 매니저 없음</param>
    /// <returns></returns>
    public static Team MakeTeam(string name, int money, int manager = -1)
    {
        Team team = new(GameManager.Instance.NextTeamId++, name, money, manager);
        GameManager.Instance.AddTeam(team);
        return team;
    }

    public static Contract MakeContract(int team, int player, Date startDate, Date endDate, int salery)
    {
        Contract contract = new(GameManager.Instance.NextContractId++, team, player, startDate, endDate, salery);
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
        //TODO: match에 여러 변수 추가: 라운드, 승자조/패자조, 준결승/결승, 1경기/2경기/... 등등. 이후 다시 만들것
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
                        Match m = MakeMatch(pointTeam, tempTeams[(i + tempTeams.Count / 2) % tempTeams.Count], date + i, leagueId);
                        league.AddMatch(m.IDNumber);
                    }
                }
                date += tempTeamsCount + 4;
                break;
            case LeagueSystem.SingleElimination:
                int basicNum = 1;
                int roundCount = 0;
                while (basicNum < tempTeamsCount)
                {
                    basicNum *= 2;
                    roundCount++;
                }
                int unearnedCount = basicNum - tempTeamsCount;
                
                int matchCountInARound = 1; ;
                List<int> tempMatchList = new();
                while (roundCount > 0)
                {
                    switch (roundCount)
                    {
                        case 2:
                            for (int i = 0; i < unearnedCount; i++)
                            {
                                Match m = MakeMatch(-1, tempTeams[0], date + roundCount * 2, leagueId);
                                tempTeams.RemoveAt(0);
                                tempMatchList.Add(m.IDNumber);
                            }
                            for (int i = 0; i < matchCountInARound - unearnedCount; i++)
                            {
                                Match m = MakeMatch(-1, -1, date + roundCount * 2, leagueId);
                                tempMatchList.Add(m.IDNumber);
                            }
                            break;
                        case 1:
                            for (int i = 0; i < tempTeams.Count / 2; i++)
                            {
                                team1 = tempTeams[i];
                                team2 = tempTeams[(tempTeams.Count - 1) - i];
                                Match m = MakeMatch(team1, team2, date + roundCount * 2, leagueId);
                                tempMatchList.Add(m.IDNumber);
                            }
                            break;
                        default:
                            for (int i = 0; i < matchCountInARound; i++)
                            {
                                Match m = MakeMatch(-1, -1, date + roundCount * 2, leagueId);
                                tempMatchList.Add(m.IDNumber);
                            }
                            break;
                    }
                    matchCountInARound *= 2;
                    roundCount--;
                }

                for(int i = 0; i < tempMatchList.Count; i++)
                {
                    league.Matches.Add(tempMatchList[(tempMatchList.Count - 1) - i]);
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
                    league.AddMatch(m.IDNumber);
                    league.AddPlayOffMatch(m.IDNumber);
                }
                break;
            case LeagueSystem.SingleElimination:
                matchCount = playOffTeamCount - 1;
                for (int i = 0; i < matchCount; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddMatch(m.IDNumber);
                    league.AddPlayOffMatch(m.IDNumber);
                }
                break;
            case LeagueSystem.DoubleElimination:
                matchCount = playOffTeamCount * 2 - 1;
                for (int i = 0; i < matchCount; i++)
                {
                    Match m = MakeMatch(-1, -1, date + i, leagueId);
                    league.AddMatch(m.IDNumber);
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
