using UnityEngine;
using System.Collections;

public class PlayerTarget : MonoBehaviour
{
    public CharacterClassName Class;
    private CharacterClass mCharacterClass;

    public AbilityScores AbilityScores;

    public int Level = 1;
    public int Armor = 20;

    public HitPoints HP;
    public float GuardRechargeDelay = 5f;
    

	// Use this for initialization
	void Start ()
    {
        Debug.Log(CharacterClassLibrary.Instance.name);
        mCharacterClass = CharacterClassLibrary.Instance.CharacterClassDictionary[Class];
        HP.MaxHealth = 10 + (mCharacterClass.HPM * AbilityScores.Constitution * Level);
        HP.CurrentHealth = HP.MaxHealth;

        HP.MaxArmor = Armor;
        HP.CurrentArmor = HP.MaxArmor;

        int attackAttribute = 1;
        switch (mCharacterClass.AccuracyAttribute)
        {
            case AbilityScore.Strength:
                attackAttribute = AbilityScores.Strength;
                break;
            case AbilityScore.Dexterity:
                attackAttribute = AbilityScores.Dexterity;
                break;
            case AbilityScore.Intelligence:
                attackAttribute = AbilityScores.Intelligence;
                break;
            case AbilityScore.Mind:
                attackAttribute = AbilityScores.Mind;
                break;
            default:
                break;
        }

        HP.MaxParry = mCharacterClass.PRM
            * attackAttribute * Level;
        Debug.Log("Max Parry: " + HP.MaxParry);
        HP.CurrentParry = HP.MaxParry;
        HP.ParryRechargeRate = attackAttribute;
        HP.ParryRechargeMultiplier = 1f;

        HP.MaxDodge = mCharacterClass.DRM
            * AbilityScores.Dexterity * Level;
        HP.CurrentDodge = HP.MaxDodge;
        HP.DodgeRechargeRate = mCharacterClass.DRM;
        HP.DodgeRechargeMultiplier = 1f;

        HP.MaxBlock = mCharacterClass.BRM
            * AbilityScores.Strength * Level;
        HP.CurrentBlock = HP.MaxBlock;
        HP.BlockRechargeRate = mCharacterClass.BRM;
        HP.BlockRechargeMultiplier = 1f;

        // Old
        //HP.MaxHealth = HP.Health;
        //HP.CurrentHealth = HP.Health;

        //HP.MaxArmor = HP.Armor;
        //HP.CurrentArmor = HP.Armor;

        //HP.MaxParry = HP.Parry;
        //HP.CurrentParry = HP.Parry;
        //HP.ParryRechargeRate = HP.ParryRecharge;
        //HP.ParryRechargeMultiplier = 1f;

        //HP.MaxDodge = HP.Dodge;
        //HP.CurrentDodge = HP.Dodge;
        //HP.DodgeRechargeRate = HP.DodgeRecharge;
        //HP.DodgeRechargeMultiplier = 1f;

        //HP.MaxBlock = HP.Block;
        //HP.CurrentBlock = HP.Block;
        //HP.BlockRechargeRate = HP.BlockRecharge;
        //HP.BlockRechargeMultiplier = 1f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // TODO: For some reason these are not recharging properly or not being shown recharging by the GUI

        // Recharge Dodge pool
        if (HP.DodgeRechargeMultiplier < 1f)
        {
            HP.DodgeRechargeMultiplier += Time.deltaTime / GuardRechargeDelay;
        }
        else
        {
            HP.DodgeRechargeMultiplier = 1f;
        }
        //Debug.Log("Dodge recharging by: " + (HP.DodgeRechargeRate * HP.DodgeRechargeMultiplier * Time.deltaTime).ToString());
        HP.CurrentDodge += (HP.DodgeRechargeRate * HP.DodgeRechargeMultiplier * Time.deltaTime);
        //Debug.Log("Current dodge: " + HP.CurrentDodge);

        if (HP.CurrentDodge > HP.MaxDodge) HP.CurrentDodge = HP.MaxDodge;

        // Recharge Parry pool
        if (HP.ParryRechargeMultiplier < 1f) HP.ParryRechargeMultiplier += Time.deltaTime / GuardRechargeDelay;
        else HP.ParryRechargeMultiplier = 1f;
        HP.CurrentParry += (HP.ParryRechargeRate * HP.ParryRechargeMultiplier * Time.deltaTime);
        if (HP.CurrentParry > HP.MaxParry) HP.CurrentParry = HP.MaxParry;

        // Recharge Block pool
        if (HP.BlockRechargeMultiplier < 1f) HP.BlockRechargeMultiplier += Time.deltaTime / GuardRechargeDelay;
        else HP.BlockRechargeMultiplier = 1f;
        HP.CurrentBlock += (HP.BlockRechargeRate * HP.BlockRechargeMultiplier * Time.deltaTime);
        if (HP.CurrentBlock > HP.MaxBlock) HP.CurrentBlock = HP.MaxBlock;
    }

    void OnTriggerEnter(Collider _other)
    {
        AttackCollider attack = _other.GetComponent<AttackCollider>();
        if (attack != null)
        {
            // TODO: Probably store these values in the attack itself to avoid having to reference the player
            float attackAmt = Random.Range(0f, attack.Player.MaxAttack);
            float attackDmg = Random.Range(0f, attack.Player.MaxDamage);
            //Debug.Log("Attack: " + attackAmt);

            // Evaluate attack power against all HP (Guard) pools and subtract accordingly
            // Any HP pool that is hit by an attack has its recharge rate reset to zero
            if (attackAmt <= HP.CurrentBlock)
            {
                //Debug.Log("Block: " + attackDmg);
                HP.CurrentBlock -= attackDmg;
                HP.BlockRechargeMultiplier = 0f;
                return;
            } attackAmt -= HP.CurrentBlock;
            if (attackAmt <= HP.CurrentDodge)
            {
                //Debug.Log("Dodge: " + attackDmg);
                HP.CurrentDodge -= attackDmg;
                HP.DodgeRechargeMultiplier = 0f;
                return;
            } attackAmt -= HP.CurrentDodge;
            if (attackAmt <= HP.CurrentParry)
            {
                //Debug.Log("Parry: " + attackDmg);
                HP.CurrentParry -= attackDmg;
                HP.ParryRechargeMultiplier = 0f;
                return;
            } attackAmt -= HP.CurrentParry;

            // Evaluate remaining attack power against Armor
            float armorBypassChance = attackAmt / (attackAmt + HP.CurrentArmor);
            if (Random.Range(0f, 1f) >= armorBypassChance)
            {
                //Debug.Log("Armor");
                return;
            }

            // Subtract HP
            //Debug.Log("Damage: " + attackDmg);
            HP.CurrentHealth -= attackDmg;
        }
    }

    private void updateClassModifiers()
    {
        mCharacterClass = CharacterClassLibrary.Instance.CharacterClassDictionary[Class];
        HP.MaxHealth = 10 + (mCharacterClass.HPM * AbilityScores.Constitution * Level);

        HP.MaxArmor = Armor;

        int attackAttribute = 1;
        switch (mCharacterClass.AccuracyAttribute)
        {
            case AbilityScore.Strength:
                attackAttribute = AbilityScores.Strength;
                break;
            case AbilityScore.Dexterity:
                attackAttribute = AbilityScores.Dexterity;
                break;
            case AbilityScore.Intelligence:
                attackAttribute = AbilityScores.Intelligence;
                break;
            case AbilityScore.Mind:
                attackAttribute = AbilityScores.Mind;
                break;
            default:
                break;
        }

        HP.MaxParry = mCharacterClass.PRM
            * attackAttribute * Level;
        HP.ParryRechargeRate = attackAttribute;
        HP.ParryRechargeMultiplier = 1f;

        HP.MaxDodge = mCharacterClass.DRM
            * AbilityScores.Dexterity * Level;
        HP.DodgeRechargeRate = mCharacterClass.DRM;
        HP.DodgeRechargeMultiplier = 1f;

        HP.MaxBlock = mCharacterClass.BRM
            * AbilityScores.Strength * Level;
        HP.BlockRechargeRate = mCharacterClass.BRM;
        HP.BlockRechargeMultiplier = 1f;
    }
}
