 using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager_script : MonoBehaviour
{
    /// <summary>
    /// 跳轉場景
    /// </summary>
    /// <param name="number">場景編號</param>
    public static void ToScene(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
