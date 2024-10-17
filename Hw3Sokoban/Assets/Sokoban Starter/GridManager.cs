using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public (int,GridObject)[,] gridMatrix;
    int maxX, maxY;
    // Start is called before the first frame update
    void Start()
    {
        GridMaker g = GetComponent<GridMaker>();
        maxX = (int)g.dimensions.x;
        maxY = (int)g.dimensions.y;
        gridMatrix = new (int,GridObject)[(int)g.dimensions.x +1,(int)g.dimensions.y + 1];
    }
    /*
    0 <- empty
    1 <- player
    2 <- smooth
    3 <- sticky
    4 <- clingy
    5 <- wall
    */
    public bool requestMove(int x, int y, int moveX,int moveY, GridObject reqRef, int type)
    {
        if(x + moveX < 1 || x + moveX > maxX || y + moveY < 1 || y + moveY > maxY)
            return false;

        (int t,GridObject r) targetTuple = gridMatrix[x+moveX,y+moveY];
        if(targetTuple.t == 0)
        {
            gridMatrix[x+moveX,y+moveY] = (type,reqRef);
            reqRef.gridPosition =  new Vector2Int(x+moveX,y+moveY);
            gridMatrix[x,y] = (0,null);
            return true;
        }
        return false;
    }

    public void addBlock(int x, int y, GridObject reqRef, int type)
    {
        gridMatrix[x,y] = (type,reqRef);
    }
}
