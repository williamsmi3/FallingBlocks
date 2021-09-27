using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlocks : MonoBehaviour
{
    public float blockFallSpeed;
    private float blockScale;
    

    // Start is called before the first frame update
    void Start()
    {
        blockScale = Random.Range(-0.5f, 2f);
        transform.localScale += new Vector3(blockScale, blockScale, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.down * Time.deltaTime * blockFallSpeed);
    }
}
