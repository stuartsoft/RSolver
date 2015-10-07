using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RubiksCube : MonoBehaviour {

    public GameObject CubePrefab;

    public List<List<List<GameObject>>> cubePrefabMatrix;
    public List<List<List<Cube>>> cubeRef;

    // Use this for initialization
    void Start () {
        StartCoroutine(SetupCube());
	}
	
    IEnumerator SetupCube()
    {
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
                    yield return new WaitForSeconds(0.25f);
                    GameObject cubePrefab = Instantiate(CubePrefab, Vector3.zero, Quaternion.identity) as GameObject;
                    cubePrefab.transform.SetParent(transform);
                    float spacing = 1.05f;
                    cubePrefab.transform.position = new Vector3((x - 1), (y - 1), (z - 1)) * spacing;
                    Renderer rend = cubePrefab.GetComponent<Renderer>();
                    rend.enabled = true;
                    rend.material.color = Color.black;

                    Cube temp = cubePrefab.GetComponent<Cube>();
                    //temp.setFrontPanelColor(Color.white);
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
    }

    // Update is called once per frame
    void Update () {
        //transform.Rotate(Time.deltaTime * 10, Time.deltaTime * 10, 0.0f);
	}
}
