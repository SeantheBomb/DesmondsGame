using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyTeleport : MonoBehaviour, IDestination
{

    public float maxDistance = 6f;
    public float teleportRate = 2f;
    
    Rigidbody2D rb;

    Vector2 destination;

    bool hasDestination;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        StartCoroutine(LoopTeleport());
    }


    IEnumerator LoopTeleport()
    {
        while (enabled)
        {
            yield return new WaitUntil(() => hasDestination);
            Vector2 dir = destination - rb.position;
            Vector2 target = destination;
            if(dir.magnitude > maxDistance)
            {
                target = rb.position + (dir.normalized * maxDistance);
            }
            rb.MovePosition(target);
            ClearDestination();
            yield return new WaitForSeconds(teleportRate);
        }
    }


    public void ClearDestination()
    {
        hasDestination = false;
    }

    public bool IsDestinationReached()
    {
        return hasDestination == false;
    }

    public void SetDestination(Vector2 destination)
    {
        this.destination = destination;
        hasDestination = true;
    }
}
