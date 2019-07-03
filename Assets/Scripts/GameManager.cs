using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemyList;

    public List<GameObject> enemySpawnList;

    

    
    public enum GameState
    {
        notInCombat,
        inCombat
    }
    public GameState gameState;

    public enum CombatState
    {
        playerTurn,
        enemyTurn,
        victory,
        loss
    }
    public CombatState combatState;

    //objects for combat
    public GameObject playerObj;
    public GameObject enemyObj;

    private bool doBattle = true;


    void Start()
    {
       
        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(Enemy);
        }

    }

    void Update()
    {
        if(doBattle)
        {
            //set who goes first by comparing speeds
            StartCoroutine(battleGo());
            doBattle = false;
        }
    }

    public void DamageEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health -= 10;
        }
    }

    public void HealEnemies()
    {
        foreach (GameObject enemy in enemyList)
        {
            enemy.GetComponent<Stats>().health += 10;
        }
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
        enemySpawnList.Remove(enemyToRemove);
    }

    public void SpawnEnemy()
    {
        //Spawning an enemy using the size of the list as the maximum of the random range
        Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
        Debug.Log(" An enemy appeard!");
    }

    public void CheckCombatState()
    {
        switch (combatState)
        {
            //Player Turn
            case CombatState.playerTurn:
                //decision - attack
                //attack the enemy
                BattleRound(playerObj, enemyObj);
                //check if enemy is defeated
                if (enemyObj.GetComponent<Stats>().isDefeated)
                {
                    //need to remove enemy
                    combatState = CombatState.victory;
                    break;
                }
                    
                //next case, usually enemy turn
                combatState = CombatState.enemyTurn;
                break;


                //Enemy Turn
            case CombatState.enemyTurn:
                //decision - attack
                //attack the player
                BattleRound(enemyObj, playerObj);
                //check if player is defeated
                if (playerObj.GetComponent<Stats>().isDefeated)
                {
                    //go to lose screen
                    combatState = CombatState.loss;
                    break;
                }
                //next case, usually player turn
                combatState = CombatState.playerTurn;
                break;

            case CombatState.victory:
                //we won
                Debug.Log("Enemy Defeated");
                GetComponent<XPandLvlUp>().XPUp(enemyObj);
                RemoveEnemy(enemyObj);
                SpawnEnemy();
                break;

            case CombatState.loss:
                //we lose, restart
                SceneManager.LoadScene("GameOver");
                break;

               


                //Victory
                //tell player they won
                //end game

                //loss
                //tell player they lost
                //restart game
        }
    }

    public void BattleRound(GameObject attacker, GameObject defender)
    {
        //makes the fight happen
        defender.GetComponent<Stats>().Attacked(attacker.GetComponent<Stats>().attack, Stats.StatusEffect.none);
        Debug.Log(attacker.name +
            " attacks " +
            defender.name +
            " for " +
            (attacker.GetComponent<Stats>().attack - (attacker.GetComponent<Stats>().attack * (100/( defender.GetComponent<Stats>().defense + 100)))) + 
            " damage");
    }
    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;

    }
}
