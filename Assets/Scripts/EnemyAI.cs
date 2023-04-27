using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float nextWPDistance;
    [SerializeField]
    private SpriteRenderer characterSR;
    public bool roaming = true;

    public Seeker seeker;
    public bool updateContinuesPath;
    bool reachDestination = false;
    Transform target;
    Path path;
    Coroutine moveCoroutine;
    private void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;
    }
    void CalculatePath()
    {
        Vector2 target = FindTarget();
        if (seeker.IsDone() && (reachDestination || updateContinuesPath))
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (p.error) return;
        path = p;
        //Move to target
        MoveToTarget();
    }
    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        {
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
        }
    }
    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;
        while (currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * playerSpeed * Time.deltaTime;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if (distance < nextWPDistance)
            {
                currentWP++;
            }
            yield return null;
            //if (force.x != 0)
            //{
            //    if (force.x < 0)
            //    {
            //        characterSR.transform.localScale = new Vecter3(-1, 1, 0);
            //    }
            //    else
            //    {
            //        characterSR.transform.localScale = new Vecter3(-1, 1, 0);
            //    }
            //}
        }
    }
    Vector2 FindTarget()
    {
        Vector3 playerPos = FindObjectOfType<PlayerMovement1>().transform.position;
        if (roaming == true)
        {
            return (Vector2)playerPos + (Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return playerPos;
        }
    }
}
