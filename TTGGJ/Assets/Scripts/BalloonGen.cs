using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGen : MonoBehaviour
{
    [SerializeField] float SpawnLimitXLeft, SpawnLimitXRight;
    float timer;

    public GameObject BalloonPrefabOne;
    public GameObject BalloonPrefabTwo;

    private void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= 0.75f)
        {
            SpawnBalloon();
            timer = 0;
        }
    }

    public void SpawnBalloon()
    {
        Vector2 spawnPos = new Vector2(Random.Range(SpawnLimitXLeft, SpawnLimitXRight), -5.5f);
        Instantiate(BalloonPrefabOne, spawnPos, transform.rotation);

        spawnPos = new Vector2(Random.Range(SpawnLimitXLeft, SpawnLimitXRight), -5.5f);
        Instantiate(BalloonPrefabTwo, spawnPos, transform.rotation);
    }
}
