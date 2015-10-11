using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public RubiksCubePrefab RCP;
    Solver S;

    void Start()
    {
    }

    public void ScrambleCube()
    {
        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
    }

    public void Solve()
    {
        RubiksCube tempRC = RCP.RC.cloneCube();
        S = new Solver(RCP.RC);
        string solution = S.Solution();
        RCP.RC = tempRC;
        //StartCoroutine(RCP.animateCustomSequence(solution));
        Debug.Log(solution)

        //S = new Solver(RCP.RC);
        //string solution = S.Solution();
        //RCP.RefreshPanels();
    }

}
