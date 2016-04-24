using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public RubiksCubePrefab RCP;
    Solver S;
    public Text txtTurnRecord;
    public Text txtNumMoves;
    public bool rotateCamera = true;
    Vector3 cameraResetPos = new Vector3(4, 4, -4);

    private IEnumerator coroutine;

    void Start()
    {
        txtTurnRecord = txtTurnRecord.GetComponent<Text>();
        txtNumMoves = txtNumMoves.GetComponent<Text>();
        setAnimationSpeed(RCP.rotationSpeed);

        //Camera.main.transform.position = cameraResetPos;
        //Camera.main.transform.LookAt(RCP.transform.position);
    }

    public void Update()
    {
        if (rotateCamera)
            Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10);

        if (Input.GetKeyDown(KeyCode.H))//halt
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            RCP.resetCubePrefabPositions();
            RCP.RefreshPanels();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SearchedAndTrimmedSolve();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Solve();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ScrambleCube();
        }
        else if (Input.GetKeyDown(KeyCode.F1)){
            runCheckerboard();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            runSixDot();
        }
    }

    public void EasyScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(3);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void ScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
        txtNumMoves.text = "";
    }

    public void Solve()
    {
        Debug.Log("Standard Solve");

        if (coroutine != null)
            StopCoroutine(coroutine);

        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);

        string solution = S.Solution();

        RubiksCube solCube = new RubiksCube();
        solCube.RunCustomSequence(solution);
        coroutine = RCP.animateCustomSequence(solution);
        StartCoroutine(coroutine);
        txtTurnRecord.text = solution;
        txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void TrimmedSolve()
    {
        Debug.Log("Trimmed Solve");

        if (coroutine != null)
            StopCoroutine(coroutine);

        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);

        string solution = S.Solution();
        solution = S.trimTurnRecord(solution);

        RubiksCube solCube = new RubiksCube();
        solCube.RunCustomSequence(solution);
        coroutine = RCP.animateCustomSequence(solution);
        StartCoroutine(coroutine);
        txtTurnRecord.text = solution;
        txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void SearchedSolve()
    {
        Debug.Log("SearchedSolve");

        if (coroutine != null)
            StopCoroutine(coroutine);

        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);

        string solution = S.SearchedSolution();

        RubiksCube solCube = new RubiksCube();
        solCube.RunCustomSequence(solution);
        coroutine = RCP.animateCustomSequence(solution);
        StartCoroutine(coroutine);
        txtTurnRecord.text = solution;
        txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void SearchedAndTrimmedSolve()
    {
        Debug.Log("SearchedAndTrimmedSolve");
        if (coroutine != null)
            StopCoroutine(coroutine);

        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);

        string solution = S.SearchedSolution();
        solution = S.trimTurnRecord(solution);

        RubiksCube solCube = new RubiksCube();
        solCube.RunCustomSequence(solution);
        coroutine = RCP.animateCustomSequence(solution);
        StartCoroutine(coroutine);
        txtTurnRecord.text = solution;
        txtNumMoves.text = solCube.TurnRecordTokenCount() + " Moves";
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void setAnimationSpeed(float speed)
    {
        RCP.rotationSpeed = speed;
    }

    public void setCameraRotation(bool on)
    {
        rotateCamera = on;
        Camera.main.transform.position = cameraResetPos;
        Camera.main.transform.LookAt(RCP.transform.position);
    }

    public void runCheckerboard()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = RCP.animateCustomSequence(RCP.RC.sequences[10]);
        StartCoroutine(coroutine);
    }

    public void runSixDot()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = RCP.animateCustomSequence(RCP.RC.sequences[11]);
        StartCoroutine(coroutine);
    }

}
