using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharachterStat : MonoBehaviour
{
    public int attack;
    public int speed;
    public int def;
    public int health;
    public int magnet;

    public static CharachterStat instance;
    
    private void Awake()
    {
        instance = this;
    }
}
