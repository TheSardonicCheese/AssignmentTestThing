using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> EnemySpawnList;
    public List<GameObject> EnemiesToFight;
    List<int> storedPlayerStats;
    Transform storedPlayerTransform;

    public enum Worlds
    {
        OverWorld,
        BattleStage
    }

    private static GameManager gameManRef;

    //void awke is called before void start on ANY  OBJECT
    private void Awake()
    {
        if(gameManRef == null)
        {
            gameManRef = this;
            //this will make it so we can travel between scenes (good for keeping track of gameplay!)
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
     
    }

    private void Start()
    {
        LoadPlayerStuff(true);
    }

    public void TravelToWorld(Worlds destination)
    {
        switch (destination)
        {
            case Worlds.OverWorld:
                //load overworld scene
                SavePlayerStuff(false);
                SceneManager.LoadScene("Overworld");
                LoadPlayerStuff(false);
                break;
            case Worlds.BattleStage:
                //load battle scene
                GenerateEnemies();
                SavePlayerStuff(true);
                SceneManager.LoadScene("BattleScene");
                LoadPlayerStuff(true);
                break;
        }
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            //add random enemies to fight from our list, this will run each time we enter wild grass!
            EnemiesToFight.Add(EnemySpawnList[Random.Range(0, EnemySpawnList.Count)]);
        }
    }

    void SavePlayerStuff(bool isFromOverworld)
    {
       
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (isFromOverworld)
        {
            PlayerPrefs.SetFloat("playerPosX", playerObj.transform.position.x);
            PlayerPrefs.SetFloat("playerPosY", playerObj.transform.position.y);
            PlayerPrefs.SetFloat("playerPosZ", playerObj.transform.position.z);
            PlayerPrefs.SetFloat("playerRotX", playerObj.transform.rotation.x);
            PlayerPrefs.SetFloat("playerRotY", playerObj.transform.rotation.y);
            PlayerPrefs.SetFloat("playerRotZ", playerObj.transform.rotation.z);
        }
      


        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

        PlayerPrefs.SetFloat("playerHealth", playerStats.health);
        PlayerPrefs.SetFloat("playerCurrentExp", playerStats.curExp);
        PlayerPrefs.SetFloat("playerLevel", playerStats.level);
    }
    void LoadPlayerStuff(bool goingToOverworld)
    {
        Stats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
        playerStats.health = PlayerPrefs.GetFloat("playerHealth", playerStats.maxHealth);
        playerStats.curExp = PlayerPrefs.GetInt("playerCurExp", 0);
        playerStats.level = PlayerPrefs.GetInt("playerLevel", 1);
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (goingToOverworld)
        {
            //public transform spawnPos
            //change 0fs to spawnPos.x spawnPos.y etc
            playerObj.transform.position = new Vector3(PlayerPrefs.GetFloat("playerPosX", 0f),
            PlayerPrefs.GetFloat("playerPosY", 0f),
                                                       PlayerPrefs.GetFloat("playerPosZ", 0f));
            playerObj.transform.rotation = Quaternion.Euler(PlayerPrefs.GetFloat("playerRotX", 0f),
                                                            PlayerPrefs.GetFloat("playerRotY", 0f),
                                                            PlayerPrefs.GetFloat("playerRotZ", 0f));

            PlayerPrefs.GetFloat("playerPosY", playerObj.transform.position.y);
            PlayerPrefs.GetFloat("playerPosZ", playerObj.transform.position.z);
            PlayerPrefs.GetFloat("playerRotX", playerObj.transform.rotation.x);
            PlayerPrefs.GetFloat("playerRotY", playerObj.transform.rotation.y);
            PlayerPrefs.GetFloat("playerRotZ", playerObj.transform.rotation.z);
        }
    }
    void DeleteSavedStuff()
    {
        //hard reset
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Overworld");
    }
}

