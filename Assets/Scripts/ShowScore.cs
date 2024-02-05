using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スコア表示用のコンポーネント
/// RestartTextにそれぞれアタッチする
/// モード変更を作るならif文で表示する値を変える
/// </summary>
public class ShowScore : MonoBehaviour
{
    GameManager _gameManager;
    Text _text;
    void Start()
    {
        _gameManager = GameManager.Instance;
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _gameManager.ShowScore(_text);
    }
}
