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

    void Start()
    {
        txtTurnRecord = txtTurnRecord.GetComponent<Text>();
        SpeedSlider = SpeedSlider.GetComponent<Slider>();
        txtAnimationSpeed = txtAnimationSpeed.GetComponent<Text>();
        SpeedSlider.value = RCP.rotationSpeed;
        setAnimationSpeed(RCP.rotationSpeed);
        toggleRotateCamera = toggleRotateCamera.GetComponent<Toggle>();
        toggleRotateCamera.isOn = rotateCamera;

        Camera.main.transform.position = new Vector3(5, 5, -5);
        Camera.main.transform.LookAt(RCP.transform.position);
        //StartCoroutine(RCP.animateCustomSequence(RCP.RC.sequences[10]));
    }

    public void Update()
    {
        if (rotateCamera)
            Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * 10);
    }

    public void ScrambleCube()
    {
        RCP.RC.Scramble(50);
        RCP.RefreshPanels();
        txtTurnRecord.text = "";
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

    public void setAnimationSpeed(float speed)
    {
        txtAnimationSpeed.text = "Animation Speed: " + (int)speed;
        RCP.rotationSpeed = speed;
    }

    public void setCameraRotation(bool on)
    {
        rotateCamera = on;
        Camera.main.transform.position = new Vector3(5,5,-5);
        Camera.main.transform.LookAt(RCP.transform.position);
    }

    public void runCheckerboard()
    {
        StartCoroutine(RCP.animateCustomSequence(RCP.RC.sequences[10]));
        //RCP.RC.RunSequence(10);
    }

}
