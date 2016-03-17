﻿using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetGameOverMenuVisibility(bool visibility)
	{
		GameObject gameOverMenuGO = GameObject.Find("GameOverCanvas");
		
		if(gameOverMenuGO != null)
		{
			if (visibility)
			{
				gameOverMenuGO.GetComponent<Canvas>().enabled = true;
				Time.timeScale = 0f;
			}
			else
			{
				gameOverMenuGO.GetComponent<Canvas>().enabled = false;
				Time.timeScale = 1.0f;
			}			
		}
	}	
}