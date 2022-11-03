namespace BoardDefense.DefenseItem
{
    public class DefenseItem1 : DefenseItemBase
    {
        public override int Damage => 3;
        public override int Range => 4;
        public override int Interval => 3;
        public override Direction Direction => Direction.Forward;
    }
}