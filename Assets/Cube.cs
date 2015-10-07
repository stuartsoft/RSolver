using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube : MonoBehaviour {

    public enum sides { FRONT=0, RIGHT=1, BACK=2, LEFT=3, TOP=4, BOTTOM=5 };
    public static Color REDCOLOR { get { return Color.red; } }
    public static Color BLUECOLOR { get { return Color.blue; } }
    public static Color GREENCOLOR { get { return Color.green; } }
    public static Color ORANGECOLOR { get { return new Color(1.0f, 0.25f, 0.0f); } }
    public static Color YELLOWCOLOR { get { return Color.yellow; } }
    public static Color WHITECOLOR { get { return Color.white; } }
    public static Color BLACKCOLOR { get { return Color.black; } }
    public List<GameObject> panels = new List<GameObject>();

    void Start()
    {
        
    }

    public void setAllSideColors(Color c)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            setSideColor((sides) i, c);
        }
    }

    public void setSideColor(sides side, Color c) {
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
