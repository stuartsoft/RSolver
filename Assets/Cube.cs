using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cube
{

    public enum sides { FRONT = 0, LEFT = 1, BACK = 2, RIGHT = 3, TOP = 4, BOTTOM = 5 };
    public static Color REDCOLOR { get { return Color.red; } }
    public static Color BLUECOLOR { get { return Color.blue; } }
    public static Color GREENCOLOR { get { return Color.green; } }
    public static Color ORANGECOLOR { get { return new Color(1.0f, 0.25f, 0.0f); } }
    public static Color YELLOWCOLOR { get { return Color.yellow; } }
    public static Color WHITECOLOR { get { return Color.white; } }
    public static Color BLACKCOLOR { get { return Color.black; } }
    public List<Color> colors = new List<Color>();

    public Cube()
    {
        for (int i = 0; i < 6; i++) { colors.Add(BLACKCOLOR); }
        setAllSideColors(BLACKCOLOR);
    }

    public Cube(List<Color> c)
    {
        for (int i = 0; i < 6; i++) { colors.Add(BLACKCOLOR); }
        setSideColors(c);
    }

    public void setSideColors(List<Color> colors)
    {
        for (int i = 0; i < 6; i++)
        {
            setSideColor((sides)i, colors[i]);
        }
    }

    public void setAllSideColors(Color c)
    {
        for (int i = 0; i < colors.Count; i++)
        {
            setSideColor((sides)i, c);
        }
    }

    public List<Color> getColors()
    {
        List<Color> tempcolors = new List<Color>();
        for (int i = 0; i < colors.Count; i++)
        {
            tempcolors.Add(colors[i]); 
        }
        return tempcolors;
    }

    public void rotateY(int clockwise)
    {
        List<Color> oldColors = getColors();
        int iterations = 0;
        if (clockwise == 1)
            iterations = 1;
        else
            iterations = 3;

        for (int j = 0; j < iterations; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                setSideColor((sides)i, oldColors[(i + 1) % 4]);
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

    public void setSideColor(sides side, Color c)
    {
        colors[(int)side] = c;
    }

    public Color getColor(sides side)
    {
        return colors[(int)side];
    }
}
