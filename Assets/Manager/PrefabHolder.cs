using UnityEngine;
using System.Collections;

public class PrefabHolder : MonoBehaviour
{
    private static PrefabHolder s_Instance;
    public static PrefabHolder Instance
    {
        get { return s_Instance; }
    }

    public GameObject BasicEnemy;
    public GameObject ShooterEnemy;

    public void Awake()
    {
        s_Instance = this;
    }
}
