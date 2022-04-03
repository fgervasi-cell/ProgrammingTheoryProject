using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class StartMenu : MonoBehaviour
{
    private Button startGameButton;
    private Button settingsButton;
    private Button viewControlsButton;
    private Button exitButton;
    private Button viewHighscoresButton;
    private AudioSource audioSrc;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private AudioClip buttonClick;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        startGameButton = GameObject.Find("StartGameButton").GetComponent<Button>();
        startGameButton.onClick.AddListener(StartGame);
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        settingsButton.onClick.AddListener(ShowSettings);
        viewControlsButton = GameObject.Find("ViewControlsButton").GetComponent<Button>();
        viewControlsButton.onClick.AddListener(ShowControls);
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        exitButton.onClick.AddListener(Exit);
        viewHighscoresButton = GameObject.Find("ViewHighscoresButton").GetComponent<Button>();
        viewHighscoresButton.onClick.AddListener(ShowHighscores);
        audioSrc = GetComponent<AudioSource>();
    }

    void StartGame()
    {
        audioSrc.PlayOneShot(buttonClick);
        SceneManager.LoadScene("MainScene");
    }

    void ShowSettings()
    {
        audioSrc.PlayOneShot(buttonClick);
    }

    void ShowControls()
    {
        audioSrc.PlayOneShot(buttonClick);
    }

    void Exit()
    {
        audioSrc.PlayOneShot(buttonClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    void ShowHighscores()
    {

    }
}
