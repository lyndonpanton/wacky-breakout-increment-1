using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// Controls and interaction logic for the paddle game object
/// </summary>
public class Paddle : MonoBehaviour
{
    /// <summary>
    /// The rigidybody2d componenet of the game object
    /// </summary>
    Rigidbody2D rb2d;

    /// <summary>
    /// Half the width of the game object's box collider 2d
    /// </summary>
    float halfBC2DWidth;
    

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        halfBC2DWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0)
        {
            float newXPosition = CalculateClampedX(
                horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime + transform.position.x
            );

            rb2d.MovePosition(
                new Vector2(
                    newXPosition,
                    transform.position.y
                )
            );
        }
    }

    // CalculateClampedX ensures the game object is kept within the screen's bounds
    float CalculateClampedX(float newXPosition)
    {
        //if ((newXPosition - halfBC2DWidth * 2) <= ScreenUtils.ScreenLeft || (newXPosition + halfBC2DWidth * 2) >= ScreenUtils.ScreenRight)
        //{
        //    return transform.position.x;
        //}

        //return newXPosition;

        if ((newXPosition - halfBC2DWidth) <= ScreenUtils.ScreenLeft)
        {
            newXPosition = ScreenUtils.ScreenLeft + 10 * halfBC2DWidth;
        } else if ((newXPosition + halfBC2DWidth) >= ScreenUtils.ScreenRight)
        {
            newXPosition = ScreenUtils.ScreenRight - 10 * halfBC2DWidth;
        }

        return newXPosition;
    }
}
