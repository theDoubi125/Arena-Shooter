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
	
	public void WaveWon(bool success)
	{
		if(this.uiManager != null)
		{
			if(success)
				this.uiManager.SetWaveWonMenuVisibility(true);	
			else		
				this.uiManager.SetWaveLostMenuVisibility(true);
		}
	}
}
