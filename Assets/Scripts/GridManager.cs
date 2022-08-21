using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public Grid grid;
    public Transform firstpos;
    public List<Grid> GridList = new List<Grid>();


    void Awake()
    {
        firstpos = grid.transform;
        GridMaker();
    }

    void GridMaker()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 7; j++)
            {
              Grid newGrid =  Instantiate(grid, new Vector2(firstpos.position.x + j * 0.65f, firstpos.position.y - i * 0.65f), Quaternion.identity);
                newGrid.transform.parent = this.gameObject.transform;
              GridList.Add(newGrid);
            }
        }
    }
}
