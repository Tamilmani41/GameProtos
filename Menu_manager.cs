using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_manager : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
    }
    public void Play_game()
    {
        // Load the scene by build index or scene name..
        SceneManager.LoadScene(1);
        // Load the next scene in the order...
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif 
    }
}
