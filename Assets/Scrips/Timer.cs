using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timer = 0;
    public Text textoTimer;

    void Update()
    {
        timer = timer + 1;
        textoTimer.text = "" + timer.ToString();
        
    }
}
