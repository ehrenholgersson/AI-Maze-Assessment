using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameControl
{
    public static async void RestartLevel()
    {
        await Task.Delay(4000);
        UIText.DisplayText("Time for a new Adventure!");
        await Task.Delay(4000);
        GameObject.Find("LoadScreen").GetComponent<LoadScreen>().EndScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
