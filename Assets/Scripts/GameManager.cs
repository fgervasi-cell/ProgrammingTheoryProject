using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Player player;
    [SerializeField] private TextAsset abilityDataJson;
    [SerializeField] private Sprite[] abilitySprites;
    public static Sprite[] AbilitySprites;
    public static Ability.AbilityData[] playerAbilities;
    public static GameObject[] playerAbilityPlaceholders;
    public static bool IsInitialized { get; private set; }
    private Text scoreText;
    private Image healthBar;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        playerAbilityPlaceholders = GameObject.FindGameObjectsWithTag("AbilityPlaceholder");
        Utility.SortArrayByObjectNameEnding(playerAbilityPlaceholders);
        playerAbilities = JsonHelper.FromJson<Ability.AbilityData>(abilityDataJson.text);
        AbilitySprites = abilitySprites;
        healthBar = GameObject.Find("PlayerHealthBar").GetComponent<Image>();
        IsInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {player.Score}";
        healthBar.fillAmount = player.LifePoints / player.maxHealth;
    }
}
