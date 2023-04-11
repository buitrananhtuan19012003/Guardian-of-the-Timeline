using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public delegate void CollectMedal(int medal); //Dinh nghia ham delegate 
    public static CollectMedal collectMedalDelegate; //Khai bao ham delegate
    private int medals = 0;

    private void Start()
    {
        if (GameManager.HasInstance)
        {
            medals = GameManager.Instance.Medals;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Medal"))
        {
            if (AudioManager.HasInstance)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_COLLECT);
            }
            Destroy(collision.gameObject);
            medals++;
            GameManager.Instance.UpdateMedals(medals);
            collectMedalDelegate(medals); //Broadcast event
        }
    }
}
