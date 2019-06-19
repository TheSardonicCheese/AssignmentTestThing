using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour

{
    Stats myStats;
    //enemy 0 = small
    //enemy 1 = medium
    //enemy 2 = large
    public int enemyID = 1;
    public enum EnemyTypes
    {
        Small,
        Medium,
        Large,
    }

    public EnemyTypes myType;


	// Use this for initialization
	void Start () 
    {
		
		myStats = GetComponent<Stats>();
        switch (myType)
        {
            case EnemyTypes.Small:
                //do setup
                break;
            case EnemyTypes.Medium:
                //do thing
                break;
            case EnemyTypes.Large:
                //do thing
                break;

        }

	}
	
	// Update is called once per frame
	void Update () 
    {
        myStats.health = 45;
    }

    public void Attacked(int incDmg, Stats.StatusEffect incEffect)
    {
        myStats.health -= incDmg - myStats.defense;
        myStats.myStatus = incEffect;
    }

    public void AttackTarget()
    {
        Attacked(myStats.attack,Stats.StatusEffect.none);
    }
}
