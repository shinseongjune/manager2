public class Contract
{
    readonly int idNumber;
    readonly int team;
    readonly int player;
    
    int remainingPeriod;
    readonly int salery;

    public int IDNumber => idNumber;
    public int Team => team;
    public int Player => player;
    public int RemainingPeriod
    {
        get { return remainingPeriod; }
        set { remainingPeriod = value; }
    }
    public int Salery => salery;

    public Contract(int id, int team, int player, int remainingPeriod, int salery)
    {
        idNumber = id;
        this.team = team;
        this.player = player;
        this.remainingPeriod = remainingPeriod;
        this.salery = salery;
    }
}
