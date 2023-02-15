using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines interaction logic for the paddle game object
/// </summary>
public class Ball : MonoBehaviour
{
    /// <summary>
    /// The rigidybody2d componenet of the game object
    /// </summary>
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        float startingAngle = 20 * Mathf.Deg2Rad;

        float x = Mathf.Cos(startingAngle) * ConfigurationUtils.BallImpulseForce;
        float y = Mathf.Sin(startingAngle) * ConfigurationUtils.BallImpulseForce;

        Vector2 force = new Vector2(x, y);

        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddForce(
            force,
            ForceMode2D.Force
        );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
