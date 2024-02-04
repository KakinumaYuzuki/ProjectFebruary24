using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �c�@�\�����s���R���|�[�l���g
/// �Ǘ��I�u�W�F�N�g�ɃA�^�b�`���Ďg��
/// </summary>
public class BallCounter : MonoBehaviour
{
    [Tooltip("�c�@�Ƃ��ĕ\������X�v���C�g")]
    [SerializeField] Sprite _ballUISprite = null;
    [Tooltip("�c�@�Ƃ��ĕ\������X�v���C�g�̃T�C�Y")]
    [SerializeField] Vector2 _spriteSize = new Vector2(150f, 150f);
    [Tooltip("�c�@�\��������p�l��")]
    [SerializeField] RectTransform _ballCounterPanel = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// �c�@�\�����X�V����
    /// </summary>
    /// <param name="ballCount"></param>
    public void Refresh(int ballCount)
    {
        if (_ballUISprite != null && _ballCounterPanel != null)
        {
            foreach (Transform t in _ballCounterPanel.transform)
            {
                Destroy(t.gameObject);
            }
            Debug.Log("���t���b�V��");
            // �c�@�������X�v���C�g���p�l���̎q�I�u�W�F�N�g�Ƃ��Đ���
            for (int i = 0; i < ballCount - 1; i++)
            {
                // Image�����
                GameObject go = new GameObject();
                Image image = go.AddComponent<Image>();
                // Sprite���A�T�C������
                image.sprite = _ballUISprite;
                // �T�C�Y��ς���
                RectTransform rect = go.GetComponent<RectTransform>();
                rect.sizeDelta = _spriteSize;
                // �p�l���̎q�I�u�W�F�N�g�ɂ���
                go.transform.SetParent(_ballCounterPanel.transform);
            }
        }
    }
}
