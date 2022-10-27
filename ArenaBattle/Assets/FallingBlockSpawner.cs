using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlockSpawner : MonoBehaviour
{
public GameObject fallingBlockToSpawn;

public float timeBetweenSpawn;
private float currentTimeBetweenSpawn;

private float positionOffsetX;
private float positionOffsetZ;

private int numberOfBlocksThatFitInArenaWidth;
private int numberOfBlocksThatFitInArenaHeight;

public bool shrinkingOn = false;

    // Start is called before the first frame update
    void Start()
    {
        float leftWallPosX = GameObject.Find("LeftWall").transform.position.x;
        float rightWallPosX = GameObject.Find("RightWall").transform.position.x;
        
        //get position of front amd back wall object
        float frontWallPosZ = GameObject.Find("TopWall").transform.position.z;
        float backWallPosZ = GameObject.Find("BottomWall").transform.position.z;
        
        float distanceBetweenLeftAndRightWalls = rightWallPosX - leftWallPosX;
        float distanceBetweenFrontAndBackWalls = backWallPosZ - frontWallPosZ;
        
        //how many blocks can fit in the arena width
        numberOfBlocksThatFitInArenaWidth = (int)(distanceBetweenLeftAndRightWalls / fallingBlockToSpawn.transform.localScale.x);
        
        //how many blocks can fit in the arena height
        numberOfBlocksThatFitInArenaHeight = (int)(distanceBetweenFrontAndBackWalls / fallingBlockToSpawn.transform.localScale.z);
        numberOfBlocksThatFitInArenaHeight = Mathf.Abs(numberOfBlocksThatFitInArenaHeight);
    }

    // Update is called once per frame
    void Update()
    {
        if (shrinkingOn)
        {
            UpdateTimer();
        }
    }

    void UpdateTimer()
    {
        if (currentTimeBetweenSpawn > 0)
        {
            currentTimeBetweenSpawn -= Time.deltaTime;
        }
        else
        {
            SpawnObject();
            currentTimeBetweenSpawn = timeBetweenSpawn;
        }
    }

    public void SpawnObject()
    {
        var localScale = fallingBlockToSpawn.transform.localScale;
        
        //if the position to spawn is beyond the width move down a row
        if(positionOffsetX >= (numberOfBlocksThatFitInArenaWidth - 1) * localScale.x)
        {
            positionOffsetX = 0;
            positionOffsetZ -= localScale.z;
        }
        
        //make sure the position is within the height of the arena
        float currentOffsetZ = -(numberOfBlocksThatFitInArenaHeight - 1) * localScale.z;
        if (positionOffsetZ < currentOffsetZ)
        {
            return;
        }
        
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += positionOffsetX;
        spawnPosition.z += positionOffsetZ;
        
        string nameOfObject = "FallingBlock: " + positionOffsetX / localScale.x + ", " + positionOffsetZ / localScale.z;
        fallingBlockToSpawn.name = nameOfObject;
        Instantiate(fallingBlockToSpawn, spawnPosition, Quaternion.identity, transform);
        
        positionOffsetX += localScale.x;
    }
}
