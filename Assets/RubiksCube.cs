using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {

    public GameObject CubePrefab;

    public List<List<List<GameObject>>> cubePrefabMatrix;
    public List<List<List<Cube>>> cubeRef;
    public float spacing = 1.05f;

    // Use this for initialization
    void Start () {
        cubePrefabMatrix = new List<List<List<GameObject>>>();
        cubeRef = new List<List<List<Cube>>>();

        for (int x = 0; x < 3; x++)
        {
            List<List<GameObject>> GOPlane = new List<List<GameObject>>();
            List<List<Cube>> CubePlane = new List<List<Cube>>();
            for (int y = 0; y < 3; y++)
            {
                List<GameObject> GORow = new List<GameObject>();
                List<Cube> CubeRow = new List<Cube>();
                for (int z = 0; z < 3; z++)
                {
                    //yield return new WaitForSeconds(0.1f);
                    //yield return null;
                    GameObject cubePrefab = Instantiate(CubePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    cubePrefab.transform.SetParent(transform);
                    cubePrefab.transform.position = new Vector3((x - 1), (y - 1), (z - 1)) * spacing;

                    Cube temp = cubePrefab.GetComponent<Cube>();

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


        cubeRef[0][2][0].rotateY(1);

    }

    // Update is called once per frame
    void Update () {
        //transform.Rotate(Time.deltaTime*20, Time.deltaTime * 20, 0.0f);
	}

    List<List<Cube>> getFrontFace()
    {
        List<List<Cube>> face = new List<List<Cube>>();
        for (int i = 0; i < 3; i++)
        {
            List<Cube> row = new List<Cube>();
            for (int j= 0; j < 3; j++)
            {
                row.Add(cubeRef[i][j][0]);
            }
            face.Add(row);
        }

        return face;
    }

    public void rotateFrontFace(int clockwise)
    {

    }
}
