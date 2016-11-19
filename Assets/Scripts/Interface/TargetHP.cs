using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetHP : MonoBehaviour
{
    public PlayerTarget Host;

    public Image HealthBar;
    public Text HealthText;
    public Image ParryBar;
    public Text ParryText;
    public Image DodgeBar;
    public Text DodgeText;
    public Image BlockBar;
    public Text BlockText;
    

    private float mMaxBarWidth;

	// Use this for initialization
	void Start ()
    {
        mMaxBarWidth = HealthBar.rectTransform.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 delta = HealthBar.rectTransform.sizeDelta;
        delta.x = (Host.HP.CurrentHealth / Host.HP.MaxHealth) * mMaxBarWidth;
        HealthBar.rectTransform.sizeDelta = delta;
        HealthText.text = Host.HP.CurrentHealth.ToString();

        delta = ParryBar.rectTransform.sizeDelta;
        delta.x = (Host.HP.CurrentParry / Host.HP.MaxParry) * mMaxBarWidth;
        ParryBar.rectTransform.sizeDelta = delta;
        ParryText.text = Host.HP.CurrentParry.ToString();

        delta = DodgeBar.rectTransform.sizeDelta;
        delta.x = (Host.HP.CurrentDodge / Host.HP.MaxDodge) * mMaxBarWidth;
        DodgeBar.rectTransform.sizeDelta = delta;
        DodgeText.text = Host.HP.CurrentDodge.ToString();

        // Most characters' maximum block will be 0. If so, don't try to perform division on it.
        if (Host.HP.MaxBlock != 0)
        {
            delta = BlockBar.rectTransform.sizeDelta;
            delta.x = (Host.HP.CurrentBlock / Host.HP.MaxBlock) * mMaxBarWidth;
            BlockBar.rectTransform.sizeDelta = delta;
            BlockText.text = Host.HP.CurrentBlock.ToString();
        }
        else
            BlockBar.transform.parent.gameObject.SetActive(false);
        

        //delta = GuardBar.rectTransform.sizeDelta;
        //float currentGuard = (Host.HP.CurrentParry + Host.HP.CurrentDodge + Host.HP.CurrentBlock);
        //float maxGuard = (Host.HP.MaxParry + Host.HP.MaxDodge + Host.HP.MaxBlock);
        //delta.x = (currentGuard / maxGuard) * mMaxBarWidth;
        //GuardBar.rectTransform.sizeDelta = delta;
    }
}
