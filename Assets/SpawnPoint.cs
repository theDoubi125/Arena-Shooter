using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour 
{
	public GameObject toBeSpawned = null;

	void Start () {	
	}
	
	void Update () {	
	}
	
	public void Spawn()
	{
		Debug.Log("1");
		if(toBeSpawned != null)
			GameObject.Instantiate(toBeSpawned, this.transform.position, this.transform.rotation);
	}
}
