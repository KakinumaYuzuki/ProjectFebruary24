using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�c�莞�ԕ\���e�L�X�g")] public Text timerText;
    [Header("�X�R�A�\���e�L�X�g")] public Text scoreText;    // �u���b�N�j�󐔁Z/�Z�@�t�B�[���h�ɂ���K�v�Ȃ�����
    
    [Header("BGM")]
    [SerializeField] public AudioSource bgmSource;  // BGM�f�[�^
    [SerializeField] public AudioClip bgm;          // BGM�̉��ʒ���

    [Header("�V�[����")]
    [SerializeField] public string titleSceneName;  // �^�C�g���V�[���̖��O
    [SerializeField] public string mainSceneName;   // �v���C��ʂ̖��O
    [SerializeField] public string resultSceneName; // ���U���g��ʂ̖��O

    [Header("�Q�[���̏��")] public GameState currentGameState = GameState.Play;

    GameObject _stageSettingsObject;
    StageSettings _stageSettingsScript;   // �X�e�[�W���Ƃ̃u���b�N���⎞�ԂȂǂ�enum�Őݒ肵���X�N���v�g
    int _blockCount = 0;    // �u���b�N�̑���
    int _brokenCount = 0;   // �u���b�N���󂵂���
    float _gameTimer;       // ��������

    bool _started = false;
    bool _setting = false;
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
    }

    void Update()
    {
        switch (currentGameState)
        {
            case GameState.Title:
                //ResetGame();
                Debug.Log("Title");
                _setting = false;   // ���Z�b�g�̂��ߏd�v�I�I�@�����O���ύX�\��
                break;
            case GameState.Play:
                if (!_setting)
                {
                    _stageSettingsObject = GameObject.Find("StageSettings");
                    _stageSettingsScript = _stageSettingsObject.GetComponent<StageSettings>();
                    _gameTimer = _stageSettingsScript.GetStageTimeLimit();   // �X�e�[�W���Ƃ̐������Ԃ��擾
                    _setting = true;   // ���Z�b�g�̂��ߏd�v�I�I�@�����O���ύX�\��
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
            scoreText.text = $"�󂵂��u���b�N : {_brokenCount} / {_blockCount}";
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
    }*/ //SceneLorder�Ɉړ�

    //------Timer------//
    public void StopTimer()
    {
        Time.timeScale = 0;
        currentGameState = GameState.Pause;
    }

    public void StartTimer()
    {
        Time.timeScale = 1;
        _gameTimer -= Time.deltaTime;   // Time.deltaTime��ϐ��ɂ����ق����ǂ�����
        currentGameState = GameState.Play;
    }

    public void ShowTimer(Text timerText)
    {
        if (timerText != null)
        {
            timerText.text = $"�c�莞�� : {_gameTimer.ToString("00")}";
        }
    }

    public enum GameState
    {
        Title,
        Play,
        Pause,
        InStage,
    }
    public void ChangeGameState(GameState nextState) // public����Ȃ��ق����ǂ�����
    {
        currentGameState = nextState;
    }

    public void ResetGame()
    {
        _gameTimer = 0;
        //_blockCount = 0;    // ����Ȃ�����
        _brokenCount = 0;   //
    }
}
