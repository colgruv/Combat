using UnityEngine;
using System.Collections;

/// <summary>
/// HIT POINT POOLS
/// Current [Score] + Temp [Score] = Total Score
/// </summary>
[System.Serializable]
public struct HitPoints
{
    // Hit Point pools in ascending order by priority.

    // Health (Non-recharging, vital HP)
    public int Health;
    private Vector3 health;
    public float CurrentHealth { get { return health.x; } set { health.x = value; } }
    public float MaxHealth { get { return health.y; } set { health.y = value; } }
    public float TempHealth { get { return health.z; } set { health.z = value; } }

    // Armor (Static, permanent HP)
    public int Armor;
    private Vector3 armor;
    public float CurrentArmor { get { return armor.x; } set { armor.x = value; } }
    public float MaxArmor { get { return armor.y; } set { armor.y = value; } }
    public float TempArmor { get { return armor.z; } set { armor.z = value; } }

    // Parry (Recharchable evasion pool - activates counterattacks)
    public int Parry;
    private Vector3 parry;
    public float CurrentParry { get { return parry.x; } set { parry.x = value; } }
    public float MaxParry { get { return parry.y; } set { parry.y = value; } }
    public float TempParry { get { return parry.z; } set { parry.z = value; } }
    public float ParryRecharge;
    private Vector2 parryRecharge;
    public float ParryRechargeRate { get { return parryRecharge.x; } set { parryRecharge.x = value; } }
    public float ParryRechargeMultiplier { get { return parryRecharge.y; } set { parryRecharge.y = value; } }

    //Dodge (Rechargable evasion pool - activates defensive abilities)
    public int Dodge;
    private Vector3 dodge;
    public float CurrentDodge { get { return dodge.x; } set { dodge.x = value; } }
    public float MaxDodge { get { return dodge.y; } set { dodge.y = value; } }
    public float TempDodge { get { return dodge.z; } set { dodge.z = value; } }
    public float DodgeRecharge;
    private Vector2 dodgeRecharge;
    public float DodgeRechargeRate { get { return dodgeRecharge.x; } set { dodgeRecharge.x = value; } }
    public float DodgeRechargeMultiplier { get { return dodgeRecharge.y; } set { dodgeRecharge.y = value; } }

    // Block (Relatively large rechargable evasion pool)
    public int Block;
    private Vector3 block;
    public float CurrentBlock { get { return block.x; } set { block.x = value; } }
    public float MaxBlock { get { return block.y; } set { block.y = value; } }
    public float TempBlock { get { return block.z; } set { block.z = value; } }
    public float BlockRecharge;
    private Vector2 blockRecharge;
    public float BlockRechargeRate { get { return blockRecharge.x; } set { blockRecharge.x = value; } }
    public float BlockRechargeMultiplier { get { return blockRecharge.y; } set { blockRecharge.y = value; } }

    // Barrier (Non-rechargable evasion pool)
    public int Barrier;
}

public enum AbilityScore
{
    Strength,
    Constitution,
    Dexterity,
    Intelligence,
    Mind,
    Willpower
};

/// <summary>
/// ABILITY SCORES
/// Natural [Score] + Temp [Score] = Total Score
/// </summary>
[System.Serializable]
public struct AbilityScores
{
    // Strength (Physical power & defense)
    public int Strength;
    private Vector2 strength;
    public int NaturalStrength { get { return (int)strength.x; } set { strength.x = value; } }
    public int TempStrength { get { return (int)strength.y; } set { strength.y = value; } }

    // Constitution (Physical stamina & resistance)
    public int Constitution;
    private Vector2 constitution;
    public int NaturalConstitution { get { return (int)constitution.x; } set { constitution.x = value; } }
    public int TempConstitution { get { return (int)constitution.y; } set { constitution.y = value; } }

    // Dexterity (Physical evasion & accuracy)
    public int Dexterity;
    private Vector2 dexterity;
    public int NaturalDexterity { get { return (int)dexterity.x; } set { dexterity.x = value; } }
    public int TempDexterity { get { return (int)dexterity.y; } set { dexterity.y = value; } }

    // Intelligence (Magical power & control)
    public int Intelligence;
    private Vector2 intelligence;
    public int NaturalIntelligence { get { return (int)intelligence.x; } set { intelligence.x = value; } }
    public int TempIntelligence { get { return (int)intelligence.y; } set { intelligence.y = value; } }

    // Serenity (Magical resistance & support)
    public int Mind;
    private Vector2 mind;
    public int NaturalSerenity { get { return (int)mind.x; } set { mind.x = value; } }
    public int TempMind { get { return (int)mind.y; } set { mind.y = value; } }

    // Willpower (Magical stamina)
    public int Willpower;
    private Vector2 willpower;
    public int NaturalWillpower { get { return (int)willpower.x; } set { willpower.x = value; } }
    public int TempWillpower { get { return (int)willpower.y; } set { willpower.y = value; } }
}

public struct AttackEffect
{
    public int Accuracy;
    public DamageSource[] DamageSources;
}

public enum DamageType
{
    Slashing,
    Piercing,
    Bludgeoning,
    Force,
    Fire,
    Frost,
    Electric,
    Acidic,
    Radiant,
    Poison,
    Necrotic
};

public struct DamageSource
{
    public int Power;
    public DamageType Type;
}

public enum CharacterClassName
{
    Barbarian,
    Champion,
    Cleric,
    Druid,
    Duelist,
    Engineer,
    Hoplite,
    Illusionist,
    Pugilist,
    Ranger,
    Rogue,
    Sentinel,
    Soldier,
    Sorcerer,
    Warlock,
    Wizard
};

[System.Serializable]
public struct CharacterClass
{
    public CharacterClassName ClassName;
    public float ACM;
    public float PDM;
    public float GBM;
    public float MDM;
    public float MHM;
    public float CRM;
    public float CBM;
    public float PRM;
    public float DRM;
    public float BRM;
    public float HPM;
    public float MPM;
    public AbilityScore BaseDamageAttribute;
    public AbilityScore AccuracyAttribute;
    public AbilityScore CriticalChanceAttribute;
    public AbilityScore CriticalBonusAttribute;
}
