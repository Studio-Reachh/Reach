using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private LevelLoader _levelLoader;

    [Header("Audio")]
    public string Audio;

    private void Awake()
    {
        _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll();
        _levelLoader.LoadNextLevel("Room01", true, Audio);
    }

    public void ContinueGame()
    {
        _levelLoader.LoadNextLevel("Room01", false, Audio);
    }
}
