

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
public GameObject objectToSpawn;

public float timeBetweenSpawn;
private float currentTimeBetweenSpawn;

private float leftWallPosX;
private float rightWallPosX;

//get position of front amd back wall object
float frontWallPosZ;
float backWallPosZ;

    // Start is called before the first frame update
    void Start()
    {
         leftWallPosX = GameObject.Find("LeftWall").transform.position.x;
         rightWallPosX = GameObject.Find("RightWall").transform.position.x;

        //get position of front amd back wall object
         frontWallPosZ = GameObject.Find("TopWall").transform.position.z;
         backWallPosZ = GameObject.Find("BottomWall").transform.position.z;
    }

    // Update is called once per frame
        void Update()
        {
            UpdateTimer();
        }

        void UpdateTimer()
        {
            if (GameManager.Instance.GameMode == GameManager.GameModeEnum.MostResources)
            {
                if (currentTimeBetweenSpawn > 0)
                {
                    currentTimeBetweenSpawn -= Time.deltaTime;
                }
                else
                {
                    SpawnObject();
                    currentTimeBetweenSpawn = timeBetweenSpawn;
                    //add some variation to the time between spawn
                    timeBetweenSpawn = timeBetweenSpawn * UnityEngine.Random.Range(0.9f, 1.1f);
                }
            }
        }

        void SpawnObject()
        {
            //spawn radomly in the area
            Vector3 spawnPosition = new Vector3(UnityEngine.Random.Range(leftWallPosX, rightWallPosX), 60, UnityEngine.Random.Range(frontWallPosZ, backWallPosZ));
                Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, transform);
        }
    }

