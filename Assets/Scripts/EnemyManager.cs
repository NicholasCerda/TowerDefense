using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Group
{
    public GameObject enemy;
    public float spawnTime;
    public int numberOfEnemies;

    public Group(GameObject enemy, float spawnTime, int numberOfEnemies)
    {
        this.enemy = enemy;
        this.spawnTime = spawnTime;
        this.numberOfEnemies = numberOfEnemies;
    }
}

[System.Serializable]
public struct Wave
{
    public Group[] enemyGroups;
    public float waveDelay;//time before wave spawns,

    public Wave(Group[] enemyGroups,float amt)
    {
        this.enemyGroups = enemyGroups;
        this.waveDelay = amt;
    }
}


public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyA;
    public GameObject EnemyB;
    public float timeToWaitA = 1.2f;
    public float timeToWaitB = 1.5f;
    public float waveTimeRemaining;
    public Wave currentWave;
    public Wave[] waveList;
    public WaypointManager waypointManager;
    public int waveNumber;
    void Start()
    {
        waveNumber = 0;
        Group groupA = new Group(EnemyA, timeToWaitA, 100);//5//Group partOfWave = new Group(enemyType(gameObject that is),time between spawning each unit, number of enemies in of this type in wave);
        Group groupB = new Group(EnemyB, timeToWaitB, 100);//3//Group otherEnemyTypeInWave = new Group (enemyType, spawnRate, count);

        //Group[] groups = new Group[2]{groupA, groupB};//Group[] fullWave = new Group[groupAmount]{groupA,groupB,....};
        currentWave = new Wave(new Group[2] { groupA, groupB },0.0f);//currentWave=new Wave(new G

        SpawnWave(currentWave);
    }

    private void SpawnWave(Wave newWave)
    {
        foreach (Group group in newWave.enemyGroups)
        {
            StartCoroutine(SpawnGroup(group));
        }
    }
    void Update()
    {
        /*
         float diff = waveList[waveNumber].waveDelay-
         if (waveList[waveNumber].waveDelay-
         
         
         
         */
    }
    //private IEnumerable WaveDelay(Group @group)
    //{
    //
    //}

    //private IEnumerator SpawnWave(Wave newWave)
    //{
    //  while (true)
    //  {
    //    yield return (1);
    //  }
    //}

    private IEnumerator SpawnGroup(Group @group)
    {
        while (@group.numberOfEnemies > 0)
        {
            yield return new WaitForSeconds(@group.spawnTime);
            GameObject enemy = Instantiate(@group.enemy);
            enemy.GetComponent<Enemy>().Initialize(waypointManager);
            @group.numberOfEnemies--;
        }
    }
}
