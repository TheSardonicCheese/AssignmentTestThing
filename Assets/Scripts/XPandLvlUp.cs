using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPandLvlUp : MonoBehaviour 
{
public int CurrentLevel = 1;
public float TotalXP = 0;
public float XPRequired;

	
	void Update () 
	{
		 
		if (CurrentLevel >= 5)
		{
			XPRequired = CurrentLevel*CurrentLevel + 3;
		}
		else
		{
			XPRequired = CurrentLevel * 3 + 4;
		}

		if(Input.GetKeyDown("x"))
		{
			//placeholder if statement until defeat enemy function works
			TotalXP += 1.5f;
			print("+1.5 xp");
		}
		if(TotalXP >= XPRequired)
		{
			CurrentLevel ++;
			Debug.Log("Level Up!");
		}
	}
}
