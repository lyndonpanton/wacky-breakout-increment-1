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

    /// <summary>
    /// 
    /// </summary>
    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

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
                rb2d.position.x
                + horizontalInput
                * ConfigurationUtils.PaddleMoveUnitsPerSecond
                * Time.fixedDeltaTime
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

        if ((newXPosition - halfBC2DWidth) < ScreenUtils.ScreenLeft)
        {
            newXPosition = ScreenUtils.ScreenLeft + 10 * halfBC2DWidth;
        } else if ((newXPosition + halfBC2DWidth) >= ScreenUtils.ScreenRight)
        {
            newXPosition = ScreenUtils.ScreenRight - 10 * halfBC2DWidth;
        }

        return newXPosition;
    }
    
    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="collision">Collision information</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && isTopCollision(collision))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                collision.transform.position.x;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfBC2DWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = collision.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    bool isTopCollision(Collision2D collision)
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[2];
        collision.GetContacts(contactPoints);

        if (Vector2.Distance(contactPoints[0].point, contactPoints[1].point) < 0.05f)
        {
            return true;
        }

        return false;
    }
}
