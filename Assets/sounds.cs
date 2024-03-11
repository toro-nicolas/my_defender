using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    public static sounds sound;

    void Awake()
    {
        if (sound != null) {
            Destroy(gameObject);
        } else {
            sound = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
