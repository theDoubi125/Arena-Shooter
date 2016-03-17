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
            LivingEntity[] entities = FindObjectsOfType<LivingEntity>();
            foreach (LivingEntity entity in entities)
            {
                if (entity.tag != "Player")
                    Destroy(entity.gameObject);
                else
                    entity.Reset();
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
