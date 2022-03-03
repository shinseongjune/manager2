public class Match
{
    #region Variables
    readonly int idNumber;
    int team1;
    int team2;

    readonly Date dDay;

    readonly int league;
    #endregion Variables

    #region Properties
    public int IDNumber => idNumber;
    public int Team1
    {
        get => team1;
        set => team1 = value;
    }
    public int Team2
    {
        get => team2;
        set => team2 = value;
    }

    public Date DDay => dDay;

    public int League => league;
    #endregion Properties

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idNumber"></param>
    /// <param name="team1">-1 = 미정</param>
    /// <param name="team2">-1 = 미정</param>
    /// <param name="dDay"></param>
    /// <param name="eLeague"></param>
    public Match(int idNumber, int team1, int team2, Date dDay, int league)
    {
        this.idNumber = idNumber;
        this.team1 = team1;
        this.team2 = team2;
        this.dDay = dDay;
        this.league = league;
    }
}
