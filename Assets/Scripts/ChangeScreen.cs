using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // ����� ��� ������ ����
    public void StartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(1);
    }

    // ����� ��� ������ �� ����
    public void ExitGame()
    {

        // ���� ���� �������� � ���������, �� ���������� ����
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // ���� ���� �������� ��� ������, �� ������� ����������
        Application.Quit();
        #endif
    }

    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(0);
    }

    public void Score()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadSceneAsync(4);
    }
}
