using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool ModusDevelopment;

    public string PlayerLeftName { get; set; } = "Player 1";
    public string PlayerRightName { get; set; } = "Player 2";
    public int CharacterAmount { get; set; } = 0;

    /// <summary>
    /// Awake method.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Object remains after scene change.
        }
        else
        {
            Destroy(gameObject); // Destroy the new instance instead of the old one.
        }
    }

    /// <summary>
    /// Sets the player names and character amount for the level.
    /// </summary>
    /// <param name="playerLeftName"></param>
    /// <param name="playerRightName"></param>
    /// <param name="characterAmount"></param>
    public void SetLevelData(string playerLeftName, string playerRightName, int characterAmount)
    {
        PlayerLeftName = playerLeftName;
        PlayerRightName = playerRightName;
        CharacterAmount = characterAmount;
    }

    /// <summary>
    /// Sets the default player names.
    /// </summary>
    public void SetDefault()
    {
        PlayerLeftName = "Player 1";
        PlayerRightName = "Player 2";
    }

    /// <summary>
    //// Loads the specified scene.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Log(string text)
    {
               Debug.Log(text);
    }
}

