using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool isVisible()
    {
        GameObject waveLostMenuGO = GameObject.Find("WaveLostCanvas");
        GameObject waveWonMenuGO = GameObject.Find("WaveWonCanvas");
        return waveLostMenuGO != null && waveLostMenuGO.GetComponent<Canvas>().enabled
            || waveWonMenuGO != null && waveWonMenuGO.GetComponent<Canvas>().enabled;
    }
	
	public void SetWaveLostMenuVisibility(bool visibility)
	{
		GameObject waveLostMenuGO = GameObject.Find("WaveLostCanvas");
		
		if(waveLostMenuGO != null)
		{
			if (visibility)
			{
				waveLostMenuGO.GetComponent<Canvas>().enabled = true;
				Time.timeScale = 0f;
			}
			else
			{
				waveLostMenuGO.GetComponent<Canvas>().enabled = false;
				Time.timeScale = 1.0f;
			}			
		}
	}	
	public void SetWaveWonMenuVisibility(bool visibility)
	{
		GameObject waveWonMenuGO = GameObject.Find("WaveWonCanvas");
		
		if(waveWonMenuGO != null)
		{
			if (visibility)
			{
				waveWonMenuGO.GetComponent<Canvas>().enabled = true;
				Time.timeScale = 0f;
			}
			else
			{
				waveWonMenuGO.GetComponent<Canvas>().enabled = false;
				Time.timeScale = 1.0f;
			}			
		}
	}	
}
