using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCubePrefab : MonoBehaviour {

    public GameObject CubePrefab;

    public RubiksCube RC;//the actual rubiks cube data structure
    public List<List<List<GameObject>>> cubePrefabMatrix;
    public float spacing = 1.05f;
    public float rotationSpeed = 40;

    // Use this for initialization
    void Start () {
        RC = new RubiksCube();
        
        cubePrefabMatrix = new List<List<List<GameObject>>>();
        for (int x = 0; x < 3; x++)
        {
            List<List<GameObject>> PrefabRow = new List<List<GameObject>>();
            for (int y = 0; y < 3; y++)
            {
                List<GameObject> PrefabColumn = new List<GameObject>();
                for (int z = 0; z < 3; z++)
                {
                    GameObject cubePrefab = Instantiate(CubePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    cubePrefab.transform.SetParent(transform);
                    cubePrefab.transform.position = new Vector3((x - 1), (y - 1), (z - 1)) * spacing;
                    //cubePrefab.GetComponent<CubePrefab>().refreshPanels(RC.cubeMatrix[x][y][z]);
                    PrefabColumn.Add(cubePrefab);
                }
                PrefabRow.Add(PrefabColumn);
            }
            cubePrefabMatrix.Add(PrefabRow);
        }

        StartCoroutine(animateCustomSequence("XYZZiYiXi"));

    }

    void Update()
    {
        RefreshPanels();
        //transform.Rotate(Time.deltaTime * 0, Time.deltaTime * 20, 0);
        //cubePrefabMatrix[0][0][0].transform.RotateAround(transform.position, Vector3.fwd, 10*Time.deltaTime);

    }

    void resetCubePrefabPositions()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    cubePrefabMatrix[i][j][k].transform.position = new Vector3((i - 1), (j - 1), (k - 1)) * spacing;
                    cubePrefabMatrix[i][j][k].transform.rotation = Quaternion.identity;
                }
            }
        }
    }
    
    IEnumerator animateCustomSequence(string seq)
    {

        int step = 0;


        while (step < seq.Length)
        {
            char c = seq[step];//get the character of the turn to run
            bool clockwise = true;
            if (step + 1 < seq.Length)
            {
                if (seq[step + 1] == 'i')//does the next character indicate an inverse operation?
                {
                    clockwise = false;
                    step++;//increment past inverse character
                }
            }
            //===========================

            float totalRotation = 0;
            int dir = 1;
            if (clockwise)
                dir = -1;
            float delta = 0;

            if (c == 'R'){
                while (Mathf.Abs(totalRotation) < 90){
                    delta = -1* dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[2][i][j].transform.RotateAround(transform.position, transform.right, delta); } }
                    yield return null;
                }
                RC.rotateRightFace(clockwise);
            }
            else if (c == 'L')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[0][i][j].transform.RotateAround(transform.position, transform.right, delta); } }
                    yield return null;
                }
                RC.rotateLeftFace(clockwise);
            }
            else if (c == 'U')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = -1 * dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[i][2][j].transform.RotateAround(transform.position, transform.up, delta); } }
                    yield return null;
                }
                RC.rotateTopFace(clockwise);
            }
            else if (c == 'D')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[i][0][j].transform.RotateAround(transform.position, transform.up, delta); } }
                    yield return null;
                }
                RC.rotateBottomFace(clockwise);
            }
            else if (c == 'F')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[i][j][0].transform.RotateAround(transform.position, transform.forward, delta); } }
                    yield return null;
                }
                RC.rotateFrontFace(clockwise);
            }
            else if (c == 'B')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = -1 * dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    for (int i = 0; i < 3; i++) { for (int j = 0; j < 3; j++) { cubePrefabMatrix[i][j][2].transform.RotateAround(transform.position, transform.forward, delta); } }
                    yield return null;
                }
                RC.rotateBackFace(clockwise);
            }
            else if (c == 'X')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    transform.RotateAround(transform.position, transform.right, delta);
                    yield return null;
                }
                RC.turnCubeX(clockwise);
            }
            else if (c == 'Y')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    transform.RotateAround(transform.position, transform.up, delta);
                    yield return null;
                }
                RC.turnCubeY(clockwise);
            }
            else if (c == 'Z')
            {
                while (Mathf.Abs(totalRotation) < 90)
                {
                    delta = dir * rotationSpeed * Time.deltaTime;
                    totalRotation += delta;
                    transform.RotateAround(transform.position, transform.forward, delta);
                    yield return null;
                }
                RC.turnCubeZ(clockwise);
            }


            step++;
            transform.rotation = Quaternion.identity;
            transform.position = Vector3.zero;
            resetCubePrefabPositions();
            RefreshPanels();
        }

        yield return null;
    }

    public void RefreshPanels()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int z = 0; z < 3; z++)
                {
                    cubePrefabMatrix[x][y][z].GetComponent<CubePrefab>().refreshPanels(RC.cubeMatrix[x][y][z]);
                }
            }
        }
    }


}
