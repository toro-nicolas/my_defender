using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class tiles_script : MonoBehaviour
{
    public point position { get; private set; }
    
    public bool full { get; set; }
    
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite
    {
        get
        {
            if (sprite == null)
            {
                sprite = GetComponent<SpriteRenderer>();
            }
            return sprite;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setup(point start_position, Vector3 world_position, Transform parent)
    {
        position = start_position;
        transform.position = world_position;
        transform.SetParent(parent);
        level.Instance.tiles_map.Add(start_position, this);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            place_tower();
        }
    }

    public void place_tower()
    {
        if (game.Instance.Currency < 50 || full)
            return;
        GameObject tower = Instantiate(game.Instance.Towers[0], transform.position, quaternion.identity); //GetComponent<tower_script>().create(position);
        tower.GetComponent<SpriteRenderer>().sortingOrder = position.Y;
        tower.transform.SetParent(transform);
        game.Instance.Currency -= 50;
        full = true;
    }
}
