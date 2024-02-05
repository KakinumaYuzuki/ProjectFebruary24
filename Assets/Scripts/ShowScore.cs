using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �X�R�A�\���p�̃R���|�[�l���g
/// RestartText�ɂ��ꂼ��A�^�b�`����
/// ���[�h�ύX�����Ȃ�if���ŕ\������l��ς���
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
