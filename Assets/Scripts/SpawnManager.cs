using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> lsMonsters = new List<GameObject>();
    public float timerSpawn = 1;
    public float timerCountdown = 3;
    public bool isStartGame = false;

    void Update()
    {
        timerCountdown -= Time.deltaTime;
        if (isStartGame == true && timerCountdown <= 0)
        {
            timerCountdown = timerSpawn;
            int randomIndex = Random.Range(0, lsMonsters.Count);
            GameObject goRandomMonster = Instantiate(lsMonsters[randomIndex], transform);
            float randomPositionX = Random.Range(-2.5f, 2.5f);
            goRandomMonster.transform.localPosition = new Vector3(randomPositionX, 0, 0);
        }
    }
}
