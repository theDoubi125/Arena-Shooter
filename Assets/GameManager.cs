using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour 
{
	public UIManager uiManager = null;
	public List<SpawnPoint> spawnList = new List<SpawnPoint>();

	void Start () {
		Spawn();
	}
	
	void Update () {
	
	}
	
	public void Spawn()
	{
		foreach(SpawnPoint spawn in spawnList)
			spawn.Spawn();
	}
	public void GameOver()
	{
		if(this.uiManager != null)
			this.uiManager.SetGameOverMenuVisibility(true);
	}
}
