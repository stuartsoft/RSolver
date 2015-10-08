using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {

    public GameObject CubePrefab;

    public List<List<List<GameObject>>> cubePrefabMatrix;
    public List<List<List<CubePiece>>> cubeRef;
    public float spacing = 1.05f;

    // Use this for initialization
    void Start () {
        cubePrefabMatrix = new List<List<List<GameObject>>>();
        cubeRef = new List<List<List<CubePiece>>>();

        for (int x = 0; x < 3; x++)
        {
            List<List<GameObject>> GOPlane = new List<List<GameObject>>();
            List<List<CubePiece>> CubePlane = new List<List<CubePiece>>();
            for (int y = 0; y < 3; y++)
            {
                List<GameObject> GORow = new List<GameObject>();
                List<CubePiece> CubeRow = new List<CubePiece>();
                for (int z = 0; z < 3; z++)
                {
                    //yield return new WaitForSeconds(0.1f);
                    //yield return null;
                    GameObject cubePrefab = Instantiate(CubePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    cubePrefab.transform.SetParent(transform);
                    cubePrefab.transform.position = new Vector3((x - 1), (y - 1), (z - 1)) * spacing;

                    CubePiece temp = cubePrefab.GetComponent<CubePiece>();

                    temp.setAllSideColors(Cube.BLACKCOLOR);
                    GORow.Add(cubePrefab);
                    CubeRow.Add(temp);

                }
                GOPlane.Add(GORow);
                CubePlane.Add(CubeRow);
            }
            cubePrefabMatrix.Add(GOPlane);
            cubeRef.Add(CubePlane);
        }
        //transform.rotation = Quaternion.Euler(new Vector3(45,0,45));

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                cubeRef[i][j][0].setSideColor(Cube.sides.FRONT, Cube.REDCOLOR);
                cubeRef[2][i][j].setSideColor(Cube.sides.RIGHT, Cube.BLUECOLOR);
                cubeRef[0][i][j].setSideColor(Cube.sides.LEFT, Cube.GREENCOLOR);
                cubeRef[i][j][2].setSideColor(Cube.sides.BACK, Cube.ORANGECOLOR);
                cubeRef[i][2][j].setSideColor(Cube.sides.TOP, Cube.WHITECOLOR);
                cubeRef[i][0][j].setSideColor(Cube.sides.BOTTOM, Cube.YELLOWCOLOR);
            }
        }
        cubeRef[1][1][1].setAllSideColors(Cube.BLACKCOLOR);


        //cubeRef[0][2][0].rotateY(1);
        
        rotateBackFace(true);
        rotateFrontFace(true);
    }

    // Update is called once per frame
    void Update () {
        //transform.Rotate(Time.deltaTime*20, Time.deltaTime * 20, 0.0f);
	}

    List<List<Cube>> getCubeXFace(int r)
    {
        List<List<Cube>> face = new List<List<Cube>>();
        for (int i = 0; i < 3; i++)
        {
            List<Cube> row = new List<Cube>();
            for (int j = 0; j < 3; j++)
            {
                Cube tempcube = new Cube(cubeRef[i][j][r].getColors());
                row.Add(tempcube);
                
            }
            face.Add(row);
        }

        return face;
    }

    List<List<CubePiece>> getCubePieceXFace(int r)
    {
        List<List<CubePiece>> face = new List<List<CubePiece>>();
        for (int i = 0; i < 3; i++)
        {
            List<CubePiece> row = new List<CubePiece>();
            for (int j = 0; j < 3; j++){row.Add(cubeRef[i][j][r]);}
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
        for (int i = 0; i < 3; i++) {
            outline.Add(face[0][i]);
        }
        outline.Add(face[1][2]);
        for (int i = 2; i >= 0; i--){
            outline.Add(face[2][i]);
        }
        outline.Add(face[1][0]);

        return outline;
    }
    List<CubePiece> getOutline(List<List<CubePiece>> face)
    {
        //converts a 2d matrix of cubes to a linear list of the outline cubes of the provided face
        //the list is ordered in a clockwise direction, starting with the lower left cube [0][0]
        //getOutline returns a list of references to the origional matrix
        List<CubePiece> outline = new List<CubePiece>();
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
            List<Cube> oldFrontOutline = getOutline(getCubeXFace(0));
            List<CubePiece> currentFrontOutline = getOutline(getCubePieceXFace(0));
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
            List<Cube> oldFrontOutline = getOutline(getCubeXFace(2));
            List<CubePiece> currentFrontOutline = getOutline(getCubePieceXFace(2));
            for (int i = 0; i < 8; i++)
            {
                currentFrontOutline[i].setSideColors(oldFrontOutline[(i + 2) % 8].getColors());
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
                currentFrontOutline[i].rotateZ();
            }
        }
    }
}
