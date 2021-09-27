using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerPosition;
    public float speed = 7f;
    float screenHalfWidthInWorldUnits;
    public bool hasCollided = false;

    // Start is called before the first frame update
    void Start()
    {
        float halfPlayerWidth = transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float velocity = inputX * speed;
        transform.Translate(Time.deltaTime * velocity * Vector2.right);

        if(transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(-screenHalfWidthInWorldUnits, transform.position.y);
        }

        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector2(screenHalfWidthInWorldUnits, transform.position.y);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        print("COLLISION");
        hasCollided = true;
    }
}
