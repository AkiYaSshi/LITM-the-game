 using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManager_script : MonoBehaviour
{
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="number">�����s��</param>
    public static void ToScene(int number)
    {
        SceneManager.LoadScene(number);
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
