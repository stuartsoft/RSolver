using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube : MonoBehaviour {

    public enum sides { FRONT=0, RIGHT=1, BACK=2, LEFT=3, TOP=4, BOTTOM=5 };
    public List<GameObject> panels = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            //setColor((sides)i, Color.black);
        }
    }

    public void setColor(sides side, Color c) {
        Renderer rend = panels[(int)side].GetComponent<Renderer>();
        rend.enabled = true;
        rend.material.color = c;
    }

    public Color getColor(sides side)
    {
        Renderer rend = panels[(int)side].GetComponent<Renderer>();
        return rend.material.color;
    }
    
}
