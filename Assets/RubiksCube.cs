using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube
{
    public List<List<List<Cube>>> cubeMatrix;

    // Use this for initialization
    public RubiksCube()
    {
        cubeMatrix = new List<List<List<Cube>>>();

        for (int x = 0; x < 3; x++)
        {
            List<List<Cube>> CubeRow = new List<List<Cube>>();
            for (int y = 0; y < 3; y++)
            {
                List<Cube> CubeColumn = new List<Cube>();
                for (int z = 0; z < 3; z++)
                {
                    Cube tempcube = new Cube();
                    tempcube.setAllSideColors(Cube.BLACKCOLOR);
                    CubeColumn.Add(tempcube);
                }
                CubeRow.Add(CubeColumn);
            }
            cubeMatrix.Add(CubeRow);
        }
        //transform.rotation = Quaternion.Euler(new Vector3(45,0,45));
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cubeMatrix[i][j][0].setSideColor(Cube.sides.FRONT, Cube.REDCOLOR);
                cubeMatrix[2][i][j].setSideColor(Cube.sides.RIGHT, Cube.BLUECOLOR);
                cubeMatrix[0][i][j].setSideColor(Cube.sides.LEFT, Cube.GREENCOLOR);
                cubeMatrix[i][j][2].setSideColor(Cube.sides.BACK, Cube.ORANGECOLOR);
                cubeMatrix[i][2][j].setSideColor(Cube.sides.TOP, Cube.WHITECOLOR);
                cubeMatrix[i][0][j].setSideColor(Cube.sides.BOTTOM, Cube.YELLOWCOLOR);
            }
        }
        cubeMatrix[1][1][1].setAllSideColors(Cube.BLACKCOLOR);

        //rotateBackFace(true);
        //rotateFrontFace(true);
    }

    List<List<Cube>> getCubeXFace(int r, bool reference)
    {
        List<List<Cube>> face = new List<List<Cube>>();
        for (int i = 0; i < 3; i++)
        {
            List<Cube> row = new List<Cube>();
            for (int j = 0; j < 3; j++)
            {
                if (reference)
                    row.Add(cubeMatrix[i][j][r]);
                else
                {
                    Cube tempcube = new Cube(cubeMatrix[i][j][r].getColors());
                    row.Add(tempcube);
                }
            }
            face.Add(row);
        }

        return face;
    }

    List<List<Cube>> getCubeYFace(int r, bool reference)
    {
        List<List<Cube>> face = new List<List<Cube>>();
        for (int i = 0; i < 3; i++)
        {
            List<Cube> row = new List<Cube>();
            for (int j = 0; j < 3; j++)
            {
                if (reference)
                    row.Add(cubeMatrix[r][i][j]);
                else
                {
                    Cube tempcube = new Cube(cubeMatrix[r][i][j].getColors());
                    row.Add(tempcube);
                }
            }
            face.Add(row);
        }

        return face;
    }

    List<List<Cube>> getCubeZFace(int r, bool reference)
    {
        List<List<Cube>> face = new List<List<Cube>>();
        for (int i = 0; i < 3; i++)
        {
            List<Cube> row = new List<Cube>();
            for (int j = 0; j < 3; j++)
            {
                if (reference)
                    row.Add(cubeMatrix[i][r][j]);
                else
                {
                    Cube tempcube = new Cube(cubeMatrix[i][r][j].getColors());
                    row.Add(tempcube);
                }
            }
            face.Add(row);
        }

        return face;
    }

    List<Cube> getOutline(List<List<Cube>> face)
    {
        //converts a 2d matrix of cubes to a linear list of the outline cubes of the provided face
        //the list is ordered in a clockwise direction, starting with the lower left cube [0][0]
        //getOutline returns a list of references to the origional matrix
        List<Cube> outline = new List<Cube>();
        for (int i = 0; i < 3; i++)
        {
            outline.Add(face[0][i]);
        }
        outline.Add(face[1][2]);
        for (int i = 2; i >= 0; i--)
        {
            outline.Add(face[2][i]);
        }
        outline.Add(face[1][0]);

        return outline;
    }

    public void rotateFrontFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateZ();
            }
        }
    }

    public void rotateBackFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
            }
        }
    }

    public void rotateRightFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeYFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeYFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateX();
            }
        }
    }

    public void rotateLeftFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeYFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeYFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();

            }
        }
    }

    public void rotateTopFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeZFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeZFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i+2)%8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateY();
                currentFrontOutline[(i + 2) % 8].rotateY();
                currentFrontOutline[(i + 2) % 8].rotateY();
            }
        }
    }

    public void rotateBottomFace(bool clockwise)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeZFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeZFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i+2)%8].getColors());
                currentFrontOutline[i].rotateY();
            }
        }
    }

    public Vector3 cornerPieceWithColors(Color a, Color b, Color c)
    {
        if (a == b || b == c || c == a)
            throw new System.ArgumentException("No two colors in query can be the same");

        for (int i= 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (cubeMatrix[i][j][k].containsColors(a, b, c))
                    {
                        return new Vector3(i, j, k);
                    }
                }
            }
        }

        //cube was not found. Something is wrong if you're looking for a cube that doesn't exist
        throw new System.ArgumentException("No such cube exists with colors provided");

    }
}
