public enum LeagueName
{
    Champions,
    GBPro,
    ChallengersCup
}

public class Match
{
    #region Variables
    readonly int idNumber;
    readonly int team1;
    readonly int team2;

    readonly Date dDay;

    readonly LeagueName eLeague;
    #endregion Variables

    #region Properties
    public int IDNumber => idNumber;
    public int Team1 => team1;
    public int Team2 => team2;

    public Date DDay => dDay;

    public LeagueName ELeague => eLeague;
    #endregion Properties

    public Match(int idNumber, int team1, int team2, Date dDay, LeagueName eLeague)
    {
        this.idNumber = idNumber;
        this.team1 = team1;
        this.team2 = team2;
        this.dDay = dDay;
        this.eLeague = eLeague;
    }
}
