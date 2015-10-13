using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

    public RubiksCubePrefab RCP;
    Solver S;
    public Text txtTurnRecord;
    public Slider SpeedSlider;
    public Text txtAnimationSpeed;
    public Toggle toggleRotateCamera;
    public bool rotateCamera = true;
    Vector3 cameraResetPos = new Vector3(4, 4, -4);

    private IEnumerator coroutine;

    void Start()
    {
        txtTurnRecord = txtTurnRecord.GetComponent<Text>();
        SpeedSlider = SpeedSlider.GetComponent<Slider>();
        txtAnimationSpeed = txtAnimationSpeed.GetComponent<Text>();
        SpeedSlider.value = RCP.rotationSpeed;
        setAnimationSpeed(RCP.rotationSpeed);
        toggleRotateCamera = toggleRotateCamera.GetComponent<Toggle>();
        toggleRotateCamera.isOn = rotateCamera;

        Camera.main.transform.position = cameraResetPos;
        Camera.main.transform.LookAt(RCP.transform.position);
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
            OptimizedSolve();
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

    public void ScrambleCube()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
    }

    public void Solve()
    {
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
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void OptimizedSolve()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        RubiksCube RC = RCP.RC.cloneCube();
        S = new Solver(RC);
        string solution = S.OptimizedSolution();
        RubiksCube solCube = new RubiksCube();
        solCube.RunCustomSequence(solution);
        coroutine = RCP.animateCustomSequence(solution);
        StartCoroutine(coroutine);
        txtTurnRecord.text = solution;
        Debug.Log(solution);
        Debug.Log(solCube.TurnRecordTokenCount() + " Moves");
    }

    public void setAnimationSpeed(float speed)
    {
        txtAnimationSpeed.text = "Animation Speed: " + (int)speed;
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
