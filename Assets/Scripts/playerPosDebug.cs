using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosDebug : MonoBehaviour
{
    public MazeGenerator maze;
    public int a, b;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(a - maze.xCurrent, 0.5f, b - maze.yCurrent);
    }
}
