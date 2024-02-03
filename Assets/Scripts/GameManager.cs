using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("残り時間表示テキスト")] public Text timerText;
    [Header("スコア表示テキスト")] public Text scoreText;    // ブロック破壊数〇/〇　フィールドにする必要ないかも
    
    [Header("BGM")]
    [SerializeField] public AudioSource bgmSource;  // BGMデータ
    [SerializeField] public AudioClip bgm;          // BGMの音量調整

    [Header("シーン名")]
    [SerializeField] public string titleSceneName;  // タイトルシーンの名前
    [SerializeField] public string mainSceneName;   // プレイ画面の名前
    [SerializeField] public string resultSceneName; // リザルト画面の名前

    [Header("ゲームの状態")] public GameState currentGameState = GameState.Play;

    GameObject _stageSettingsObject;
    StageSettings _stageSettingsScript;   // ステージごとのブロック数や時間などをenumで設定したスクリプト
    int _blockCount = 0;    // ブロックの総数
    int _brokenCount = 0;   // ブロックを壊した数
    float _gameTimer;       // 制限時間

    bool _started = false;
    bool _setting = false;
    // シングルトン
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        //ResetGame();    // ゲームの情報(時間、壊した数)を初期化　いらないかも それぞれのシーンで呼ぶ必要あり

        //_stageSettingsObject = GameObject.Find("StageSettings");
        //_stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
        //_blockCount = _stageSettingsScript.GetStageBlockCount();   // ステージごとの総ブロック数を取得
        //_gameTimer = _stageSettingsScript.GetStageTimeLimit();   // ステージごとの制限時間を取得

    }

    void Start()
    {
        //_stageSettingsScript = FindObjectOfType<StageSettings>();

        //_stageSettingsObject = GameObject.Find("StageSettings");
        //_stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
        //_blockCount = _stageSettingsScript.GetStageBlockCount();   // ステージごとの総ブロック数を取得
        //_gameTimer = _stageSettingsScript.GetStageTimeLimit();   // ステージごとの制限時間を取得
    }

    void Update()
    {
        switch (currentGameState)
        {
            case GameState.Title:
                //ResetGame();
                Debug.Log("Title");
                _setting = false;   // リセットのため重要！！　★名前等変更予定
                break;
            case GameState.Play:
                if (!_setting)
                {
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();   // ステージごとの制限時間を取得
                    _setting = true;   // リセットのため重要！！　★名前等変更予定
                }
                //StartTimer();
                _started = true;
                break;
            case GameState.Pause:
                //StopTimer();
                _started = false;
                break;

        }
        Debug.Log("play = " + _started);
    }

    //------Block------//

    public void AddBrokenCount()
    {
        _brokenCount++;
    }

    public int GetBrokenCount()
    {
        return _brokenCount;
    }

    public void ShowScore(Text scoreText)
    {
        if (scoreText != null)
        {
            scoreText.text = $"壊したブロック : {_brokenCount} / {_blockCount}";
        }
    }

    //------Scene------//
    public void Title()
    {
        currentGameState = GameState.Title;
        ResetGame();
    }
    /*public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }*/ //SceneLorderに移動

    //------Timer------//
    public void StopTimer()
    {
        Time.timeScale = 0;
        currentGameState = GameState.Pause;
    }

    public void StartTimer()
    {
        Time.timeScale = 1;
        _gameTimer -= Time.deltaTime;   // Time.deltaTimeを変数にしたほうが良いかも
        currentGameState = GameState.Play;
    }

    public void ShowTimer(Text timerText)
    {
        if (timerText != null)
        {
            timerText.text = $"残り時間 : {_gameTimer.ToString("00")}";
        }
    }

    public enum GameState
    {
        Title,
        Play,
        Pause,
        InStage,
    }
    public void ChangeGameState(GameState nextState) // publicじゃないほうが良いかも
    {
        currentGameState = nextState;
    }

    public void ResetGame()
    {
        _gameTimer = 0;
        //_blockCount = 0;    // いらないかも
        _brokenCount = 0;   //
    }
}
