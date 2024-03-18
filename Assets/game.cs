using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class game : singleton<game>
{
    [SerializeField]
    private GameObject[] towers;
    public GameObject[] Towers
    {
        get
        {
            if (towers == null)
            {
                towers = Resources.LoadAll<GameObject>("Towers");
            }
            return towers;
        }
    }
    
    [SerializeField]
    private Text currency_text;
    
    private int currency;
    
    public int Currency
    {
        get
        {
            return currency;
        }
        set
        {
            currency = value;
            currency_text.text = value.ToString() + "$";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currency = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
