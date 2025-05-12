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
    /// <summary>
    /// �ʵe�ƪ��ؼЪ���}�C
    /// </summary>
    [SerializeField]
    private GameObject[] Targets;

    /// <summary>
    /// �ʵe�����C�|
    /// </summary>
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

    /// <summary>
    /// ��e��ܪ��ʵe����
    /// </summary>
    [SerializeField]
    private AnimationType animationType;

    /// <summary>
    /// �ʵe����������
    /// </summary>
    [SerializeField]
    private iTween.EaseType BetweenType;
    private Space space;

    /// <summary>
    /// �ʵe����V�M���ܶq
    /// </summary>
    [SerializeField]
    private Vector3 Direction;
    private Vector3 LastTransform;
    private Vector3 BackDirection;

    /// <summary>
    /// �ʵe������ɶ�
    /// </summary>
    [SerializeField]
    private float AnimationTime;

    /// <summary>
    /// �ʵe����}�l�ɶ�
    /// </summary>
    [SerializeField]
    private float Delay = 0;

    /// <summary>
    /// �O�_���ù��y�Эp�Ⲿ��
    /// </summary>
    [SerializeField]
    private bool moveInScreenSpace = true;
    [SerializeField]
    private bool UseGlobalPosition = true;
    bool reverse = false;

    /// <summary>
    /// ��v���Ѧ�
    /// </summary>
    private Camera cam;
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion
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