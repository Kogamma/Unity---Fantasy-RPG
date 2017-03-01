using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarManager : MonoBehaviour {

    public Image healthBar;
    public Image manaBar;
    public Text healthText;
    public Text manaText;
	
	void Update ()
    {
        PlayerSingleton.instance.playerHealth = Mathf.Clamp(PlayerSingleton.instance.playerHealth, 0, PlayerSingleton.instance.playerMaxHealth);

        PlayerSingleton.instance.playerMana = Mathf.Clamp(PlayerSingleton.instance.playerMana, 0, 10);

        healthBar.fillAmount = (float)((float)PlayerSingleton.instance.playerHealth / (float)PlayerSingleton.instance.playerMaxHealth);

        manaBar.fillAmount = (float)((float)PlayerSingleton.instance.playerMana / (float)10);

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
