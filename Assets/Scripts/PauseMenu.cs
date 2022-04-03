using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(AudioSource))]
public class PauseMenu : MonoBehaviour
{
    private Button resumeButton;
    private Button backToMenuButton;
    private Button saveGameButton;
    private CanvasGroup canvasGroup;
    private AudioSource audioSrc;
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip menuOpen;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(() => ResumeGame(true));
        backToMenuButton = GameObject.Find("BackToMenuButton").GetComponent<Button>();
        backToMenuButton.onClick.AddListener(BackToMenu);
        saveGameButton = GameObject.Find("SaveGame").GetComponent<Button>();
        saveGameButton.onClick.AddListener(SaveGame);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            audioSrc.PlayOneShot(menuOpen);
            if (canvasGroup.alpha == 0)
            {
                ShowMenu();
            }
            else
            {
                ResumeGame(false);
            }
       }
    }

    void ShowMenu()
    {
        Time.timeScale = 0;
        canvasGroup.alpha = 1;
    }

    void ResumeGame(bool playSound)
    {
        if (playSound)
        {
            audioSrc.PlayOneShot(buttonClick);
        }
        Time.timeScale = 1;
        canvasGroup.alpha = 0;
    }

    void BackToMenu()
    {
        Time.timeScale = 1;
        audioSrc.PlayOneShot(buttonClick);
        SceneManager.LoadScene("StartMenu");
    }

    void SaveGame()
    {
        audioSrc.PlayOneShot(buttonClick);
    }
}
