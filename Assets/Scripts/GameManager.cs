using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("ボール")]
    [SerializeField] GameObject _ballPrefab = null; // ボールのプレハブ
    BallCounter _ballCounter;

    [Header("UIテキスト")]
    [Tooltip("残り時間表示テキスト")]
    [SerializeField] public Text timerText;
    [Tooltip("スコア表示テキスト")]
    [SerializeField] public Text scoreText;    // ブロック破壊数〇/〇　フィールドにする必要ないかも
    [Tooltip("ゲームオーバーパネル")]
    [SerializeField] GameObject _gameOverPanel = null;
    
    [Header("BGM")]
    [SerializeField] public AudioSource bgmSource;  // BGMデータ
    [SerializeField] public AudioClip bgm;          // BGMの音量調整

    [Header("シーン名")]
    [SerializeField] public string titleSceneName;  // タイトルシーンの名前
    [SerializeField] public string mainSceneName;   // プレイ画面の名前
    [SerializeField] public string resultSceneName; // リザルト画面の名前

    [Header("ゲームの状態")] public GameState currentGameState = GameState.Standby;
    [Header("ボールの状態")] public BallState currentBallState = BallState.Move;  // ヘッダー、パブリックじゃなくていいかも


    public bool _canControll = false;   // リフレクターを動かせるか
    public bool _canBallMove = false;   // ボールが動けるか
    public bool _stopBallMove = false;  // ボールを止めるか　一時停止用
    bool _addBallMove = true;

    GameObject _stageSettingsObject;
    StageSettings _stageSettingsScript;   // ステージごとのブロック数や時間などをenumで設定したスクリプト
    int _blockCount = 0;        // ブロックの総数
    int _brokenCount = 0;       // ブロックを壊した数
    int _life = 1;              // 残機数
    float _gameTimer;           // 制限時間    

    bool _started = false;
    bool _setting = false;
    bool _deathBall = false;
    bool _stop = false;

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

        // ゲームオーバーテキストを非表示にする ★パネルごとやったほうが良いかも
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }
        _ballCounter = GetComponent<BallCounter>();
    }

    void Update()
    {
        switch (currentGameState)
        {
            case GameState.Title:
                //ResetGame();
                Debug.Log("Title");
                _setting = false;   // リセットのため重要！！　★名前等変更予定
                _canControll = false;   // リフレクター操作できない
                _canBallMove = false;   // ボール動けない
                _stopBallMove = true;   // ボール止める
                // スタートボタンを押したら

                break;
            case GameState.Standby:
                if (!_setting)
                {
                    Debug.Log("set");
                    // ステージごとの設定を取得 GMがシングルトンのためUpdateに書いてる
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _blockCount = _stageSettingsScript.GetStageBlockCount();    // ブロックの総数
                    _life = _stageSettingsScript.GetStageLife();                // 残機
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();      // 制限時間
                    currentGameState = _stageSettingsScript.GetSettingGameState();
                    _setting = true;   // リセットのため重要！！　★名前等変更予定
                }
                Debug.Log("Lキーでスタート");
                if (Input.GetKeyDown(KeyCode.L))
                {
                    currentGameState = GameState.Start;
                }
                _canControll = true;    // リフレクター操作できる
                _canBallMove = false;   // ボール動けない
                _stopBallMove = true;   // ボール止める
                break;
            case GameState.Start:
                // ★時間の設定を書く
                currentGameState = GameState.Play;
                _canControll = true;    // リフレクター操作できる
                _canBallMove = true;    // ボール動ける
                _stopBallMove = false;  // ボール止めない
                break;
            case GameState.Play:
                _gameTimer -= Time.deltaTime;
                ShowTimer(timerText);
                /*if (!_setting)
                {
                    Debug.Log("set");
                    // ステージごとの設定を取得
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _blockCount = _stageSettingsScript.GetStageBlockCount();    // ブロックの総数
                    _life = _stageSettingsScript.GetStageLife();                // 残機
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();      // 制限時間
                    _setting = true;   // リセットのため重要！！　★名前等変更予定
                }*/
                //StartTimer();
                _canControll = true;    // リフレクター操作できる
                _canBallMove = true;    // ボール動ける
                _stopBallMove = false;  // ボール止めない
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StopTimer();
                }   // Pauseへ
                _started = true;
                break;
            case GameState.Pause:
                // ★時間の設定を書く
                //StopTimer();
                _canControll = false;    // リフレクター操作できない
                _canBallMove = false;    // ボール動けない
                _stopBallMove = true;    // ボール止める
                if (Input.GetKeyDown(KeyCode.R))    // キー変更予定　orでescを設定
                {
                    StartTimer();
                }   // Startへ
                _started = false;
                break;
        }

        //Debug.Log("Life = " + _life);
        switch (currentBallState)
        {
            case BallState.NoInstanse:
                Instantiate(_ballPrefab);               // ボールを生成
                currentBallState = BallState.Instanse;  // ボールのステータスを更新
                //_ballCounter.Refresh(_life);            // 残機表示を更新
                Debug.Log("ボールを生成 まだ諦めるな！");
                break;

            case BallState.Instanse:
                currentGameState = GameState.Standby;                
                currentBallState = BallState.Move;
                /*if (Input.GetKeyDown(KeyCode.K))
                {
                    //_canBallMove = true;                // ボールが生成されてKを押した時だけ初速を与えられる
                    //Time.timeScale = 1;               // 時間を再開する
                    currentBallState = BallState.Move;  // ボールのステータスを更新
                    Debug.Log("ゲーム再開！");
                }*/
                break;

            case BallState.Move:
                //_canBallMove = false;
                break;

            case BallState.Destroy:
                // 残機がなければゲームオーバーを表示する
                if (_gameOverPanel != null && _life < 1)
                {
                    _gameOverPanel.SetActive(true);
                    _canControll = false;   // リフレクター操作できない
                    Debug.Log("GameOver");
                }
                else if (_life > 0)
                {
                    //Time.timeScale = 0;                     // 時間を止める ボールの動きを止められればいいのでBallMoveに修正を入れて消す　復活時にアニメーションを使いたいため
                    currentBallState = BallState.NoInstanse;  // ボールのステータスを更新
                    Debug.Log("死亡");
                }
                break;

        }

        //Debug.Log("play = " + _started);
        /*if (_deathBall)
        {
            Time.timeScale = 0;
            if (Input.GetButtonDown("Jump"))
            {
                _deathBall = false;
            }
        }
        else
        {
            Time.timeScale = 1;
        }*/
    }


    //------Ball-------//
    /// <summary>
    /// ボールの残機の処理
    /// ゲームオーバー判定もここ
    /// </summary>
    /// <param name="_gameOverText"></param>
    public void DestroyBoll()
    {
        Debug.Log("DestroyBall");
        _life -= 1;
        //_deathBall = true;
        //Time.timeScale = 0;
        /*if (_life < 1)
        {
            Debug.Log("GameOver");
            _gameOverText.enabled = true;
            // 操作用のフラグをfalseにする↓

        }
        else
        {
            ReviveBall();
        }*/
    }

    /// <summary>
    /// ボールを復活させる
    /// 使用するときは残機があるときのみ
    /// </summary>
    public void ReviveBall()
    {
        Debug.Log("ReviveBall");
        _deathBall = true;
        Instantiate(_ballPrefab);       // ボールを生成
        _ballCounter.Refresh(_life);    // 残機表示を更新
        /*if (Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 1;
        }*/
    }

    //------Block------//
    /// <summary>
    /// 壊したブロック数のカウントを増やす
    /// </summary>
    public void AddBrokenCount()
    {
        _brokenCount++;
    }

    /// <summary>
    /// 壊したブロック数を外部から受け取る
    /// </summary>
    /// <returns></returns>
    public int GetBrokenCount()
    {
        return _brokenCount;
    }

    /// <summary>
    /// スコアを表示
    /// 壊したブロック / ブロックの総数
    /// </summary>
    /// <param name="scoreText"></param>
    public void ShowScore(Text scoreText)
    {
        if (scoreText != null)
        {
            scoreText.text = $"壊したブロック : {_brokenCount} / {_blockCount}";
        }
    }

    //------Scene------//
    /// <summary>
    /// GameStateをTitleにし、インゲームの値を初期化する
    /// タイトルシーンのオブジェクトで使う
    /// </summary>
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
    public void Timer(Text timerText)
    {
        if (Input.GetButtonDown("Jump"))
        {
            // ポーズ再開時に一瞬_canMoveをtrueにして力を加える
            if (_addBallMove)
            {
                _canBallMove = true;
                _addBallMove = false;
            }   // ポーズ中
            _stop = _stop ? false : true;
            Debug.Log("KeyPushed");
            Debug.Log("Stop = " + _stop);
        }

        if (_stop)
        {
            if (currentGameState == GameState.Pause)
            {
                ReproduceTimer();
            }
            else
            {
                StopTimer();
                _addBallMove = true;
            }
        }
        else
        {
            // ポーズではなく、最初の状態なら時間を進める
            if (currentGameState != GameState.Play)
            {
                StartTimer();
            }
            // ポーズ中なら、時間を進め、GameStateをPlayにする
            _addBallMove = false;
        }
        ShowTimer(timerText);
    }

    /// <summary>
    /// 時間を進める
    /// </summary>
    public void StartTimer()
    {
        //_canControll = true;
        //_canMove = true;
        //Time.timeScale = 1;
        //_gameTimer -= Time.deltaTime;   // Time.deltaTimeを変数にしたほうが良いかも ★時間の取得方法考える
        currentGameState = GameState.Start;
    }

    /// <summary>
    /// 時間を進め、GameStateをPlayにする
    /// Pause→Playの時に使う
    /// </summary>
    public void ReproduceTimer()
    {
        //_canControll = true;
        //_gameTimer -= Time.deltaTime;
        currentGameState = GameState.Play;
    }

    /// <summary>
    /// 時間を止め、GameStateをPauseにする
    /// </summary>
    public void StopTimer()
    {
        //_canControll = false;
        //_canBallMove = false;
        //Time.timeScale = 0;
        //_gameTimer += 0;
        currentGameState = GameState.Pause;
    }

    /// <summary>
    /// タイマーを表示
    /// </summary>
    /// <param name="timerText"></param>
    public void ShowTimer(Text timerText)
    {
        if (timerText != null)
        {
            timerText.text = $"残り時間 : {_gameTimer.ToString("00")}";
        }
    }

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {
        Title,
        Standby,
        Start,
        Play,
        Pause,
        InStage,
    }

    /// <summary>
    /// ゲームステートを外部から変更する
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeGameState(GameState nextState) // publicじゃないほうが良いかも
    {
        currentGameState = nextState;
    }

    /// <summary>
    /// ゲームステートを外部から受け取る
    /// </summary>
    /// <returns></returns>
    public GameState GetGameState()
    {
        return currentGameState;
    }

    /// <summary>
    /// インゲームの値を初期化する
    /// </summary>
    public void ResetGame()
    {
        _gameTimer = 0;
        _blockCount = 0;    // いらないかも
        _brokenCount = 0;   //
        _life = 1;
    }

    public enum BallState
    {
        NoInstanse,
        Instanse,
        Move,
        Destroy,
    }

    /// <summary>
    /// ボールステートを外部から変更する
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeBallState(BallState nextState)
    {
        currentBallState = nextState;
    }

    /// <summary>
    /// ボールステートを外部から受け取る
    /// </summary>
    /// <returns></returns>
    public BallState GetBallState()
    {
        return currentBallState;
    }
}
