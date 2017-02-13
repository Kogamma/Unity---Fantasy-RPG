using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleTon {

    public static PlayerSingleTon instance
    {
        get
        {
            if (m_instance == null)
                m_instance = new PlayerSingleTon();

            return m_instance;
        }
    }

    private static PlayerSingleTon m_instance;

    public int strength;
    public int intelligencest;
    public int dexterity;
    public int luck;
    public int health;
    public int mana;
    public int exp;
}
