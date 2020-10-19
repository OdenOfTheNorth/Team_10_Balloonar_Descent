using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
    public string scene;
    public void LoadGameplayScene()
    {
        SceneManager.LoadScene(scene);
    }
}
