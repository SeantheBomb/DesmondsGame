using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IDestination
{

    public float stoppingDistance = 0.1f;
    public float moveSpeed = 3f;

    Vector2 destination;

    bool hasDestination;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasDestination == false)
            return;
        Vector2 dir = destination - rb.position;
        rb.MovePosition(rb.position + dir.normalized * moveSpeed * Time.deltaTime);
        if (IsDestinationReached())
            hasDestination = false;
    }

    public void SetDestination(Vector2 destination)
    {
        this.destination = destination;
        hasDestination = true;
    }

    public void ClearDestination()
    {
        hasDestination = false;
    }

    public bool IsDestinationReached()
    {
        if (hasDestination == false)
            return true;
        return Vector3.Distance(rb.position, destination) < stoppingDistance;
    }
}
