using UnityEngine;
using System.Collections;

public class Solver : MonoBehaviour {

    public RubiksCubePrefab RCP;

	// Use this for initialization
	void Start () {
        RCP.RC.Scramble(50);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
