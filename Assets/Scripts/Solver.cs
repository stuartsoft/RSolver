using UnityEngine;
using System.Collections;

public class Solver : MonoBehaviour {

    public RubiksCubePrefab RCP;

	// Use this for initialization
	void Start () {
        RCP.RC.Scramble(50);
        Stage2();
	}

    void Stage2()//Solve the white cross
    {

        for (int i = 0; i < 4; i++)
        {
            Color TargetColor = RCP.RC.cubeMatrix[1][1][0].getColor(Cube.sides.FRONT);
            Debug.Log("TargetColor: " + TargetColor);
            Vector3 Pos = RCP.RC.sideCubeWithColors(TargetColor, Cube.WHITECOLOR);
            Debug.Log("Pos: " + Pos);
            Vector3 TargetPos = new Vector3(1, 2, 0);
            Debug.Log("TargetPos: " + TargetPos);
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
                Debug.Log("running sequence 0");
                RCP.RC.turnCubeY(true);
                RCP.RC.RunSequence(0);
                RCP.RC.turnCubeY(false);
            }
            RCP.RC.turnCubeY(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
