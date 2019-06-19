using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesFunctions : MonoBehaviour 
{

	public int PlayerLives = 2;
	float PlayerHealth = 100f;
	bool PlayerAlive = true;
	public string PlayerName = "Player";

	void Start () 
	{

		PlayerLives++;
		
		if(PlayerLives == 3)
		{
			//do stuff
		}
		else if (PlayerLives == 2)
		{
			//do other stuff
		}
		else if (PlayerLives == 1)
		{
			//do thing
		}
		else
		{
			//gameover
		}

	}
	
	void Update () 
	{
		if(PlayerLives > 0)
		{
		DecreaseLives(1);
		}

		if(PlayerLives == 0)
		{
			Debug.Log("Game Over");
		}
	}

	public void DecreaseLives(int IncLives)
	{
		PlayerLives-= IncLives;
		Debug.Log(PlayerName + " has " + PlayerLives + " lives ");

	}
}
