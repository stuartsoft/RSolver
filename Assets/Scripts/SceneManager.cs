using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public RubiksCubePrefab RCP;
    Solver S;

    void Start()
    {
        //StartCoroutine(RCP.animateCustomSequence(RCP.RC.sequences[10]));
    }

    public void ScrambleCube()
    {
        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
    }


    public void Solve()
    {
        RubiksCube RC = RCP.RC.cloneCube();
        RubiksCube RC2 = RCP.RC.cloneCube();
        S = new Solver(RC);
        string solution = S.Solution();

        S = new Solver(RC2);
        string trimmedsolution = S.TrimmedSolution();

        Debug.Log(solution);
        Debug.Log(trimmedsolution);

        StartCoroutine(RCP.animateCustomSequence(trimmedsolution));
    }

}
