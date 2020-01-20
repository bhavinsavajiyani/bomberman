using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(CreateLevel))]
[CanEditMultipleObjects]

public class LevelEditor : Editor
{
    private GameObject _wallPrefab;
    private GameObject _innerWallPrefab;
    private CreateLevel _createLevel;
    private bool _isScriptActive;

    new public void OnInspectorGUI()
    {
        DrawDefaultInspector();

        _createLevel = (CreateLevel)target;
        _wallPrefab = _createLevel.wall;
        _innerWallPrefab = _createLevel.innerWall;

        // Creates two buttons in the editor, which are placed side by side,
        // and triggers the provided functions, based on the conditions specified.
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Create Border"))
        {
            if(!_isScriptActive)
            {
                BuildBorder();
            }
        }

        if(GUILayout.Button("Delete Border"))
        {
            if(!_isScriptActive)
            {
                DeleteBorder();
            }
        }
        EditorGUILayout.EndHorizontal();

        // Same as creating outer walls, except this time, we generate inner walls!
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Create Inner Walls"))
        {
            if(!_isScriptActive)
            {
                BuildInnerWalls();
            }
        }

        if(GUILayout.Button("Delete Inner Walls"))
        {
            if(!_isScriptActive)
            {
                DeleteInnerWalls();
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void BuildBorder()
    {
        // Check if grid size is of appropriate Value.
        // 5 x 5 is the minimal grid size, which is used for convenience!
        if(_createLevel.gridSize.x < 5 || _createLevel.gridSize.z < 5)
        {
            Debug.LogWarning("Grid size should be greater than or equal to 5!");
            return;
        }

        // If grid size is even, then it would leave no space for extra tiles in center,
        // and therefore, it is advisable to create grid, of "Odd" size.
        if(_createLevel.gridSize.x % 2 == 0 || _createLevel.gridSize.z % 2 == 0)
        {
            Debug.LogWarning("Grid size must be odd numbers!");
            return;
        }

        // Call DeleteBorder Function, to get empty line.
        DeleteBorder();
        _isScriptActive = true;

        // Loop for generating grid
        for(int i = 0; i < _createLevel.gridSize.x; i++)
        {
            for(int j = 0; j < _createLevel.gridSize.z; j++)
            {
                // Check if grid is on left side, or on the outer side (Max amount), only then create.
                if(i == 0 || i == _createLevel.gridSize.x - 1)
                {
                    // Instantiate wall prefab, while maintaining connection with original prefab!
                    GameObject wall = PrefabUtility.InstantiatePrefab(_wallPrefab) as GameObject;
                    // Set position of wall. 
                    wall.transform.position = new Vector3(_createLevel.startPosition.x + i + _createLevel.offset.x, 
                        _createLevel.startPosition.y + _createLevel.offset.y,
                            _createLevel.startPosition.z + j + _createLevel.offset.z);

                    wall.transform.parent = _createLevel.outerWallHolder;
                }

                if(j == 0 || j == _createLevel.gridSize.z - 1)
                {
                    // Instantiate wall prefab, while maintaining connection with original prefab!
                    GameObject wall = PrefabUtility.InstantiatePrefab(_wallPrefab) as GameObject;
                    // Set position of wall. 
                    wall.transform.position = new Vector3(_createLevel.startPosition.x + i + _createLevel.offset.x, 
                        _createLevel.startPosition.y + _createLevel.offset.y,
                            _createLevel.startPosition.z + j + _createLevel.offset.z);

                    wall.transform.parent = _createLevel.outerWallHolder;
                }
            }
        }

        ResizePlane();
        _isScriptActive = false;
    }

    void DeleteBorder()
    {
        int childCount = _createLevel.outerWallHolder.transform.childCount;

        for(int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_createLevel.outerWallHolder.transform.GetChild(i).gameObject);
        }
    }

    void ResizePlane()
    {
        // RESIZE
        Vector3 scaler = new Vector3((float)_createLevel.gridSize.x / 10, 1, (float)_createLevel.gridSize.z / 10);
        _createLevel.groundPlane.transform.localScale = scaler;

        // POSITION
        _createLevel.groundPlane.transform.position = new Vector3(_createLevel.gridSize.x / 2,
            0, _createLevel.gridSize.z / 2);

        // Add material to expanded portion of plane.
        _createLevel.groundPlane.GetComponent<MeshRenderer>().sharedMaterial.
            mainTextureScale = new Vector2(_createLevel.gridSize.x, _createLevel.gridSize.z);
    }

    void BuildInnerWalls()
    {
        // Check if grid size is of appropriate Value.
        // 5 x 5 is the minimal grid size, which is used for convenience!
        if(_createLevel.gridSize.x < 5 || _createLevel.gridSize.z < 5)
        {
            Debug.LogWarning("Grid size should be greater than or equal to 5!");
            return;
        }

        // If grid size is even, then it would leave no space for extra tiles in center,
        // and therefore, it is advisable to create grid, of "Odd" size.
        if(_createLevel.gridSize.x % 2 == 0 || _createLevel.gridSize.z % 2 == 0)
        {
            Debug.LogWarning("Grid size must be odd numbers!");
            return;
        }
        
        DeleteInnerWalls();
        _isScriptActive = true;
        int distance = 2;
        for(int i = distance; i <= _createLevel.gridSize.x - distance; i++)
        {
            for (int j = distance; j <= _createLevel.gridSize.z - distance; j++)
            {
                // Place even number of inner walls only
                if((i % distance == 0) && (j % distance == 0))
                {
                    GameObject inner_wall = PrefabUtility.InstantiatePrefab(_innerWallPrefab) as GameObject;
                    inner_wall.transform.position = new Vector3(_createLevel.startPosition.x + i + _createLevel.offset.x,
                        _createLevel.startPosition.y + _createLevel.offset.y, 
                            _createLevel.startPosition.z + j + _createLevel.offset.z);
                    
                    inner_wall.transform.parent = _createLevel.innerWallHolder;
                }
            }
        }
        _isScriptActive = false;
    }

    void DeleteInnerWalls()
    {
        int childCount = _createLevel.innerWallHolder.transform.childCount;

        for(int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(_createLevel.innerWallHolder.transform.GetChild(i).gameObject);
        }
    }
}
