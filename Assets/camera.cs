using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    private float camara_speed = 5.0f;
    
    private float max_x;
    private float max_y;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        analyse_input();   
    }

    private void analyse_input()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * camara_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * camara_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * camara_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * camara_speed * Time.deltaTime);
        }
        
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 0, max_x), Mathf.Clamp(transform.position.y, max_y, 0), transform.position.z);
    }

    public void set_limit(Vector3 last_tile)
    {
        Vector3 camera_position = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));
        
        max_x = last_tile.x - camera_position.x;
        max_y = last_tile.y - camera_position.y;
    }
}
