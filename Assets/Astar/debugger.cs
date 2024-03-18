using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugguer : MonoBehaviour
{
    [SerializeField]
    private tiles_script start;
    [SerializeField]
    private tiles_script goal;
    
    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                tiles_script tile = hit.collider.GetComponent<tiles_script>();
                if (tile != null)
                {
                    if (start == null)
                    {
                        start = tile;
                        start.Sprite.color = Color.green;
                    }
                    else if (goal == null)
                    {
                        goal = tile;
                        goal.Sprite.color = Color.red;
                    }
                }
            }
        }
    }

    public void debug_path(HashSet<node> open_list, HashSet<node> closed_list)
    {
        foreach (node node in open_list)
        {
            if (node.tile == start || node.tile == goal)
            {
                continue;
            }
            node.tile.Sprite.color = Color.cyan;
        }

        foreach (node node in closed_list)
        {
            if (node.tile == start || node.tile == goal)
            {
                continue;
            }
            node.tile.Sprite.color = Color.blue;
        }
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            astar.set_path(start.position, goal.position);
            debug_path(astar.open_list, astar.closed_list);
        }
    }
}
