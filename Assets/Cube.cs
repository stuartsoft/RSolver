using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube : MonoBehaviour {

    public enum sides { FRONT=0, LEFT=1, BACK=2, RIGHT=3, TOP=4, BOTTOM=5 };
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

    public List<Color> getColors()
    {
        List<Color> tempColors = new List<Color>();
        for (int i = 0; i < panels.Count; i++)
        {
            Renderer rend = panels[i].GetComponent<Renderer>();
            tempColors.Add(rend.material.color);
        }
        return tempColors;
    }

    public void rotateY(int clockwise)
    {
        List<Color> oldColors = getColors();
        int iterations = 0;
        if (clockwise == 1)
            iterations = 1;
        else
            iterations = 3;

        for (int j = 0; j < iterations; j++){
            for (int i = 0; i < 4; i++){
                if (i == 3) setSideColor((sides)i, oldColors[0]);
                else setSideColor((sides)i, oldColors[i + 1]);
            }
        }
    }

    public void rotateX(int clockwise)
    {
        List<Color> oldColors = getColors();
        int iterations = 0;
        if (clockwise == 1)
            iterations = 1;
        else
            iterations = 3;

        for (int j = 0; j < iterations; j++)
        {
            setSideColor(sides.TOP, oldColors[(int)sides.FRONT]);
            setSideColor(sides.BACK, oldColors[(int)sides.TOP]);
            setSideColor(sides.BOTTOM, oldColors[(int)sides.BACK]);
            setSideColor(sides.FRONT, oldColors[(int)sides.BOTTOM]);
        }
    }

    public void rotateZ(int clockwise)
    {
        List<Color> oldColors = getColors();
        int iterations = 0;
        if (clockwise == 1)
            iterations = 1;
        else
            iterations = 3;

        for (int j = 0; j < iterations; j++)
        {
            setSideColor(sides.TOP, oldColors[(int)sides.LEFT]);
            setSideColor(sides.RIGHT, oldColors[(int)sides.TOP]);
            setSideColor(sides.BOTTOM, oldColors[(int)sides.RIGHT]);
            setSideColor(sides.LEFT, oldColors[(int)sides.BOTTOM]);
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
