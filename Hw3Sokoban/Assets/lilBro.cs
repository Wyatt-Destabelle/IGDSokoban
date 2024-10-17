using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lilBro : MonoBehaviour
{
    public int type;
    GridObject g;
    // Start is called before the first frame update
    void Start()
    {
        g = GetComponent<GridObject>();
        FindObjectOfType<GridManager>().addBlock(g.gridPosition.x,g.gridPosition.y,g,type);
        Destroy(this);
    }

}
