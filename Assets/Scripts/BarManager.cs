using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour
{
    public Image healthBar;
    public Image manaBar;
    public Text healthText;
    public Text manaText;
	
	void Update ()
    {
        healthBar.fillAmount = (float)((float)PlayerSingleton.instance.playerHealth / (float)PlayerSingleton.instance.playerMaxHealth);

        manaBar.fillAmount = (float)((float)PlayerSingleton.instance.playerMana / (float)PlayerSingleton.instance.playerMaxMana);

        healthText.text = PlayerSingleton.instance.playerHealth + "/" + PlayerSingleton.instance.playerMaxHealth;
        manaText.text = PlayerSingleton.instance.playerMana + "/" + PlayerSingleton.instance.playerMaxMana;
    }

    public void ChangeHealth(int change)
    {
        PlayerSingleton.instance.playerHealth += change;
    }

    public void ChangeMana(int change)
    {
        PlayerSingleton.instance.playerMana += change;
    }
}
