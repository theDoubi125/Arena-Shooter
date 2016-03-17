using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager = null;
    public SpawnManager spawnManager = null;

    public void Reset()
    {
        if (spawnManager != null)
        {
            LivingEntity[] ennemies = FindObjectsOfType<LivingEntity>();
            foreach (LivingEntity ennemy in ennemies)
            {
                if (ennemy.tag != "Player")
                    Destroy(ennemy.gameObject);
            }
            uiManager.SetWaveLostMenuVisibility(false);
            uiManager.SetWaveWonMenuVisibility(false);
            spawnManager.Reset();
        }
        else
            Debug.Log("Warning : spawnManager null in GameManager");
    }

    public void WaveWon(bool success)
    {
        if (this.uiManager != null)
        {
            if (success)
                this.uiManager.SetWaveWonMenuVisibility(true);
            else
                this.uiManager.SetWaveLostMenuVisibility(true);
        }
    }
}
