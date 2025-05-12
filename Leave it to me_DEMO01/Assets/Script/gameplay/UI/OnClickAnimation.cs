using System;
using System.Collections;
using System.Net.NetworkInformation;
using UnityEngine;

/// <summary>
/// �B�z���s�I���᪺�ʵe�ĪG
/// </summary>
public class OnClickAnimation : MonoBehaviour
{
    #region �ܼƫŧi
    [Header("�ʵe�ؼ�")]
    [Tooltip("��ܭn���ΰʵe���ؼЪ���}�C")]
    [SerializeField]
    private GameObject[] Targets;

    [Header("�ʵe�����P�]�m")]
    [Tooltip("��ܰʵe�������G�ưʩ��Y��")]
    [SerializeField]
    private AnimationType animationType;

    public enum AnimationType
    {
        /// <summary>
        /// �ưʰʵe
        /// </summary>
        SLIDE,
        /// <summary>
        /// �Y��ʵe
        /// </summary>
        SCALE
    }

    [Tooltip("��ܰʵe�����������]�Ҧp�u�ʡB�w�J�w�X���^")]
    [SerializeField]
    private iTween.EaseType BetweenType;

    [Header("�ʵe�Ѽ�")]
    [Tooltip("�ʵe����V�M���ܶq�]�Ҧp�ưʶZ�����Y���ҡ^")]
    [SerializeField]
    private Vector3 Direction;

    [Tooltip("�ʵe������ɶ��]���G��^")]
    [SerializeField]
    private float AnimationTime;

    [Tooltip("�ʵe�}�l�e������ɶ��]���G��^")]
    [SerializeField]
    private float Delay = 0;

    [Header("�y�лP�Ŷ�")]
    [Tooltip("�O�_���ù��y�Эp�Ⲿ�ʡ]�Ӥ��O�@�ɮy�С^")]
    [SerializeField]
    private bool moveInScreenSpace = true;

    [Tooltip("�O�_�ϥΥ�����m�]�v�T�y�Эp��覡�^")]
    [SerializeField]
    private bool UseGlobalPosition = true;

    [Header("��v���P�ѦҪ���")]
    [Tooltip("�Ω�p��ù��y�Ъ���v���]�Y���ūh�ϥΥD��v���^")]
    [SerializeField]
    private Camera cam;

    [Tooltip("�Ω�����ܪ��ѦҪ���]�Ҧp��ܪ��зǦ�m�^")]
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion

    // ��L�p���ܼơ]���|�X�{�b Inspector ���^
    private Space space;
    private Vector3 LastTransform;
    private Vector3 BackDirection;
    private bool reverse = false;

    /// <summary>
    /// �Ұʰʵe����
    /// </summary>
    public void AnimationStart()
    {
        if (!reverse)
        {
            foreach (GameObject item in Targets)
            {
                LastTransform = ToScreenMovement(item);
                Hashtable hashtable = SetHash();
                switch (animationType)
                {
                    case AnimationType.SLIDE:
                        iTween.MoveBy(item, hashtable);
                        break;
                    case AnimationType.SCALE:
                        iTween.ScaleBy(item, hashtable);
                        break;
                }
            }
            reverse = true;
            return;
        }
        else //�]�w�ۤϤ�V������
        {

            foreach (GameObject item in Targets)
            {
                LastTransform = -ToScreenMovement(item);
                Hashtable hashtable = SetHash();
                switch (animationType)
                {
                    case AnimationType.SLIDE:
                        iTween.MoveBy(item, hashtable);
                        break;
                    case AnimationType.SCALE:
                        iTween.ScaleBy(item, hashtable);
                        break;
                }
            }
            reverse = false;
        }
    }

    /// <summary>
    /// �N���ʤ�V�ഫ���ù��y��
    /// </summary>
    private Vector3 ToScreenMovement(GameObject _obj)
    {
        if (moveInScreenSpace && _obj.layer != 5)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(_obj.transform.position); //����@�ɮy�����ù��y��

            Vector3 targetScreenPosition = screenPosition + Direction; //�b�ù������첾

            return cam.ScreenToWorldPoint(targetScreenPosition) - _obj.transform.position; //�N���ʫ��m��h��e�y�СA�ର�@�ɦV�q
        }
        return Direction;
    }

    /// <summary>
    /// �]�m iTween �� Hash ��Ѽ�
    /// </summary>
    /// <returns>��^�t�m�n�� Hash ��</returns>
    private Hashtable SetHash()
    {
        return new()
        {
            { "amount", LastTransform },
            { "time", AnimationTime },
            { "delay", Delay },
            { "easetype", BetweenType },
            {"space", space}
        };
    }
    /// <summary>
    /// ��l�Ʈ�����D��v��
    /// </summary>
    private void Start()
    {
        cam = Camera.main;
        space = UseGlobalPosition ? Space.World : Space.Self;
    }

}