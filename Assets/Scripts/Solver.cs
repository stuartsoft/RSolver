using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Solver : MonoBehaviour {

    public RubiksCubePrefab RCP;

	// Use this for initialization
	void Start () {
        RCP.RC.Scramble(50);
        Stage2();
        Stage3();
        RCP.RC.turnCubeZ(true);
        RCP.RC.turnCubeZ(true);
        Stage4();
        Stage5();
	}

    void Stage2()//Solve the white cross
    {

        for (int i = 0; i < 4; i++)
        {
            Color TargetColor = RCP.RC.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);
            //Debug.Log("TargetColor: " + TargetColor);
            Vector3 Pos = RCP.RC.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(1, 2, 0);
            //Debug.Log("TargetPos: " + TargetPos);
            if (TargetPos != Pos)//if the cube is not where it should be
            {
                if (Pos.y == 2)//is on top row
                {
                    if (Pos.x == 0)
                    {
                        RCP.RC.rotateLeftFace(true);
                    }
                    else if (Pos.x == 2)
                    {
                        RCP.RC.rotateRightFace(false);
                    }
                    else if (Pos.x == 1)
                    {
                        RCP.RC.rotateBackFace(true);
                        RCP.RC.rotateBackFace(true);
                    }

                }
                else if (Pos.y == 1 && Pos.z == 2)//middle row on the back
                {
                    if (Pos.x == 0)
                    {
                        RCP.RC.rotateBackFace(true);
                        RCP.RC.rotateBottomFace(true);
                        RCP.RC.rotateBackFace(false);
                    }
                    if (Pos.x == 2)
                    {
                        RCP.RC.rotateBackFace(false);
                        RCP.RC.rotateBottomFace(true);
                        RCP.RC.rotateBackFace(true);
                    }
                }

                //cube is now on bottom or front
                //requery pos
                Pos = RCP.RC.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                if (Pos.y == 0)//is on bottom
                {
                    while (Pos.z != 0)//while cube is not on the bottom
                    {
                        RCP.RC.rotateBottomFace(true);
                        Pos = RCP.RC.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                    }
                }
                while (Pos.y != 2)
                {
                    RCP.RC.rotateFrontFace(true);
                    Pos = RCP.RC.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                }
            }
            //cube is now where it should be
            Cube temp = RCP.RC.cubeMatrix[(int)TargetPos.x][(int)TargetPos.y][(int)TargetPos.z];

            if (temp.getColor(Cube.sides.TOP) != Cube.WHITECOLOR)
            {
                RCP.RC.turnCubeY(true);
                RCP.RC.RunSequence(0);
                RCP.RC.turnCubeY(false);
            }
            RCP.RC.turnCubeY(true);
        }
    }
	
    void Stage3()//sove the white corners
    {
        for (int i = 0; i < 4; i++)
        {
            Color TargetColorA = RCP.RC.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);//front center
            Color TargetColorB = RCP.RC.cubeMatrix[2][1][1].getColor(Cube.sides.RIGHT);//right center
            //Debug.Log("TargetColors: " + TargetColorA + TargetColorB);
            Vector3 Pos = RCP.RC.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(2, 2, 0);
            //Debug.Log("TargetPos: " + TargetPos);

            if (TargetPos != Pos)//not in the right spot
            {
                if (Pos.y == 2)//in the top but in the wrong spot
                {
                    if (Pos.z == 0)//top front left pos
                    {
                        RCP.RC.rotateLeftFace(true);
                        RCP.RC.rotateBottomFace(true);
                        RCP.RC.rotateLeftFace(false);
                    }
                    if (Pos.z == 2)//top back left or top back right
                    {
                        if (Pos.x == 0)
                        {
                            RCP.RC.rotateLeftFace(false);
                            RCP.RC.rotateBottomFace(true);
                            RCP.RC.rotateLeftFace(true);
                        }
                        if (Pos.x == 2)
                        {
                            RCP.RC.rotateRightFace(true);
                            RCP.RC.rotateBottomFace(true);
                            RCP.RC.rotateRightFace(false);
                        }
                    }
                }
                //cube is now on the bottom
                Pos = RCP.RC.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                while (Pos != new Vector3(2, 0, 0))
                {
                    RCP.RC.rotateBottomFace(true);
                    Pos = RCP.RC.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                }
                //cube should now be directly below it's target position or already in target position
            }

            while (true)
            {
                Pos = RCP.RC.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                Cube tempCube = RCP.RC.cubeMatrix[(int)Pos.x][(int)Pos.y][(int)Pos.z];
                if (Pos == TargetPos && tempCube.getColor(Cube.sides.FRONT) == TargetColorA && tempCube.getColor(Cube.sides.TOP) == Cube.WHITECOLOR)
                    break;

                RCP.RC.RunSequence(1);
            }

            RCP.RC.turnCubeY(true);
        }
    }

    void Stage4()//solve the middle row side cubes...
    {
        for (int i = 0; i < 4; i++) {

            Color TargetColorA = RCP.RC.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);//front center
            Color TargetColorB = RCP.RC.cubeMatrix[2][1][1].getColor(Cube.sides.RIGHT);//right center
            //Debug.Log("TargetColors: " + TargetColorA + TargetColorB);
            Vector3 Pos = RCP.RC.sideCubeWithColors(TargetColorA, TargetColorB);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(2, 1, 0);
            //Debug.Log("TargetPos: " + TargetPos);

            if (Pos != TargetPos || RCP.RC.cubeMatrix[(int)TargetPos.x][(int)TargetPos.y][(int)TargetPos.z].getColor(Cube.sides.FRONT) != TargetColorA)
            {//cube is not in the right place or is oriented wrong
                if (Pos.y != 2)//not in the top
                {
                    if (Pos.z == 0)//front
                    {
                        if (Pos.x == 0)//front left middle row
                            RCP.RC.RunSequence(3);//force the cube out of 0,1,0
                        else if (Pos.x == 2)//front right middle row
                            RCP.RC.RunSequence(2);//force the cube out of 2,1,0
                    }
                    else
                    {
                        RCP.RC.turnCubeY(true);
                        RCP.RC.turnCubeY(true);

                        if (Pos.x == 0)//front left middle row
                            RCP.RC.RunSequence(2);//force the cube out of 0,1,0
                        else if (Pos.x == 2)//front right middle row
                            RCP.RC.RunSequence(3);//force the cube out of 2,1,0

                        RCP.RC.turnCubeY(true);
                        RCP.RC.turnCubeY(true);
                    }
                }

                //cube is now guaranteed to be in top row
                Pos = RCP.RC.sideCubeWithColors(TargetColorA, TargetColorB);
                while (Pos != new Vector3(1, 2, 0))
                {
                    RCP.RC.rotateTopFace(true);
                    Pos = RCP.RC.sideCubeWithColors(TargetColorA, TargetColorB);
                }

                //cube is now in 1,2,0

                if (RCP.RC.cubeMatrix[(int)Pos.x][(int)Pos.y][(int)Pos.z].getColor(Cube.sides.FRONT) != TargetColorA)
                {
                    RCP.RC.rotateTopFace(false);
                    RCP.RC.turnCubeY(false);
                    RCP.RC.RunSequence(3);
                    RCP.RC.turnCubeY(true);
                }
                else
                {
                    RCP.RC.RunSequence(2);
                }
            }

            //cube is now in the target pos and is oriented correctly

            RCP.RC.turnCubeY(true);
        }

    }

    void Stage5()//solve the yellow side
    {
        while (!RCP.RC.isYellowCrossOnTop())
        {
            for (int i= 0; i < 4; i++)
            {
                if (RCP.RC.isYellowBackwardsLOnTop())
                    break;
                if (RCP.RC.isYellowHorizontalLineOnTop())
                    break;

                RCP.RC.rotateTopFace(true);
            }
            //cube is now oriented with either a yellow L or horizontal line or just a yellow center

            if (RCP.RC.isYellowHorizontalLineOnTop())
                RCP.RC.RunSequence(5);
            else
                RCP.RC.RunSequence(4);
        }

        //cube now has a yellow cross on top

    }

    // Update is called once per frame
    void Update () {
	
	}
}
