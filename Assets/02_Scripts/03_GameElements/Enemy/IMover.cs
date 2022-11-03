using System.Collections;

namespace BoardDefense.Enemy
{
    public interface IMover : IItem
    {
        float Speed { get; }
        IEnumerator Move();
    }
}