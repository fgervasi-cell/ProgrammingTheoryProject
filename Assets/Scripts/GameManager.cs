using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Player player;
    [SerializeField] private Text healthDisplay;
    public static Ability.AbilityData[] playerAbilities;
    public static GameObject[] playerAbilityPlaceholders;
    public static bool IsInitialized { get; private set; }
    private Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        playerAbilityPlaceholders = GameObject.FindGameObjectsWithTag("AbilityPlaceholder");
        playerAbilities = JsonHelper.FromJson<Ability.AbilityData>(File.ReadAllText("Assets/Scripts/abilities.json"));
        IsInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = $"Health: {player.LifePoints}";
        scoreText.text = $"Score: {player.Score}";
    }
}
