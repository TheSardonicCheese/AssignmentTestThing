using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BattleManager : MonoBehaviour
{
    public List<GameObject> enemyList;

    public List<GameObject> enemySpawnList;

    //event to look at battle UI script
    public event System.Action<bool, float> UpdateHealth;

    
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

    private GameObject gameManager;

    private GameObject battleUIManager;

    private void Awake()
    {
        //sub to battle ui manager
        battleUIManager = GameObject.FindGameObjectWithTag("BattleUIManager");
        battleUIManager.GetComponent<BattleUIManager>().CallAttack += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallDefend += CheckCombatState;
        battleUIManager.GetComponent<BattleUIManager>().CallHeal += CheckCombatState;
        //you would need to probaly have an enum called player decisin whciwould keep track
        //of what buttonwas pressed then call check combat stae using that
        //ont the players turn, automaticallyu run during the enemies turn but turn it back to maual
        //during the players turn (use coroutines and bools to do that)
    }
    void Start()
    {
       
        foreach (GameObject Enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemyList.Add(Enemy);
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        SpawnEnemy();

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
        enemySpawnList.Remove(enemyToRemove);
        Destroy(enemyToRemove);
    }

    public void SpawnEnemy()
    {
        //Spawning an enemy using the size of the list as the maximum of the random range
        if(enemySpawnList.Count > 0)
        {
            enemyObj = Instantiate(enemySpawnList[Random.Range(0, enemySpawnList.Count)], transform);
            Debug.Log(" An enemy appeard!");

        }
        else
        {
            combatState = CombatState.victory;
        }
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
                    Debug.Log("Enemy Defeated");
                    RemoveEnemy(enemyObj);
                    SpawnEnemy();
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
                //give xp before leaving the scene.
                gameManager.GetComponent<GameManager>().TravelToWorld(GameManager.Worlds.OverWorld);
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
        float percentage = defender.GetComponent<Stats>().health / defender.GetComponent<Stats>().maxHealth;
        UpdateHealth(combatState == CombatState.playerTurn, percentage);
        Debug.Log(percentage);
    }
    IEnumerator battleGo()
    {
        CheckCombatState();
        yield return new WaitForSeconds(1f);
        doBattle = true;

    }
}
