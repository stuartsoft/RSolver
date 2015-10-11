﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Solver {

    RubiksCube RCube;

    public Solver(RubiksCube RC)
    {
        RCube = RC;
    }

    public string Solution()
    {
        Stage2();
        Stage3();
        RCube.turnCubeZ(true);
        RCube.turnCubeZ(true);
        Stage4();
        Stage5();
        Stage6();
        RCube.turnCubeZ(true);
        RCube.turnCubeZ(true);
        return RCube.turnRecord;
    }

    void Stage2()//Solve the white cross
    {

        for (int i = 0; i < 4; i++)
        {
            Color TargetColor = RCube.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);
            //Debug.Log("TargetColor: " + TargetColor);
            Vector3 Pos = RCube.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(1, 2, 0);
            //Debug.Log("TargetPos: " + TargetPos);
            if (TargetPos != Pos)//if the cube is not where it should be
            {
                if (Pos.y == 2)//is on top row
                {
                    if (Pos.x == 0)
                    {
                        RCube.rotateLeftFace(true);
                    }
                    else if (Pos.x == 2)
                    {
                        RCube.rotateRightFace(false);
                    }
                    else if (Pos.x == 1)
                    {
                        RCube.rotateBackFace(true);
                        RCube.rotateBackFace(true);
                    }

                }
                else if (Pos.y == 1 && Pos.z == 2)//middle row on the back
                {
                    if (Pos.x == 0)
                    {
                        RCube.rotateBackFace(true);
                        RCube.rotateBottomFace(true);
                        RCube.rotateBackFace(false);
                    }
                    if (Pos.x == 2)
                    {
                        RCube.rotateBackFace(false);
                        RCube.rotateBottomFace(true);
                        RCube.rotateBackFace(true);
                    }
                }

                //cube is now on bottom or front
                //requery pos
                Pos = RCube.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                if (Pos.y == 0)//is on bottom
                {
                    while (Pos.z != 0)//while cube is not on the bottom
                    {
                        RCube.rotateBottomFace(true);
                        Pos = RCube.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                    }
                }
                while (Pos.y != 2)
                {
                    RCube.rotateFrontFace(true);
                    Pos = RCube.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
                }
            }
            //cube is now where it should be
            Cube temp = RCube.cubeMatrix[(int)TargetPos.x][(int)TargetPos.y][(int)TargetPos.z];

            if (temp.getColor(Cube.sides.TOP) != Cube.WHITECOLOR)
            {
                RCube.turnCubeY(true);
                RCube.RunSequence(0);
                RCube.turnCubeY(false);
            }
            RCube.turnCubeY(true);
        }
    }
	
    void Stage3()//sove the white corners
    {
        for (int i = 0; i < 4; i++)
        {
            Color TargetColorA = RCube.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);//front center
            Color TargetColorB = RCube.cubeMatrix[2][1][1].getColor(Cube.sides.RIGHT);//right center
            //Debug.Log("TargetColors: " + TargetColorA + TargetColorB);
            Vector3 Pos = RCube.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(2, 2, 0);
            //Debug.Log("TargetPos: " + TargetPos);

            if (TargetPos != Pos)//not in the right spot
            {
                if (Pos.y == 2)//in the top but in the wrong spot
                {
                    if (Pos.z == 0)//top front left pos
                    {
                        RCube.rotateLeftFace(true);
                        RCube.rotateBottomFace(true);
                        RCube.rotateLeftFace(false);
                    }
                    if (Pos.z == 2)//top back left or top back right
                    {
                        if (Pos.x == 0)
                        {
                            RCube.rotateLeftFace(false);
                            RCube.rotateBottomFace(true);
                            RCube.rotateLeftFace(true);
                        }
                        if (Pos.x == 2)
                        {
                            RCube.rotateRightFace(true);
                            RCube.rotateBottomFace(true);
                            RCube.rotateRightFace(false);
                        }
                    }
                }
                //cube is now on the bottom
                Pos = RCube.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                while (Pos != new Vector3(2, 0, 0))
                {
                    RCube.rotateBottomFace(true);
                    Pos = RCube.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                }
                //cube should now be directly below it's target position or already in target position
            }

            while (true)
            {
                Pos = RCube.cornerCubeWithColors(TargetColorA, TargetColorB, Cube.WHITECOLOR);
                Cube tempCube = RCube.cubeMatrix[(int)Pos.x][(int)Pos.y][(int)Pos.z];
                if (Pos == TargetPos && tempCube.getColor(Cube.sides.FRONT) == TargetColorA && tempCube.getColor(Cube.sides.TOP) == Cube.WHITECOLOR)
                    break;

                RCube.RunSequence(1);
            }

            RCube.turnCubeY(true);
        }
    }

    void Stage4()//solve the middle row side cubes...
    {
        for (int i = 0; i < 4; i++) {

            Color TargetColorA = RCube.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);//front center
            Color TargetColorB = RCube.cubeMatrix[2][1][1].getColor(Cube.sides.RIGHT);//right center
            //Debug.Log("TargetColors: " + TargetColorA + TargetColorB);
            Vector3 Pos = RCube.sideCubeWithColors(TargetColorA, TargetColorB);
            //Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(2, 1, 0);
            //Debug.Log("TargetPos: " + TargetPos);

            if (Pos != TargetPos || RCube.cubeMatrix[(int)TargetPos.x][(int)TargetPos.y][(int)TargetPos.z].getColor(Cube.sides.FRONT) != TargetColorA)
            {//cube is not in the right place or is oriented wrong
                if (Pos.y != 2)//not in the top
                {
                    if (Pos.z == 0)//front
                    {
                        if (Pos.x == 0)//front left middle row
                            RCube.RunSequence(3);//force the cube out of 0,1,0
                        else if (Pos.x == 2)//front right middle row
                            RCube.RunSequence(2);//force the cube out of 2,1,0
                    }
                    else
                    {
                        RCube.turnCubeY(true);
                        RCube.turnCubeY(true);

                        if (Pos.x == 0)//front left middle row
                            RCube.RunSequence(2);//force the cube out of 0,1,0
                        else if (Pos.x == 2)//front right middle row
                            RCube.RunSequence(3);//force the cube out of 2,1,0

                        RCube.turnCubeY(true);
                        RCube.turnCubeY(true);
                    }
                }

                //cube is now guaranteed to be in top row
                Pos = RCube.sideCubeWithColors(TargetColorA, TargetColorB);
                while (Pos != new Vector3(1, 2, 0))
                {
                    RCube.rotateTopFace(true);
                    Pos = RCube.sideCubeWithColors(TargetColorA, TargetColorB);
                }

                //cube is now in 1,2,0

                if (RCube.cubeMatrix[(int)Pos.x][(int)Pos.y][(int)Pos.z].getColor(Cube.sides.FRONT) != TargetColorA)
                {
                    RCube.rotateTopFace(false);
                    RCube.turnCubeY(false);
                    RCube.RunSequence(3);
                    RCube.turnCubeY(true);
                }
                else
                {
                    RCube.RunSequence(2);
                }
            }

            //cube is now in the target pos and is oriented correctly

            RCube.turnCubeY(true);
        }

    }

    void Stage5()//solve the yellow side
    {
        while (!RCube.isYellowCrossOnTop())
        {
            for (int i= 0; i < 4; i++)
            {
                if (RCube.isYellowBackwardsLOnTop())
                    break;
                if (RCube.isYellowHorizontalLineOnTop())
                    break;

                RCube.rotateTopFace(true);
            }
            //cube is now oriented with either a yellow L or horizontal line or just a yellow center

            if (RCube.isYellowHorizontalLineOnTop())
                RCube.RunSequence(5);
            else
                RCube.RunSequence(4);
        }

        //cube now has a yellow cross on top

        while (!RCube.isTopAllYellow())
        {
            int state = 0;

            for (int i = 0; i < 4; i++)
            {
                Cube topleftback = RCube.cubeMatrix[0][2][2];
                Cube toprightback = RCube.cubeMatrix[2][2][2];

                if (topleftback.getColor(Cube.sides.TOP) == Cube.YELLOWCOLOR && toprightback.getColor(Cube.sides.TOP) == Cube.YELLOWCOLOR)
                {
                    state = 3;
                    break;
                }
                RCube.turnCubeY(true);
            }

            if (state == 0)//no two adjacent yellow sides were found
            {
                for (int i = 0; i < 4; i++)
                {
                    Cube topleftfront = RCube.cubeMatrix[0][2][0];

                    if (topleftfront.getColor(Cube.sides.TOP) == Cube.YELLOWCOLOR)
                    {
                        state = 2;
                        break;
                    }
                    RCube.turnCubeY(true);
                }
            }

            if (state == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    Cube topleftfront = RCube.cubeMatrix[0][2][0];

                    if (topleftfront.getColor(Cube.sides.LEFT) == Cube.YELLOWCOLOR)
                    {
                        state = 1;
                        break;
                    }
                    RCube.turnCubeY(true);
                }
            }

            //cube is now oriented and ready for sequence
            RCube.RunSequence(6);
        }
    }

    void Stage6()//solve the rest
    {
        bool AllCornersCorrect = false;

        //checkif all corners are solved in any orientaion of the top face
        for (int i = 0; i < 4; i++){
            if (RCube.allTopCornersSolved()){
                AllCornersCorrect = true;
                break;
            }
            RCube.rotateTopFace(true);
        }

        while (!AllCornersCorrect)
        {

            bool foundTwoAdjacentMatchingCorners = false;
            //try to find to adjacent corners with the same color and place them in the back
            for (int i = 0; i < 4; i++)//checking for adjacent corners
            {
                Color TopFrontLeft = RCube.cubeMatrix[0][2][0].getColor(Cube.sides.FRONT);
                Color TopFrontRight = RCube.cubeMatrix[2][2][0].getColor(Cube.sides.FRONT);
                if (TopFrontLeft == TopFrontRight)
                {
                    foundTwoAdjacentMatchingCorners = true;
                    break;
                }
                RCube.turnCubeY(true);
            }

            if (foundTwoAdjacentMatchingCorners)
            {
                Color TopFrontLeft = RCube.cubeMatrix[0][2][0].getColor(Cube.sides.FRONT);
                Color MiddleFrontCenter = RCube.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);

                while (TopFrontLeft != MiddleFrontCenter)
                {
                    RCube.rotateTopFace(true);
                    RCube.turnCubeY(true);
                    TopFrontLeft = RCube.cubeMatrix[0][2][0].getColor(Cube.sides.FRONT);
                    MiddleFrontCenter = RCube.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);
                }

                RCube.turnCubeY(true);
                RCube.turnCubeY(true);
            }

            //wheather or not we found two corners on the same side, the cube should be in the right orientation now to run the sequence.
            RCube.RunSequence(7);

            //checkif all corners are solved in any orientaion of the top face
            for (int i = 0; i < 4; i++)
            {
                if (RCube.allTopCornersSolved())
                {
                    AllCornersCorrect = true;
                    break;
                }
                RCube.rotateTopFace(true);
            }
        }

        //all yellow corners are now in the right spot
        //now to fix the top side pieces

        while (!RCube.isSolved())
        {
            for (int i = 0; i < 4; i++)
            {
                Color TopFrontLeft = RCube.cubeMatrix[0][2][0].getColor(Cube.sides.FRONT);
                Color TopFrontMiddle = RCube.cubeMatrix[1][2][0].getColor(Cube.sides.FRONT);

                if (TopFrontLeft == TopFrontMiddle)
                {
                    RCube.turnCubeY(true);
                    RCube.turnCubeY(true);
                    break;
                }
                RCube.turnCubeY(true);
            }

            //we should now have either a matching face on the back or there are not matching faces
            //now run the last sequence

            RCube.RunSequence(9);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
