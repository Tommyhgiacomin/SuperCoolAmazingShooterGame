using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    // Variables
    private int _health;
    [SerializeField] private int _maxHealth = 100;

    // Properties
    // Getters
    public int MaxHealth => _maxHealth;
    [HideInInspector] public int CurrentHealth { get; private set; }

    public bool IsDead => CurrentHealth <= 0;

    public UnityEvent <int> OnDamage;
    public UnityEvent OnDied;

    // Setters
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Max(CurrentHealth - amount, 0);

        OnDamage?.Invoke(amount);

        if (IsDead)
        {
            OnDied?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead) return;

        CurrentHealth = Mathf.Min(CurrentHealth + amount, _maxHealth);
    }
}
