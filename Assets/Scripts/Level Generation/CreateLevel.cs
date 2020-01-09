using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLevel : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 offset; // Distance between two tiles.
    
    [Header("Prefabs")]
    public GameObject wall;
    public GameObject innerWall;
    public GameObject destructableWall;
    public GameObject groundPlane;

    // Placeholders are containers, that contain similar objects.
    // They are used to keep hierarchy clean (Grouping)!
    [Header("PlaceHolders")]
    public Transform outerWallHolder;
    public Transform innerWallHolder;
    public Transform destructablesHolder;

    [Header("Set Grid Size >= (5, 0, 5)")]
    public Vector3 gridSize;

    [Header("LayerMasks")]
    public LayerMask layerMask;
}
