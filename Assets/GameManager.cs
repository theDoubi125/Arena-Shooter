using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager = null;
    public SpawnManager spawnManager = null;

    private static GameManager s_Instance = null;
    public static GameManager Instance { get { return s_Instance; } }

    void Awake()
    {
        s_Instance = this;
    }

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
                {
                    entity.Reset();
                    entity.GetComponent<PlayerController>().Reset();
                }
            }

            Projectile[] projectiles = FindObjectsOfType<Projectile>();
            foreach (Projectile proj in projectiles)
            {
                Destroy(proj.gameObject);
            }

            uiManager.SetWaveLostMenuVisibility(false);
            uiManager.SetWaveWonMenuVisibility(false);

            spawnManager.Reset();
        }
        else
            Debug.Log("Warning : spawnManager null in GameManager");
    }

    public void WaveComplete(bool success)
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
