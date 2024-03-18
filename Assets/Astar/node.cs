using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node
{
    public point position { get; private set; }
    
    public tiles_script tile { get; private set; }
    
    public node parent { get; private set; }
    
    public int G { get; private set; }
    public int H { get; private set; }
    public int F { get; private set; }
    
    public node(tiles_script ptr_tile)
    {
        this.tile = ptr_tile;
        this.position = ptr_tile.position;
    }

    public void calculate_values(node node_parent, point goal, int g_cost)
    {
        parent = node_parent;
        G = node_parent.G + g_cost;
        H = (Math.Abs(position.X - goal.X) + Math.Abs(goal.Y - position.Y)) * 10;
        F = G + H;
    }
}
