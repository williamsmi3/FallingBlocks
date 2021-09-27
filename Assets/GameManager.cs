using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float blockSpawnTime = 2f;
    public float startSpeed = 4f;
    float nextSpawnTime = 0;
    public GameObject Block;
    List<GameObject> blockList = new List<GameObject>();
    private GameObject cloneBlock;
    float screenHalfWidthInWorldUnits;
    float levelStartTime;
    public float levelDuration = 10f;
    public FallingBlocks blockClass;
    public PlayerMovement playerClass;
    public float spawnAngleMax;

    // Start is called before the first frame update
    void Start()
    {
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        levelStartTime = Time.time;
        blockClass.blockFallSpeed = startSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // if collosion -> restart/game over
        if (playerClass.hasCollided)
        {
            print("GAME OVER");
            blockClass.blockFallSpeed = startSpeed;
            levelStartTime = Time.time;
            print("LEVEL 1");
            DestroyAllBlocks();
            playerClass.hasCollided = false;
        }

        //If levelDuration has elapsed, start next Level
        if (Time.time > levelStartTime + levelDuration)
        {
            blockClass.blockFallSpeed++;
            print("LEVEL " + (blockClass.blockFallSpeed - startSpeed + 1));
            levelStartTime = Time.time;
        }

        if (Time.time > nextSpawnTime)
        {
            SpawnBlock();
        }
        CullBlocks();

        
        
    }

    // Spawn new block at top of gamespace at random X position
    void SpawnBlock()
    {
        //print("spawning block");
        float xPos = Random.Range(-screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits);
        float spawnAngle = Random.Range(-spawnAngleMax, spawnAngleMax);
        cloneBlock = Instantiate(Block, new Vector3(xPos, Camera.main.orthographicSize + transform.localScale.x, 0), Quaternion.Euler(Vector3.forward * spawnAngle));
        nextSpawnTime = Time.time + blockSpawnTime;
        blockList.Add(cloneBlock);
    }

    // For loop over blockList and Destroy blocks out of view
    void CullBlocks()
    {
        for (int i = 1; i <= blockList.Count; i++)
        {
            if (blockList[i - 1].transform.position.y < -Camera.main.orthographicSize - 2)
            {
                //print("destroying block");
                Destroy(blockList[i - 1]);
                blockList.RemoveAt(i - 1);
            }
        }
    }

    void DestroyAllBlocks()
    {
        for (int i = 1; i <= blockList.Count; i++)
        {
                //print("destroying block");
                Destroy(blockList[i - 1]);
        }
        blockList = new List<GameObject>();
    }

}
