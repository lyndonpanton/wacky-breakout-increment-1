using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls and interaction logic for the paddle game object
/// </summary>
public class Paddle : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float movement = 0;

        if (horizontalInput != 0)
        {
            movement = horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime;

            rb2d.MovePosition(
                new Vector2(
                    transform.position.x + movement,
                    transform.position.y
                )
            );
        }
    }
}
