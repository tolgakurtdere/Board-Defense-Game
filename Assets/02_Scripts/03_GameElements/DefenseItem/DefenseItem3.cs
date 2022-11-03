namespace BoardDefense.DefenseItem
{
    public class DefenseItem3 : DefenseItemBase
    {
        public override int Damage => 10;
        public override int Range => 1;
        public override int Interval => 5;
        public override Direction Direction => Direction.All;
    }
}