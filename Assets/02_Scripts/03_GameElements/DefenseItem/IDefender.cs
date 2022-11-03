namespace BoardDefense.DefenseItem
{
    public interface IDefender : IItem
    {
        int Damage { get; }
        int Range { get; }
        int Interval { get; }
        Direction Direction { get; }
        void StartAttack();
    }
}