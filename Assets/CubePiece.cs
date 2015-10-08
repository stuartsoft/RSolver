using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CubePiece : MonoBehaviour
{
    Cube cube = new Cube();
    public List<GameObject> panels = new List<GameObject>();

    void Start()
    {

    }

    public void setSideColors(List<Color> colors)
    {
        cube.setSideColors(colors);
        updatePanels();
    }

    public void setAllSideColors(Color c)
    {
        cube.setAllSideColors(c);
        updatePanels();

    }

    public List<Color> getColors()
    {
        return cube.getColors();
    }

    public void rotateY()
    {
        cube.rotateY();
        updatePanels();

    }

    public void rotateX()
    {
        cube.rotateX();
        updatePanels();

    }

    public void rotateZ()
    {
        cube.rotateZ();
        updatePanels();

    }

    public void setSideColor(Cube.sides side, Color c)
    {
        cube.setSideColor(side, c);
        updatePanels();
    }

    public Color getColor(Cube.sides side)
    {
        return cube.getColor(side);
    }
    
    public Cube getCubeData() { return cube; }

    void updatePanels()
    {
        for (int i= 0; i < 6; i++)
        {
            Renderer rend = panels[i].GetComponent<Renderer>();
            rend.enabled = true;
            rend.material.color = cube.colors[i];
        }
    }
}
