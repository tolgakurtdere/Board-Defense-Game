namespace BoardDefense.DefenseItem
{
    public class DefenseItem2 : DefenseItemBase
    {
        public override int Damage => 5;
        public override int Range => 2;
        public override int Interval => 4;
        public override Direction Direction => Direction.Forward;
    }
}