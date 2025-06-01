using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [Tooltip("�Ω�p��첾�q���ϼh")]
    [SerializeField]
    private Canvas canvas;

    [Tooltip("�Ω�����ܪ��ѦҪ���]�Ҧp��ܪ��зǦ�m�^")]
    [SerializeField]
    private GameObject ShowObjStandard;
    #endregion

    // ��L�p���ܼơ]���|�X�{�b Inspector ���^
    private Space space;
    private Vector3 LastTransform;
    private Vector3 BackDirection;
    private bool reverse = false;
    private bool isUI;
    private CanvasScaler scaler;

    /// <summary>
    /// �Ұʰʵe����
    /// </summary>
    public void AnimationStart()
    {
        if (!reverse)
        {
            Animation();
            reverse = true;
            return;
        }
        else //�]�w�ۤϤ�V������
        {
            Animation(-1);
            reverse = false;
        }
    }

    private void Animation(int dire = 1)
    {
        foreach (GameObject item in Targets)
        {
            if (!isUI) LastTransform = Object3DMovement(item) * dire; //�DUI����
            else if (isUI) LastTransform = UIMovement(item) * dire; //UI����
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
    }

    #region �y�д���
    /// <summary>
    /// �N���ʤ�V�ഫ���ù��y��
    /// </summary>
    private Vector3 Object3DMovement(GameObject _obj)
    {
        if (moveInScreenSpace && _obj.layer != 5)
        {
            Vector3 screenPosition = cam.WorldToScreenPoint(_obj.transform.position); //����@�ɮy�����ù��y��

            Vector3 targetScreenPosition = screenPosition + Direction; //�b�ù������첾
            targetScreenPosition.x = screenPosition.x + (Direction.x / 1920) * Screen.width;
            targetScreenPosition.y = screenPosition.y + (Direction.y / 1080) * Screen.height;

            return cam.ScreenToWorldPoint(targetScreenPosition) - _obj.transform.position; //�N���ʫ��m��h��e�y�СA�ର�@�ɦV�q
        }
        return Direction;
    }
    /// <summary>
    /// UI����ù��첾�p��
    /// </summary>
    /// <param name="_obj"></param>
    /// <returns></returns>
    private Vector3 UIMovement(GameObject _obj)
    {
        Vector2 refResolution = scaler.referenceResolution;
        float scaleFactor = scaler.scaleFactor;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // �N�۹�첾�q�]��ҡ^�ഫ�� Canvas ���
        float displacementX = Direction.x / refResolution.x * screenSize.x;
        float displacementY = Direction.y / refResolution.y * screenSize.y;
        float displacementZ = Direction.z;

        return new Vector3(displacementX, displacementY, displacementZ);
    }
    #endregion

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
        if(canvas != null)
        {
            isUI = true;
            scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler == null || scaler.referenceResolution == Vector2.zero)
            {
                Debug.LogError("CanvasScaler �� Reference Resolution �����T�]�m�I");
                return;
            }
        }
        space = UseGlobalPosition ? Space.World : Space.Self;
    }

}