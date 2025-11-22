namespace ZorkSharp.Combat;

using ZorkSharp.Core;
using ZorkSharp.World;

/// <summary>
/// Represents a combatant (player or NPC)
/// </summary>
public interface ICombatant
{
    string Id { get; }
    string Name { get; }
    int Health { get; set; }
    int MaxHealth { get; }
    int AttackPower { get; }
    int Defense { get; }
    bool IsAlive { get; }
    IGameObject? EquippedWeapon { get; set; }
}

/// <summary>
/// Manages combat between combatants
/// </summary>
public interface ICombatEngine
{
    CombatResult Attack(ICombatant attacker, ICombatant defender, IGameObject? weapon = null);
    CombatResult Defend(ICombatant defender, int incomingDamage);
    bool IsCombatActive { get; }
    void StartCombat(ICombatant player, ICombatant enemy);
    void EndCombat();
}

/// <summary>
/// Result of a combat action
/// </summary>
public record CombatResult(
    bool Hit,
    int Damage,
    bool TargetDied,
    string Message
);

/// <summary>
/// Represents weapon effectiveness and damage calculation
/// </summary>
public interface IWeapon
{
    string Id { get; }
    int BaseDamage { get; }
    int BonusDamage { get; }
    WeaponType WeaponType { get; }
    bool IsEffectiveAgainst(ICombatant target);
}

/// <summary>
/// Types of weapons
/// </summary>
public enum WeaponType
{
    Unarmed,
    Sword,
    Knife,
    Axe,
    Blunt,
    Ranged
}

/// <summary>
/// Base implementation of combatant (stub for future implementation)
/// </summary>
public abstract class CombatantBase : ICombatant
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int AttackPower { get; set; }
    public int Defense { get; set; }
    public bool IsAlive => Health > 0;
    public IGameObject? EquippedWeapon { get; set; }

    protected CombatantBase(int maxHealth, int attackPower, int defense)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        AttackPower = attackPower;
        Defense = defense;
    }

    public virtual void TakeDamage(int damage)
    {
        Health = Math.Max(0, Health - damage);
    }

    public virtual void Heal(int amount)
    {
        Health = Math.Min(MaxHealth, Health + amount);
    }
}

/// <summary>
/// Combat engine implementation (stub - not fully implemented)
/// TODO: Implement full combat logic including turns, AI, and weapon effectiveness
/// </summary>
public class CombatEngine : ICombatEngine
{
    private readonly IOutputWriter _output;
    private ICombatant? _player;
    private ICombatant? _enemy;

    public bool IsCombatActive { get; private set; }

    public CombatEngine(IOutputWriter output)
    {
        _output = output;
    }

    public void StartCombat(ICombatant player, ICombatant enemy)
    {
        _player = player;
        _enemy = enemy;
        IsCombatActive = true;
        _output.WriteLine($"Combat started with {enemy.Name}!");
    }

    public void EndCombat()
    {
        IsCombatActive = false;
        _player = null;
        _enemy = null;
    }

    public CombatResult Attack(ICombatant attacker, ICombatant defender, IGameObject? weapon = null)
    {
        // TODO: Implement full combat logic
        // - Calculate hit chance
        // - Factor in weapon effectiveness
        // - Apply defense/armor
        // - Handle critical hits
        // - Apply status effects

        // Stub implementation
        int damage = CalculateDamage(attacker, defender, weapon);
        bool hit = true; // TODO: Implement hit chance

        if (hit)
        {
            if (defender is CombatantBase defenderBase)
            {
                defenderBase.TakeDamage(damage);
            }

            string message = $"{attacker.Name} attacks {defender.Name} for {damage} damage!";
            if (!defender.IsAlive)
            {
                message += $" {defender.Name} has been defeated!";
            }

            return new CombatResult(true, damage, !defender.IsAlive, message);
        }

        return new CombatResult(false, 0, false, $"{attacker.Name} misses {defender.Name}!");
    }

    public CombatResult Defend(ICombatant defender, int incomingDamage)
    {
        // TODO: Implement defense mechanics
        // - Blocking
        // - Dodging
        // - Armor reduction

        int reducedDamage = Math.Max(0, incomingDamage - defender.Defense);
        return new CombatResult(true, reducedDamage, false, $"{defender.Name} defends, reducing damage to {reducedDamage}!");
    }

    private int CalculateDamage(ICombatant attacker, ICombatant defender, IGameObject? weapon)
    {
        // TODO: Implement full damage calculation
        // - Base attack power
        // - Weapon bonus
        // - Random variance
        // - Defender armor reduction

        int baseDamage = attacker.AttackPower;

        if (weapon != null && weapon.HasFlag(ObjectFlags.Weapon))
        {
            // TODO: Look up weapon stats
            baseDamage += 5; // Placeholder
        }

        // Apply defender's defense
        int finalDamage = Math.Max(1, baseDamage - defender.Defense);

        return finalDamage;
    }
}
