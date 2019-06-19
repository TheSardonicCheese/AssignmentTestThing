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
			XPRequired = CurrentLevel * CurrentLevel + 3;
		}
		else
		{
			XPRequired = CurrentLevel * 3 + 4;
		}

		if(Input.GetKeyDown("x"))
		{
			//placeholder if statement until defeat small enemy function works
			TotalXP += .5f;
			print("+.5 xp");
		}
		if(Input.GetKeyDown("c"))
		{
			//placeholder if statement until defeat medium enemy function works
			TotalXP += 2.5f;
			print("+2.5 xp");
		}
		if(Input.GetKeyDown("z"))
		{
			//placeholder if statement until defeat large enemy function works
			TotalXP += 5f;
			print("+5 xp");
		}
		if(TotalXP >= XPRequired)
		{
			CurrentLevel ++;
			Debug.Log("Level Up!");
		}
	}
}
