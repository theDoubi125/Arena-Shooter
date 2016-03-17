using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Assets.GameTree.Functions;

public class SpawnManager : MonoBehaviour 
{	
	protected List<SpawnPoint> spawnPointList;
	protected Queue<SpawnQuery> spawnQueryQueue;
	
	private static System.Random rnd = new System.Random();
	
	protected class SpawnQuery
    {
		public string type;
		public SpawnEnemyFunction function;
        public int health;
		
		public SpawnQuery(string type, SpawnEnemyFunction function, int health)
        {
            this.type = type;
            this.function = function;
            this.health = health;
        }
    } 

	
	void Start() {	
        spawnPointList = new List<SpawnPoint>(gameObject.GetComponentsInChildren<SpawnPoint>());
		spawnQueryQueue = new Queue<SpawnQuery>();
	}
	
	void Update() {	
		if(Time.timeScale > 0.001)
			Spawn();
	}
	
	public void Reset() 
	{	
		spawnQueryQueue.Clear();
	}
	
	public void AddToQueue(string type, SpawnEnemyFunction function, int health)
	{
		spawnQueryQueue.Enqueue(new SpawnQuery(type, function, health));
	}
	
	public void Spawn()
	{
		while (spawnQueryQueue.Count > 0)
		{
			this.SpawnAnEnemy(spawnQueryQueue.Dequeue());
		}
	}
	
	private void SpawnAnEnemy(SpawnQuery query)
	{
		GameObject prefab = PrefabHolder.Instance.GetEnemyPrefabFromType(query.type);
		
		int spawnPointCount = spawnPointList.Count;
		if(prefab != null && spawnPointCount > 0)
		{
			SpawnPoint point = spawnPointList[rnd.Next(spawnPointCount)];
			
			GameObject spawnedObject = (GameObject) GameObject.Instantiate(prefab, point.transform.position, point.transform.rotation);	
			spawnedObject.tag = "Enemy";
			
			LivingEntity spawnedEntity = spawnedObject.GetComponent<LivingEntity>();			
			if(spawnedEntity != null)
            {
                spawnedEntity.SetSpawnEnemyFunction(query.function);
                if (query.health != 0)
                    spawnedEntity.Health = query.health;
            }
		}
		else
			Debug.Log("Can't instantiate the enemy prefab.");
	}
}
