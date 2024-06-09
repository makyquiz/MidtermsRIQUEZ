using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowSpawn : MonoBehaviour
{
    public Transform[] spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = spawnPosition[0].position;
        transform.position = spawnPosition[1].position;
        transform.position = spawnPosition[2].position;
        transform.position = spawnPosition[3].position;
    }
}
