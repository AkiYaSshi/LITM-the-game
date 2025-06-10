using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator 組件未附加到該 GameObject");
        }
    }

    // 動畫結束時觸發的函數
    public void OnAnimationEnd()
    {

        SceneManager_script.ToScene(0);
    }


    // 可選：通過參數動態指定函數
    public void OnAnimationEndWithParam(string functionName)
    {
        System.Reflection.MethodInfo method = GetType().GetMethod(functionName);
        if (method != null)
        {
            method.Invoke(this, null);
        }
    }
}