using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public (int t,GridObject obj)[,] gridMatrix;
    int maxX, maxY;
    // Start is called before the first frame update
    void Start()
    {
        GridMaker g = GetComponent<GridMaker>();
        maxX = (int)g.dimensions.x;
        maxY = (int)g.dimensions.y;
        if(gridMatrix == null)
            gridMake();
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
        bool move = false;
        if(targetTuple.t == 0)
        {
            move = true;
        }
        if(targetTuple.t == 2 || targetTuple.t == 3)
        {
            if(requestMove(x+moveX, y+moveY, moveX,moveY, targetTuple.r, targetTuple.t))
                move = true;
            else
                move = false;
        }
        if(targetTuple.t >= 4)
        {
            move = false;
        }
        if(move)
        {
            //grab behind


            gridMatrix[x+moveX,y+moveY] = (type,reqRef);
            reqRef.gridPosition =  new Vector2Int(x+moveX,y+moveY);
            gridMatrix[x,y] = (0,null);


            if(Mathf.Abs(moveX) == 1)
            {
                if(1 <= y-1)
                {
                    if(gridMatrix[x,y-1].t == 3)
                        requestMove(x, y-1, moveX,moveY, gridMatrix[x,y-1].obj,gridMatrix[x,y-1].t);
                }
                if(y+1 <= maxY)
                {
                    if(gridMatrix[x,y+1].t == 3)
                        requestMove(x, y+1, moveX,moveY, gridMatrix[x,y+1].obj,gridMatrix[x,y+1].t);
                }
            }
            else
            {
                if(1 <= x-1)
                {
                    if(gridMatrix[x-1,y].t == 3)
                        requestMove(x-1, y, moveX,moveY, gridMatrix[x-1,y].obj,gridMatrix[x-1,y].t);
                }
                if(x+1 <= maxX)
                {
                    if(gridMatrix[x+1,y].t == 3)
                        requestMove(x+1, y, moveX,moveY, gridMatrix[x+1,y].obj,gridMatrix[x+1,y].t);
                }
            }
            if(0 <= x-moveX && x-moveX <= maxX && 0 <= y-moveY && y-moveY <= maxY)
            {
                if(gridMatrix[x-moveX,y-moveY].t == 3 || gridMatrix[x-moveX,y-moveY].t == 4)
                    requestMove(x-moveX, y-moveY, moveX,moveY, gridMatrix[x-moveX,y-moveY].obj,gridMatrix[x-moveX,y-moveY].t);
            }

        }
        return move;
    }

    void gridMake()
    {
        GridMaker g = GetComponent<GridMaker>();
        gridMatrix = new (int,GridObject)[(int)g.dimensions.x +1,(int)g.dimensions.y + 1];
    }

    public void addBlock(int x, int y, GridObject reqRef, int type)
    {
        if(gridMatrix == null)
            gridMake();
        gridMatrix[x,y] = (type,reqRef);
    }
}
