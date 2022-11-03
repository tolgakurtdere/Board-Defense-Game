namespace BoardDefense.Enemy
{
    public interface IDamageable
    {
        int Health { get; }
        void TakeDamage(int damageAmount);
        void Die();
    }
}