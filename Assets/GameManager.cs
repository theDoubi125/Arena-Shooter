using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	public UIManager uiManager = null;
	public SpawnManager spawnManager = null;

	void Start () {
	}
	
	void Update () {
	}
	
	public void Reset()
	{
		if(spawnManager != null)
			spawnManager.Reset();
		else
			Debug.Log("Warning : spawnManager null in GameManager");
	}
	public void Spawn()
	{
	}
	
	public void GameOver()
	{
		if(this.uiManager != null)
			this.uiManager.SetGameOverMenuVisibility(true);
	}
	public void WaveWon()
	{
		if(this.uiManager != null)
			this.uiManager.SetWaveWonMenuVisibility(true);
	}
}
