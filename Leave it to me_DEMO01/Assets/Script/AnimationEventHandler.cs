using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator �ե󥼪��[��� GameObject");
        }
    }

    // �ʵe������Ĳ�o�����
    public void OnAnimationEnd()
    {

        SceneManager_script.ToScene(0);
    }


    // �i��G�q�L�ѼưʺA���w���
    public void OnAnimationEndWithParam(string functionName)
    {
        System.Reflection.MethodInfo method = GetType().GetMethod(functionName);
        if (method != null)
        {
            method.Invoke(this, null);
        }
    }
}