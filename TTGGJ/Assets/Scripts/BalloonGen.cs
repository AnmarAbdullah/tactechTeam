using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BalloonGen : MonoBehaviour
{
    [SerializeField] float SpawnLimitXLeft, SpawnLimitXRight;
    float timer;

    public GameObject BalloonPrefabOne;
    public GameObject BalloonPrefabTwo;

    public GameManager gmanager;

    private void Update()
    {
        if (!gmanager.GameOn) return;


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
        GameObject bOne = Instantiate(BalloonPrefabOne, spawnPos, transform.rotation);

        spawnPos = new Vector2(Random.Range(SpawnLimitXLeft, SpawnLimitXRight), -5.5f);
        GameObject bTwo = Instantiate(BalloonPrefabTwo, spawnPos, transform.rotation);


        bOne.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 7.5f);
        bTwo.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 7.5f);
    }
}
