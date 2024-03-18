using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
using SFB;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine;

public class level : singleton<level>
{
    private string[] folder;
    private int[][] map;
    
    [SerializeField]
    private GameObject[] tiles;
    
    [SerializeField]
    private camera camera;

    private point start_pos;
    private point end_pos;
    [SerializeField]
    private GameObject portal;

    public Dictionary<point, tiles_script> tiles_map { get; set; }

    [SerializeField] private Transform map_content;

    int[][] get_map_content(string folder)
    {
        string[] lines = File.ReadAllLines(folder + "/" + "ground.txt");
        int[][] result = new int[lines.Length][];
        for (int index_line = 0; index_line < lines.Length; index_line++)
        {
            string[] tiles_list = lines[index_line].Split(';');
            
            if (tiles_list.Length <= 35 || lines.Length <= 19)
            {
                //EditorUtility.DisplayDialog("Map files error : ground.txt", "The map must be at least 20x36 in size", "OK");
                print("Map files error : ground.txt");
                print("The map must be at least 20x36 in size");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                return null;
            }
            result[index_line] = new int[tiles_list.Length];
            for (int index_tile = 0; index_tile < tiles_list.Length; index_tile++)
            {
                try
                {
                    int nb = int.Parse(tiles_list[index_tile]);
                    if (nb < 0 || nb > tiles.Length - 1)
                    {
                        //EditorUtility.DisplayDialog("Map files error : ground.txt", "The files in the map folder are incorrect", "OK");
                        print("Map files error : ground.txt");
                        print("The files in the map folder are incorrect");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                        return null;
                    }

                    result[index_line][index_tile] = nb;
                } catch {
                    //EditorUtility.DisplayDialog("Map files error : ground.txt", "The files in the map folder are incorrect" + e, "OK");
                    print("Map files error : ground.txt");
                    print("The files in the map folder are incorrect");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                    return null;
                }
                if (result[index_line].Length != result[0].Length)
                {
                    //EditorUtility.DisplayDialog("Map files error : ground.txt", "The files in the map folder are incorrect", "OK");
                    print("Map files error : ground.txt");
                    print("The files in the map folder are incorrect");
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                    return null;
                }
            }
        }
        return result;
    }
    
    void display_map_content_on_logs()
    {
        print("Content of map :");

        for (int row = 0; row < map.Length; row++)
        {
            string line = "";
            
            for (int col = 0; col < map[row].Length; col++)
            {
                line += map[row][col] + " ";
            }
            Debug.Log("Line " + row + " : " + line);
        }
    }
    
    void spawn_portals(int x, int y)
    {
        try
        {
            string[] lines = File.ReadAllLines(folder[0] + "/" + "portals.txt");
            if (lines.Length != 2)
            {
                start_pos = new point(0, 0);
                end_pos = new point(x, y);
            }
            else
            {
                string[] start = lines[0].Split(';');
                string[] end = lines[1].Split(';');
                start_pos = new point(int.Parse(start[0]), int.Parse(start[1]));
                end_pos = new point(int.Parse(end[0]), int.Parse(end[1]));
            }
        }
        catch
        {
            start_pos = new point(0, 0);
            end_pos = new point(x, y);
        }
        Instantiate(portal, tiles_map[start_pos].transform.position, Quaternion.identity);
        Instantiate(portal, tiles_map[end_pos].transform.position, Quaternion.identity);
    }

    void display_tiles()
    {
        tiles_map = new Dictionary<point, tiles_script>();
        float tile_size = tiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 start_pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        Vector3 last_tile = Vector3.zero;
        int x = 0;
        int y = 0;
        
        for (int row = 0; row < map.Length; row++)
        {
            for (int col = 0; col < map[row].Length; col++)
            {
                tiles_script new_tile = Instantiate(tiles[map[row][col]]).GetComponent<tiles_script>();
                new_tile.setup(new point(col, row), new Vector3(tile_size / 2 + start_pos.x + col * tile_size, start_pos.y - (tile_size / 2) -row * tile_size, 0), map_content);
                last_tile = new Vector3(new_tile.transform.position.x + tile_size / 2, new_tile.transform.position.y - tile_size / 2, 0);
                //tiles_map.Add(new point(col, row), new_tile);
                x = col;
                y = row;
            }
        }
        
        camera.set_limit(last_tile);
        spawn_portals(x, y);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //FileStream fs = new FileStream("/dev/stdout", FileMode.OpenOrCreate, FileAccess.Write);
        //StreamWriter sw = new StreamWriter(fs);
        //Console.SetOut(sw);
        folder = StandaloneFileBrowser.OpenFolderPanel("Map folder", "", false);
        if (!File.Exists(folder[0] + "/" + "ground.txt"))
        {
            //EditorUtility.DisplayDialog("Map files error", "The files in the map folder are incorrect", "OK");
            print("Map files error");
            print("The files in the map folder are incorrect");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            return;
        }
        
        map = get_map_content(folder[0]);
        if (map == null)
            return;
        display_tiles();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
