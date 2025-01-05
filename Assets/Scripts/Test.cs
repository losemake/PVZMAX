using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    public Rigidbody2D rb;
    private float horizontalInput;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        horizontalInput = Input.GetAxis("HorizontalPlayer1");
        Vector2 movement = new Vector2(moveSpeed * horizontalInput, 0.0f);
        rb.AddForce(movement, ForceMode2D.Force);
    }
    /*void Update()
    {
        float Date = Input.GetAxis("Horizontal");
        Debug.Log(Date);
    }*/
}
