using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public static class astar
{
    private static Dictionary<point, node> nodes;

    public static HashSet<node> open_list;
    public static HashSet<node> closed_list;
        
    private static void create_nodes()
    {
        nodes = new Dictionary<point, node>();
        foreach (tiles_script tile in level.Instance.tiles_map.Values)
        {
            nodes.Add(tile.position, new node(tile));
        }
    }

    public static void set_path(point start, point goal)
    {
        if (nodes == null)
        {
            create_nodes();
        }
        
        open_list = new HashSet<node>();
        closed_list = new HashSet<node>();
        Stack<node> path = new Stack<node>();
        node current = nodes[start];
        open_list.Add(current);

        while (open_list.Count > 0)
        {

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int g_cost = 0;
                    int h_cost = 0;
                    int f_cost = 0;

                    if (Math.Abs(x - y) == 1)
                    {
                        g_cost = 10;
                    }
                    else
                    {
                        g_cost = 14;
                    }

                    point neighbour = new point(current.position.X + x, current.position.Y + y);
                    if (nodes.ContainsKey(neighbour) && nodes[neighbour].tile.full == false)
                    {
                        node new_node = nodes[neighbour];

                        if (open_list.Contains(new_node))
                        {
                            if (current.G + g_cost < new_node.G)
                            {
                                new_node.calculate_values(current, goal, g_cost);
                            }
                        }
                        else if (!closed_list.Contains(new_node))
                        {
                            open_list.Add(new_node);
                            new_node.calculate_values(current, goal, g_cost);
                        }
                    }
                }
            }

            open_list.Remove(current);
            closed_list.Add(current);

            if (open_list.Count > 0)
            {
                current = open_list.OrderBy(n => n.F).First();
            }
            if (current == nodes[goal])//!= nodes[start])
            {
                while (current != nodes[start])
                {
                    path.Push(current);
                    current = current.parent;
                }
                break;
            }
        }
    }
}
