using UnityEngine;
using System.Collections;

public class PlayerAttacker : MonoBehaviour
{
    private bool mAttacking;
    private string mAttackTypeString;
    public Animator AttackRoot;

    public float MaxAttack = 20f;
    public float MaxDamage = 20f;

	// Use this for initialization
	void Start ()
    {
        mAttacking = false;

        // Temporary (This would eventually vary based on the weapon style equipped)
        mAttackTypeString = "attack_soldier_01";
	}
	
	// Update is called once per frame
	void Update ()
    {
        bool attackInput = Input.GetMouseButton(0);

        if (attackInput != mAttacking)
        {
            mAttacking = attackInput;
            AttackRoot.Play(mAttacking ? mAttackTypeString : "attack_idle");
        }
	}
}
