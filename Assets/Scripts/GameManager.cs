using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�{�[��")]
    [SerializeField] GameObject _ballPrefab = null; // �{�[���̃v���n�u
    BallCounter _ballCounter;

    [Header("UI�e�L�X�g")]
    [Tooltip("�c�莞�ԕ\���e�L�X�g")]
    [SerializeField] public Text timerText;
    [Tooltip("�X�R�A�\���e�L�X�g")]
    [SerializeField] public Text scoreText;    // �u���b�N�j�󐔁Z/�Z�@�t�B�[���h�ɂ���K�v�Ȃ�����
    [Tooltip("�Q�[���I�[�o�[�p�l��")]
    [SerializeField] GameObject _gameOverPanel = null;
    
    [Header("BGM")]
    [SerializeField] public AudioSource bgmSource;  // BGM�f�[�^
    [SerializeField] public AudioClip bgm;          // BGM�̉��ʒ���

    [Header("�V�[����")]
    [SerializeField] public string titleSceneName;  // �^�C�g���V�[���̖��O
    [SerializeField] public string mainSceneName;   // �v���C��ʂ̖��O
    [SerializeField] public string resultSceneName; // ���U���g��ʂ̖��O

    [Header("�Q�[���̏��")] public GameState currentGameState = GameState.Standby;
    [Header("�{�[���̏��")] public BallState currentBallState = BallState.Move;  // �w�b�_�[�A�p�u���b�N����Ȃ��Ă�������


    public bool _canControll = false;   // ���t���N�^�[�𓮂����邩
    public bool _canBallMove = false;   // �{�[���������邩
    public bool _stopBallMove = false;  // �{�[�����~�߂邩�@�ꎞ��~�p
    bool _addBallMove = true;

    GameObject _stageSettingsObject;
    StageSettings _stageSettingsScript;   // �X�e�[�W���Ƃ̃u���b�N���⎞�ԂȂǂ�enum�Őݒ肵���X�N���v�g
    int _blockCount = 0;        // �u���b�N�̑���
    int _brokenCount = 0;       // �u���b�N���󂵂���
    int _life = 1;              // �c�@��
    float _gameTimer;           // ��������    

    bool _started = false;
    bool _setting = false;
    bool _deathBall = false;
    bool _stop = false;

    // �V���O���g��
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
        //ResetGame();    // �Q�[���̏��(���ԁA�󂵂���)���������@����Ȃ����� ���ꂼ��̃V�[���ŌĂԕK�v����

        //_stageSettingsObject = GameObject.Find("StageSettings");
        //_stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
        //_blockCount = _stageSettingsScript.GetStageBlockCount();   // �X�e�[�W���Ƃ̑��u���b�N�����擾
        //_gameTimer = _stageSettingsScript.GetStageTimeLimit();   // �X�e�[�W���Ƃ̐������Ԃ��擾

    }

    void Start()
    {
        //_stageSettingsScript = FindObjectOfType<StageSettings>();

        //_stageSettingsObject = GameObject.Find("StageSettings");
        //_stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
        //_blockCount = _stageSettingsScript.GetStageBlockCount();   // �X�e�[�W���Ƃ̑��u���b�N�����擾
        //_gameTimer = _stageSettingsScript.GetStageTimeLimit();   // �X�e�[�W���Ƃ̐������Ԃ��擾

        // �Q�[���I�[�o�[�e�L�X�g���\���ɂ��� ���p�l�����Ƃ�����ق����ǂ�����
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
                _setting = false;   // ���Z�b�g�̂��ߏd�v�I�I�@�����O���ύX�\��
                _canControll = false;   // ���t���N�^�[����ł��Ȃ�
                _canBallMove = false;   // �{�[�������Ȃ�
                _stopBallMove = true;   // �{�[���~�߂�
                // �X�^�[�g�{�^������������

                break;
            case GameState.Standby:
                if (!_setting)
                {
                    Debug.Log("set");
                    // �X�e�[�W���Ƃ̐ݒ���擾 GM���V���O���g���̂���Update�ɏ����Ă�
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _blockCount = _stageSettingsScript.GetStageBlockCount();    // �u���b�N�̑���
                    _life = _stageSettingsScript.GetStageLife();                // �c�@
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();      // ��������
                    currentGameState = _stageSettingsScript.GetSettingGameState();
                    _setting = true;   // ���Z�b�g�̂��ߏd�v�I�I�@�����O���ύX�\��
                }
                Debug.Log("L�L�[�ŃX�^�[�g");
                if (Input.GetKeyDown(KeyCode.L))
                {
                    currentGameState = GameState.Start;
                }
                _canControll = true;    // ���t���N�^�[����ł���
                _canBallMove = false;   // �{�[�������Ȃ�
                _stopBallMove = true;   // �{�[���~�߂�
                break;
            case GameState.Start:
                // �����Ԃ̐ݒ������
                currentGameState = GameState.Play;
                _canControll = true;    // ���t���N�^�[����ł���
                _canBallMove = true;    // �{�[��������
                _stopBallMove = false;  // �{�[���~�߂Ȃ�
                break;
            case GameState.Play:
                _gameTimer -= Time.deltaTime;
                ShowTimer(timerText);
                /*if (!_setting)
                {
                    Debug.Log("set");
                    // �X�e�[�W���Ƃ̐ݒ���擾
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _blockCount = _stageSettingsScript.GetStageBlockCount();    // �u���b�N�̑���
                    _life = _stageSettingsScript.GetStageLife();                // �c�@
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();      // ��������
                    _setting = true;   // ���Z�b�g�̂��ߏd�v�I�I�@�����O���ύX�\��
                }*/
                //StartTimer();
                _canControll = true;    // ���t���N�^�[����ł���
                _canBallMove = true;    // �{�[��������
                _stopBallMove = false;  // �{�[���~�߂Ȃ�
                if (Input.GetKeyDown(KeyCode.R))
                {
                    StopTimer();
                }   // Pause��
                _started = true;
                break;
            case GameState.Pause:
                // �����Ԃ̐ݒ������
                //StopTimer();
                _canControll = false;    // ���t���N�^�[����ł��Ȃ�
                _canBallMove = false;    // �{�[�������Ȃ�
                _stopBallMove = true;    // �{�[���~�߂�
                if (Input.GetKeyDown(KeyCode.R))    // �L�[�ύX�\��@or��esc��ݒ�
                {
                    StartTimer();
                }   // Start��
                _started = false;
                break;
        }

        //Debug.Log("Life = " + _life);
        switch (currentBallState)
        {
            case BallState.NoInstanse:
                Instantiate(_ballPrefab);               // �{�[���𐶐�
                currentBallState = BallState.Instanse;  // �{�[���̃X�e�[�^�X���X�V
                //_ballCounter.Refresh(_life);            // �c�@�\�����X�V
                Debug.Log("�{�[���𐶐� �܂����߂�ȁI");
                break;

            case BallState.Instanse:
                currentGameState = GameState.Standby;                
                currentBallState = BallState.Move;
                /*if (Input.GetKeyDown(KeyCode.K))
                {
                    //_canBallMove = true;                // �{�[�������������K��������������������^������
                    //Time.timeScale = 1;               // ���Ԃ��ĊJ����
                    currentBallState = BallState.Move;  // �{�[���̃X�e�[�^�X���X�V
                    Debug.Log("�Q�[���ĊJ�I");
                }*/
                break;

            case BallState.Move:
                //_canBallMove = false;
                break;

            case BallState.Destroy:
                // �c�@���Ȃ���΃Q�[���I�[�o�[��\������
                if (_gameOverPanel != null && _life < 1)
                {
                    _gameOverPanel.SetActive(true);
                    _canControll = false;   // ���t���N�^�[����ł��Ȃ�
                    Debug.Log("GameOver");
                }
                else if (_life > 0)
                {
                    //Time.timeScale = 0;                     // ���Ԃ��~�߂� �{�[���̓������~�߂���΂����̂�BallMove�ɏC�������ď����@�������ɃA�j���[�V�������g����������
                    currentBallState = BallState.NoInstanse;  // �{�[���̃X�e�[�^�X���X�V
                    Debug.Log("���S");
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
    /// �{�[���̎c�@�̏���
    /// �Q�[���I�[�o�[���������
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
            // ����p�̃t���O��false�ɂ��遫

        }
        else
        {
            ReviveBall();
        }*/
    }

    /// <summary>
    /// �{�[���𕜊�������
    /// �g�p����Ƃ��͎c�@������Ƃ��̂�
    /// </summary>
    public void ReviveBall()
    {
        Debug.Log("ReviveBall");
        _deathBall = true;
        Instantiate(_ballPrefab);       // �{�[���𐶐�
        _ballCounter.Refresh(_life);    // �c�@�\�����X�V
        /*if (Input.GetButtonDown("Jump"))
        {
            Time.timeScale = 1;
        }*/
    }

    //------Block------//
    /// <summary>
    /// �󂵂��u���b�N���̃J�E���g�𑝂₷
    /// </summary>
    public void AddBrokenCount()
    {
        _brokenCount++;
    }

    /// <summary>
    /// �󂵂��u���b�N�����O������󂯎��
    /// </summary>
    /// <returns></returns>
    public int GetBrokenCount()
    {
        return _brokenCount;
    }

    /// <summary>
    /// �X�R�A��\��
    /// �󂵂��u���b�N / �u���b�N�̑���
    /// </summary>
    /// <param name="scoreText"></param>
    public void ShowScore(Text scoreText)
    {
        if (scoreText != null)
        {
            scoreText.text = $"�󂵂��u���b�N : {_brokenCount} / {_blockCount}";
        }
    }

    //------Scene------//
    /// <summary>
    /// GameState��Title�ɂ��A�C���Q�[���̒l������������
    /// �^�C�g���V�[���̃I�u�W�F�N�g�Ŏg��
    /// </summary>
    public void Title()
    {
        currentGameState = GameState.Title;
        ResetGame();
    }
    /*public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }*/ //SceneLorder�Ɉړ�

    //------Timer------//
    public void Timer(Text timerText)
    {
        if (Input.GetButtonDown("Jump"))
        {
            // �|�[�Y�ĊJ���Ɉ�u_canMove��true�ɂ��ė͂�������
            if (_addBallMove)
            {
                _canBallMove = true;
                _addBallMove = false;
            }   // �|�[�Y��
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
            // �|�[�Y�ł͂Ȃ��A�ŏ��̏�ԂȂ玞�Ԃ�i�߂�
            if (currentGameState != GameState.Play)
            {
                StartTimer();
            }
            // �|�[�Y���Ȃ�A���Ԃ�i�߁AGameState��Play�ɂ���
            _addBallMove = false;
        }
        ShowTimer(timerText);
    }

    /// <summary>
    /// ���Ԃ�i�߂�
    /// </summary>
    public void StartTimer()
    {
        //_canControll = true;
        //_canMove = true;
        //Time.timeScale = 1;
        //_gameTimer -= Time.deltaTime;   // Time.deltaTime��ϐ��ɂ����ق����ǂ����� �����Ԃ̎擾���@�l����
        currentGameState = GameState.Start;
    }

    /// <summary>
    /// ���Ԃ�i�߁AGameState��Play�ɂ���
    /// Pause��Play�̎��Ɏg��
    /// </summary>
    public void ReproduceTimer()
    {
        //_canControll = true;
        //_gameTimer -= Time.deltaTime;
        currentGameState = GameState.Play;
    }

    /// <summary>
    /// ���Ԃ��~�߁AGameState��Pause�ɂ���
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
    /// �^�C�}�[��\��
    /// </summary>
    /// <param name="timerText"></param>
    public void ShowTimer(Text timerText)
    {
        if (timerText != null)
        {
            timerText.text = $"�c�莞�� : {_gameTimer.ToString("00")}";
        }
    }

    /// <summary>
    /// �Q�[���̏��
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
    /// �Q�[���X�e�[�g���O������ύX����
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeGameState(GameState nextState) // public����Ȃ��ق����ǂ�����
    {
        currentGameState = nextState;
    }

    /// <summary>
    /// �Q�[���X�e�[�g���O������󂯎��
    /// </summary>
    /// <returns></returns>
    public GameState GetGameState()
    {
        return currentGameState;
    }

    /// <summary>
    /// �C���Q�[���̒l������������
    /// </summary>
    public void ResetGame()
    {
        _gameTimer = 0;
        _blockCount = 0;    // ����Ȃ�����
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
    /// �{�[���X�e�[�g���O������ύX����
    /// </summary>
    /// <param name="nextState"></param>
    public void ChangeBallState(BallState nextState)
    {
        currentBallState = nextState;
    }

    /// <summary>
    /// �{�[���X�e�[�g���O������󂯎��
    /// </summary>
    /// <returns></returns>
    public BallState GetBallState()
    {
        return currentBallState;
    }
}
