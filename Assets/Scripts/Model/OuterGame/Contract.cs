public class Contract
{
    int idNumber;
    int team;
    int player;
    
    int remainingPeriod;
    int salery;

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
