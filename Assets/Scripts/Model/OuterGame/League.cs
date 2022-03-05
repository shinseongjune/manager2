using System;
using System.Collections;
using System.Collections.Generic;

public enum LeagueSystem
{
    RoundRobin,
    SingleElimination,
    DoubleElimination,
    None
}

public class League
{
    #region Variables
    readonly int idNumber;

    Date startDate;
    int ordinalNumber;
    string name;

    int entryMin;
    int entryMax;

    LeagueSystem regularSeason;
    LeagueSystem playOff = LeagueSystem.None;

    //플레이오프 참여팀 수
    int playOffTeamCount = 0;

    readonly List<int> matches = new();
    readonly List<int> playOffs = new();

    readonly HashSet<int> entry = new();
    #endregion Variables

    #region Properties
    public int IDNumber => idNumber;

    public Date StartDate { get => startDate; set => startDate = value; }
    public int OrdinalNumber { get => ordinalNumber; set => ordinalNumber = value; }
    public string Name { get => name; set => name = value; }

    public int EntryMin { get => entryMin; set => entryMin = value; }
    public int EntryMax { get => entryMax; set => entryMax = value; }

    public LeagueSystem RegularSeason { get => regularSeason; set => regularSeason = value; }
    public LeagueSystem PlayOff { get => playOff; set => playOff = value; }

    public int PlayOffTeamCount { get => playOffTeamCount; set => playOffTeamCount = value; }

    public List<int> Matches => matches;
    public List<int> PlayOffs => playOffs;

    public HashSet<int> Entry => entry;
    #endregion Properties

    public League(int idNumber, int ordinalNumber, string name, int entryMin, int entryMax, LeagueSystem regularSeason, LeagueSystem playOff = LeagueSystem.None, int playOffTeamCount = 0)
    {
        if (regularSeason == LeagueSystem.None) throw new RegularSeasonNoRuleException();
        if (playOffTeamCount == 1) playOffTeamCount = 2;
        if (entryMin < 4) entryMin = 4;
        if (entryMax < entryMin) entryMax = entryMin;
        if (playOff == LeagueSystem.None && playOffTeamCount > 0) throw new NoPlayOffButParticipantsExistException();

        Random r = new(DateTime.Now.Millisecond);

        this.idNumber = idNumber;
        startDate = GameManager.Instance.NowDate + r.Next(4, 10);
        this.ordinalNumber = ordinalNumber;
        this.name = name;
        this.entryMin = entryMin;
        this.entryMax = entryMax;
        this.regularSeason = regularSeason;
        this.playOff = playOff;
        this.playOffTeamCount = playOffTeamCount;
    }
}
