using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
public class MazeGenerator : MonoBehaviour
{
    public Transform wallPref;
    Transform wallParents;
    public double wallChance = 0.5d;
    public int renderDistance = 4;
    public int xCurrent, yCurrent;
    void Start() => Generate();
    public void Generate()
    {
        transform.position = new Vector3(xCurrent, 0, yCurrent);
        if (wallParents)
        {
            Destroy(wallParents.gameObject);
        }
        wallParents = new GameObject("Walls").transform;
        wallParents.transform.position = new Vector3(-renderDistance, 0.5f, -renderDistance);
        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int y = -renderDistance; y <= renderDistance; y++)
            {
                if (Mathf.PerlinNoise((x + xCurrent) * 0.1f, (y + yCurrent) * 0.1f) > wallChance)
                {
                    Instantiate(wallPref, new Vector3(x, 0.5f, y) + transform.position, UnityEngine.Quaternion.identity, wallParents);
                }
            }
        }
    }
}