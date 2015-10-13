using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube
{
    public List<List<List<Cube>>> cubeMatrix;

    public List<string> sequences;
    public string turnRecord;

    // Use this for initialization
    public RubiksCube()
    {
        sequences = new List<string>();
        sequences.Add("RiUFiUi");//fix middle in stage 2
        sequences.Add("RiDiRD");//fix corner in stage 3
        sequences.Add("URUiRiUiFiUF");
        sequences.Add("UiLiULUFUiFi");
        sequences.Add("FURUiRiFi");
        sequences.Add("FRURiUiFi");
        sequences.Add("RURiURUURi");
        sequences.Add("RiFRiBBRFiRiBBRRUi");
        sequences.Add("FFULRiFFLiRUFF");
        sequences.Add("FFUiLRiFFLiRUiFF");

        sequences.Add("LLRRFFBBUUDD");//checkerboard
        sequences.Add("UDiRLiFBiUDi");//six spots

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
    }

    public List<List<Cube>> getCubeXYFace(int r, bool reference)
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

    public List<List<Cube>> getCubeYZFace(int r, bool reference)
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

    public List<List<Cube>> getCubeXZFace(int r, bool reference)
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

    public void rotateFrontFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXYFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXYFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateZ();
            }
        }

        if (record)
        {
            turnRecord += "F";
            if (!clockwise)
                turnRecord += "i";
        }

    }

    void rotateMiddleXYFace(bool clockwise)//clockwise is relative to front face in this case
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXYFace(1, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXYFace(1, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateZ();

            }
        }
    }

    public void rotateBackFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXYFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXYFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
            }
        }

        if (record)
        {
            turnRecord += "B";
            if (!clockwise)
                turnRecord += "i";
        }
    }

    public void rotateRightFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeYZFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeYZFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateX();
            }
        }

        if (record)
        {
            turnRecord += "R";
            if (!clockwise)
                turnRecord += "i";
        }
    }

    void rotateMiddleYZFace(bool clockwise)//clockwise is relative to left face
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeYZFace(1, false));
            List<Cube> currentFrontOutline = getOutline(getCubeYZFace(1, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();

            }
        }
    }

    public void rotateLeftFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeYZFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeYZFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i + 2) % 8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();
                currentFrontOutline[(i + 2) % 8].rotateX();

            }
        }

        if (record)
        {
            turnRecord += "L";
            if (!clockwise)
                turnRecord += "i";
        }
    }

    public void rotateTopFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXZFace(2, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXZFace(2, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[(i+2)%8].setSideColors(oldFrontOutline[i].getColors());
                currentFrontOutline[(i + 2) % 8].rotateY();
                currentFrontOutline[(i + 2) % 8].rotateY();
                currentFrontOutline[(i + 2) % 8].rotateY();
            }
        }

        if (record)
        {
            turnRecord += "U";
            if (!clockwise)
                turnRecord += "i";
        }
    }

    void rotateMiddleXZFace(bool clockwise)//clockwise is relative to bottom face
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXZFace(1, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXZFace(1, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateY();
            }
        }
    }

    public void rotateBottomFace(bool clockwise, bool record = true)
    {
        int iterations = 1;
        if (!clockwise) iterations = 3;
        for (int j = 0; j < iterations; j++)
        {
            List<Cube> oldFrontOutline = getOutline(getCubeXZFace(0, false));
            List<Cube> currentFrontOutline = getOutline(getCubeXZFace(0, true));

            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i+2)%8].getColors());
                currentFrontOutline[i].rotateY();
            }
        }

        if (record)
        {
            turnRecord += "D";
            if (!clockwise)
                turnRecord += "i";
        }
    }

    public void turnCubeZ(bool clockwise)
    {
        rotateFrontFace(clockwise, false);
        rotateMiddleXYFace(clockwise);
        rotateBackFace(!clockwise, false);

        turnRecord += "Z";
        if (!clockwise)
            turnRecord += "i";
    }

    public void turnCubeX(bool clockwise)
    {
        rotateLeftFace(clockwise, false);
        rotateMiddleYZFace(clockwise);
        rotateRightFace(!clockwise, false);

        turnRecord += "X";
        if (!clockwise)
            turnRecord += "i";
    }

    public void turnCubeY(bool clockwise)
    {
        rotateBottomFace(clockwise, false);
        rotateMiddleXZFace(clockwise);
        rotateTopFace(!clockwise, false);

        turnRecord += "Y";
        if (!clockwise)
            turnRecord += "i";
    }

    public Vector3 cornerCubeWithColors(Color a, Color b, Color c)
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

    public Vector3 sideCubeWithColors(Color a, Color b)
    {
        if (a == b)
            throw new System.ArgumentException("No two colors in query can be the same");

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    if (cubeMatrix[i][j][k].numColors()==2 && cubeMatrix[i][j][k].containsColors(a, b))
                    {
                        return new Vector3(i, j, k);
                    }
                }
            }
        }

        //cube was not found. Something is wrong if you're looking for a cube that doesn't exist
        throw new System.ArgumentException("No such cube exists with colors provided");
    }
   
    public bool isYellowCrossOnTop()
    {
        bool valid = true;
        List<List<Cube>> plane =  getCubeXZFace(2, false);

        if (plane[0][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;
        if (plane[1][0].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;
        if (plane[2][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;
        if (plane[1][2].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        //check center
        if (plane[1][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        return valid;
    }

    public bool isYellowHorizontalLineOnTop()
    {
        bool valid = true;
        List<List<Cube>> plane = getCubeXZFace(2, false);

        if (plane[0][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;
        if (plane[2][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        //check center
        if (plane[1][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        return valid;
    }

    public bool isYellowBackwardsLOnTop()
    {
        bool valid = true;
        List<List<Cube>> plane = getCubeXZFace(2, false);

        if (plane[0][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;
        if (plane[1][2].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        //check center
        if (plane[1][1].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
            valid = false;

        return valid;
    }

    public bool isTopAllYellow()
    {
        bool valid = true;
        List<List<Cube>> plane = getCubeXZFace(2, false);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (plane[i][j].getColor(Cube.sides.TOP) != Cube.YELLOWCOLOR)
                    valid = false;
            }
        }

        return valid;
    }

    public bool allTopCornersSolved()
    {
        bool iscorrect = true;
        for (int i = 0; i < 4; i++)
        {
            Cube TopLeftFrontCube = cubeMatrix[0][2][0];
            Cube MiddleLeftFrontCube = cubeMatrix[0][1][0];

            if (TopLeftFrontCube.getColor(Cube.sides.FRONT) != MiddleLeftFrontCube.getColor(Cube.sides.FRONT) || TopLeftFrontCube.getColor(Cube.sides.LEFT) != MiddleLeftFrontCube.getColor(Cube.sides.LEFT))
                iscorrect = false;
            turnCubeY(true);
        }

        return iscorrect;
    }

    public bool isSolved()
    {
        List<List<Cube>> Side;
        Color c;
        bool valid = true;

        for (int z = 0; z < 4; z++)//check perimeter
        {
            Side = getCubeXYFace(0, true);
            c = Side[1][1].getColor(Cube.sides.FRONT);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Side[i][j].getColor(Cube.sides.FRONT) != c)
                        valid = false;
                }
            }

            turnCubeY(true);
        }

        //check top side
        Side = getCubeXZFace(2, true);
        c = Side[1][1].getColor(Cube.sides.TOP);
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Side[i][j].getColor(Cube.sides.TOP) != c)
                    valid = false;
            }
        }

        //if 5 of the 6 sides are correct, the last side must be correct

        return valid;
    }

    public int RunSequence(int s)
    {
        //Debug.Log("Running sequence: " + s);
        string seq = sequences[s];
        int cost = RunCustomSequence(seq);
        return cost;
    }

    public int RunCustomSequence(string seq)
    {
        int cost = 0;
        int step = 0;
        while (step < seq.Length)
        {
            char c = seq[step];
            bool clockwise = true;
            if (step + 1 < seq.Length)
            {
                if (seq[step + 1] == 'i')
                {
                    clockwise = false;
                    step++;//increment past inverse character
                }
            }

            if (c == 'R')
                rotateRightFace(clockwise);
            else if (c == 'L')
                rotateLeftFace(clockwise);
            else if (c == 'U')
                rotateTopFace(clockwise);
            else if (c == 'D')
                rotateBottomFace(clockwise);
            else if (c == 'F')
                rotateFrontFace(clockwise);
            else if (c == 'B')
                rotateBackFace(clockwise);
            else if (c == 'X')
                turnCubeX(clockwise);
            else if (c == 'Y')
                turnCubeY(clockwise);
            else if (c == 'Z')
                turnCubeZ(clockwise);

            step++;
            cost++;
        }

        return cost;
    }

    public void Scramble(int turns)
    {
        string seq = "";
        List<string> moves = new List<string>();
        moves.Add("R");
        moves.Add("L");
        moves.Add("U");
        moves.Add("D");
        moves.Add("F");
        moves.Add("B");
        
        for (int i = 0; i < turns; i++)
        {
            int j = ((int)(Random.value * 32676)) % 6;//select move
            int k = ((int)(Random.value * 32676)) % 2;//select clockwise

            seq += moves[j];
            if (k == 0)
                seq += "i";
        }

        RunCustomSequence(seq);

        clearTurnRecord();
    }

    public void clearTurnRecord()
    {
        turnRecord = "";
    }

    public RubiksCube cloneCube()
    {
        RubiksCube RC = new RubiksCube();
        RC.turnRecord = turnRecord;
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0;y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    Cube tempCube = new Cube();
                    for (int i = 0; i < 6; i++)
                    {
                        Color tempc = cubeMatrix[x][y][z].getColor((Cube.sides)i);
                        tempCube.setSideColor((Cube.sides)i, tempc);
                    }
                    RC.cubeMatrix[x][y][z] = tempCube;
                }
            }
        }

        return RC;
    }

    public int TurnRecordTokenCount()
    {
        int cost = 0;
        int step = 0;
        while (step < turnRecord.Length)
        {
            char c = turnRecord[step];
            bool clockwise = true;
            if (step + 1 < turnRecord.Length)
            {
                if (turnRecord[step + 1] == 'i')
                {
                    clockwise = false;
                    step++;//increment past inverse character
                }
            }
            step++;
            cost++;
        }

        return cost;
    }

    public void turnCubeToFaceRGBOColorWithYellowOrWhiteOnTop(Color c)
    {
        if (cubeMatrix[1][1][0].getColor(Cube.sides.FRONT) == c)
            return;//do nothing
        else if (cubeMatrix[0][1][1].getColor(Cube.sides.LEFT) == c)
            turnCubeY(true);
        else if (cubeMatrix[2][1][1].getColor(Cube.sides.RIGHT) == c)
            turnCubeY(false);
        else
        {
            turnCubeY(true);
            turnCubeY(true);
        }
    }

}
