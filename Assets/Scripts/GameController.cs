using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private SaveAndLoad saveAndLoad;
    [SerializeField] private GameObject[] RoadObjects;
    [SerializeField] private GameObject StartPoint;
    [SerializeField] private GameObject HalfPoint;
    [SerializeField] private GameObject StartPlayerPosition;
    [SerializeField] private GameObject Player;

    [SerializeField] private Text ScoreText;
    [SerializeField] private Text LapsText;

    [SerializeField] private bool isAccelerateMode;
    [SerializeField] private float playerSpeedIncrease;


    [SerializeField] private UnityEvent GameEnd;
    [SerializeField] private UnityEvent GamePaused;
    [SerializeField] private UnityEvent StartGame;
    [SerializeField] private UnityEvent ContinueGame;

    private MovementController playerMovement;
    private int _score, _lap;

    public int Score {
        get => _score;
        set {
            _score = value;
            if (ScoreText != null) ScoreText.text = value.ToString();
        }
    }

    public int Lap
    {
        get => _lap;
        set
        {
            _lap = value;
            if (isAccelerateMode) playerMovement.IncreaseMaxSpeed(this.playerSpeedIncrease);
            if (LapsText != null) LapsText.text = value.ToString();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        this.playerMovement = this.Player.GetComponent<MovementController>();
        this.StartPoint.GetComponent<CollisionController>().OnTriggerEnter2DAction += OnStartTrigger;
        this.HalfPoint.GetComponent<CollisionController>().OnTriggerEnter2DAction += OnHalfTrigger;
        this.RoadObjects[0].GetComponent<CollisionController>().OnCollisionEnter2DAction += OnRoadCollision;
        this.RoadObjects[1].GetComponent<CollisionController>().OnCollisionEnter2DAction += OnRoadCollision;
        this.StartPoint.SetActive(true);
        this.HalfPoint.SetActive(false);

        bool isAccelerate = PlayerPrefs.HasKey("GameMode")?(PlayerPrefs.GetInt("GameMode") == 1 ? true : false):false;
        StartNewGame(isAccelerate);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void StartNewGame(bool isAccelerationMode=false) {
        GUI.StartGame = false;
        this.Lap = 0;
        this.Score = 0;
        this.Player.transform.position = this.StartPlayerPosition.transform.position;
        this.playerMovement.ResetPlayer();
        this.isAccelerateMode = isAccelerateMode;
        //SetGamePause(false);
    }

    public void CallRestartGame()
    {
        MakeSave();
        StartGame?.Invoke();
        StartNewGame(this.isAccelerateMode);
    }
    public void CallGamePause() => this.GamePaused?.Invoke();

    public void CallContinueGame() => this.ContinueGame?.Invoke();

    public void SetGamePause(bool value)
    {
        if (value) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    private void OnStartTrigger(Collider2D collider) {
        this.Score++;
        this.Lap++;
        this.StartPoint.SetActive(false);
        this.HalfPoint.SetActive(true);
    }

    private void OnHalfTrigger(Collider2D collider) {
        this.Score++;
        this.StartPoint.SetActive(true);
        this.HalfPoint.SetActive(false);
    }

    private void OnRoadCollision(Collision2D collision) {
        StartNewGame();
        Debug.Log("GameIsOver!");
        MakeSave();
        GameEnd?.Invoke();
    }

    private void MakeSave() 
    {
        Save save = this.saveAndLoad.GetSave();
        save.TotalDies++;
        save.TotalLaps += this.Lap;
        save.TotalLines += this.Score;
        save.TotalTurns += this.playerMovement.TurnCount;
        save.MaxLapsByGame = (save.MaxLapsByGame < this.Lap) ? this.Lap : save.MaxLapsByGame;
        save.MaxLinesByGame = (save.MaxLinesByGame < this.Score) ? this.Score : save.MaxLinesByGame;
        save.MaxTurnsByGame = (save.MaxTurnsByGame < this.playerMovement.TurnCount) ? this.playerMovement.TurnCount : save.MaxTurnsByGame;

        saveAndLoad.SaveToFile();
    }
}
