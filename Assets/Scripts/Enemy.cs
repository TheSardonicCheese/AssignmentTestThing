﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour

{
    //removing enemies from list
    private GameObject battleManager;

    public Stats myStats;
    //enemy 0 = small
    //enemy 1 = medium
    //enemy 2 = large
    public int enemyID = 1;
    public enum EnemyTypes
    {
        small,
        medium,
        large,
    }

    public EnemyTypes myType;


	// Use this for initialization
	void Start () 
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager");

		myStats = GetComponent<Stats>();

        switch (myType)
        {
            case EnemyTypes.small:
                //do setup
                break;
            case EnemyTypes.medium:
                //do thing
                break;
            case EnemyTypes.large:
                //do thing
                break;

        }

	}

    public void Defeated()
    {
        //removing enemies from list
        battleManager.GetComponent<BattleManager>().RemoveEnemy(gameObject);
    }
}
