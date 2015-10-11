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
        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);
        string solution = S.Solution();
        Debug.Log(solution);

    }

}
