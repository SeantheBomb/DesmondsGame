using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;


    Rigidbody2D rb;

    Vector2 dir;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ClearDir());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            dir += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            dir += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            dir += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            dir += Vector2.right;

        dir = dir.normalized;

    }

    private IEnumerator ClearDir()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(0.1f);
            dir = Vector2.zero;
        }
    }


    public void MoveUp()
    {
        dir += Vector2.up;
    }

    public void MoveDown()
    {
        dir += Vector2.down;
    }

    public void MoveLeft()
    {
        dir += Vector2.left;
    }

    public void MoveRight()
    {
        dir += Vector2.right;
    }


    private void FixedUpdate()
    {
        if (dir.magnitude > 0)
        {
            rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
        }
    }
}
