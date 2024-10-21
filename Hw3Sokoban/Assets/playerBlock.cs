using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlock : MonoBehaviour
{
    GridManager manager;
    GridObject gridInfo;
    // Start is called before the first frame update
    void Start()
    {
        gridInfo = GetComponent<GridObject>();
        manager = FindObjectOfType<GridManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            manager.requestMove(gridInfo.gridPosition.x, gridInfo.gridPosition.y, 0, -1,gridInfo,1);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            manager.requestMove(gridInfo.gridPosition.x, gridInfo.gridPosition.y, 0, 1,gridInfo,1);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            manager.requestMove(gridInfo.gridPosition.x, gridInfo.gridPosition.y, 1, 0,gridInfo,1);
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            manager.requestMove(gridInfo.gridPosition.x, gridInfo.gridPosition.y, -1, 0,gridInfo,1);
        }
    }
}
