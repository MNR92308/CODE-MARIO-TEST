using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // ต้องเป็น public เท่านั้น ถึงจะมองเห็นใน On Click ()
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // หรือแบบระบุ Index ของ Scene
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // ออกจากเกม
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // หยุด Play ใน Editor
#else
        Application.Quit(); // ออกจากเกมจริงตอน Build
#endif
    }
}