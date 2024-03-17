using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiles_script : MonoBehaviour
{
    public point position { get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void setup(point start_position, Vector3 world_position)
    {
        position = start_position;
        transform.position = world_position;
        level.Instance.tiles_map.Add(start_position, this);
    }
}
