using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public RubiksCubePrefab RCP;
    Solver S;
    public Text txtTurnRecord;

    void Start()
    {
        txtTurnRecord = txtTurnRecord.GetComponent<Text>();
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
        S = new Solver(RC);
        string solution = S.Solution();
        StartCoroutine(RCP.animateCustomSequence(solution));
        txtTurnRecord.text = solution;
    }

    public void TrimmedSolve()
    {
        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);
        string solution = S.TrimmedSolution();
        StartCoroutine(RCP.animateCustomSequence(solution));
        txtTurnRecord.text = solution;
    }

}
