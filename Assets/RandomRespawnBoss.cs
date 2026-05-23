using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRespawnBoss : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public static float timebetween;
    public float starttimebetween;
    public static int mHI;

    // Start is called before the first frame update
    void Start()
    {

        timebetween = starttimebetween;
    }

    // Update is called once per frame
    void Update()
    {
        if (timebetween <= 0)
        {
            timebetween = starttimebetween;

            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(enemyPrefabs[0], spawnPoints[randSpawnPoint].position, spawnPoints[randSpawnPoint].rotation);
            mHI = mHI + 150;
        }

        else
        {
            timebetween -= Time.deltaTime;
        }
    }


}
