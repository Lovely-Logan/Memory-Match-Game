using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public string levelName; // Level to load

    public void StartGame() // Start the game
    {
        SceneManager.LoadScene(levelName); // Load the level
    }
    public void QuitGame() // Quit the game
    {
        Application.Quit(); // Quit the game
    }

}
