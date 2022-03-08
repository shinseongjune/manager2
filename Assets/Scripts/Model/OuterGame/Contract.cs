public class Contract
{
    readonly int idNumber;
    readonly int team;
    readonly int player;

    readonly Date startDate;
    readonly Date endDate;
    
    readonly int salery;

    public int IDNumber => idNumber;
    public int Team => team;
    public int Player => player;
    public Date StartDate => startDate;
    public Date EndDate => endDate;
    public int RemainingPeriod
    {
        get { return endDate.GetHashCode() - GameManager.Instance.NowDate.GetHashCode(); }
    }
    public int Salery => salery;

    public Contract(int id, int team, int player, Date startDate, Date endDate, int salery)
    {
        idNumber = id;
        this.team = team;
        this.player = player;
        this.startDate = startDate;
        this.endDate = endDate;
        this.salery = salery;
    }
}
