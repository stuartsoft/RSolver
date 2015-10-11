using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public RubiksCubePrefab RCP;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyUp(KeyCode.A))
        {
            //RCP.RC.rotateTopFace(true);
            //Debug.Log(RCP.RC.allTopCornersSolved());
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            //RCP.RC.rotateTopFace(false);
            //Debug.Log(RCP.RC.allTopCornersSolved());

        }
    }
}
