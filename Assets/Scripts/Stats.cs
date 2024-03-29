﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float maxHealth;
    public float health;
    public int speed;
    public int attack;
    public int defense;
    public int luck;
    public int curExp;
    public int level;
    public bool isDefeated;

    public enum StatusEffect
    {
        none,
        dizzy,
        poisoned,
        stunned

    }

    public StatusEffect myStatus;
    public StatusEffect attackEffect;

    //public int health2, speed2, attack1, defense2, luck2

    public void Attacked(int incDmg, StatusEffect incEffect)
    {
        health -= incDmg - (incDmg * (100 / (defense + 100)));
        myStatus = incEffect;
        if (health <= 0)
            isDefeated = true;
    }
}