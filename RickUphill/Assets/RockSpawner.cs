using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{

	public float MinSpawnTime, MaxSpawnTime, SpawnTimeReductionInterval;
	public GameObject RockPrefab;
	public Transform SpawnPos;
	public Vector3 ForceDir;
	private float AbsMinSpawnTime = 0.5f;
	private float SpawnTimeReductionAmount = 0.15f;

	void Awake()
	{
		if (Game.Data != null)
			Game.Data.OnGameOver += StopSpawning;
	}
	
	void Start ()
	{
		StartCoroutine("SpawnRock");
		StartCoroutine(ReduceSpawnTime());
	}

	private IEnumerator ReduceSpawnTime()
	{
		yield return new WaitForSeconds(SpawnTimeReductionInterval);
		if (MinSpawnTime <= AbsMinSpawnTime) yield break;
		MinSpawnTime -= SpawnTimeReductionAmount;
		MaxSpawnTime -= SpawnTimeReductionAmount;
		StartCoroutine(ReduceSpawnTime());
	}

	public void StopSpawning()
	{
		StopCoroutine("SpawnRock");
	}

	IEnumerator SpawnRock()
	{
		var interval = Random.Range(MinSpawnTime, MaxSpawnTime);
		yield return new WaitForSeconds(interval);
		var thisRock = Instantiate(RockPrefab, SpawnPos.position, Quaternion.identity);
		thisRock.GetComponent<Rigidbody2D>().AddForce(ForceDir);
		StartCoroutine("SpawnRock");
	}
}
