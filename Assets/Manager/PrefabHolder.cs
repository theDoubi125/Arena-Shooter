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
    public GameObject BossEnemy;

    public void Awake()
    {
        s_Instance = this;
    }
	
	public GameObject GetEnemyPrefabFromType(string type)
	{
		if(type.Equals("BasicEnemy"))
			return BasicEnemy;
		else if(type.Equals("ShooterEnemy") )
			return ShooterEnemy;
		else
			return null;
	}
}
