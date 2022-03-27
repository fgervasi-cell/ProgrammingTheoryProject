using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class Ability : Pickup
{
    private Player player;
    public Sprite image;
    public string description;
    public string abilityName;
    public int cooldown;
    public int damage;
    private GameObject infoBoard;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        infoBoard = GameObject.Find("InfoBoard");
    }

    public void SetImage(string path)
    {
        image = AssetDatabase.LoadAssetAtPath<Sprite>(path);
    }

    protected override void ReleaseEffect()
    {
        player.abilities.Add(this);
        for (int i = 0; i < GameManager.playerAbilityPlaceholders.Length; i++)
        {
            if (!GameManager.playerAbilityPlaceholders[i].GetComponent<Image>().sprite && player.abilities.Count <= 3)
            {
                GameManager.playerAbilityPlaceholders[i].GetComponent<Image>().sprite = image;
                GameManager.playerAbilityPlaceholders[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                break;
            }
        }
        ShowInfoBoard();
    }

    private void ShowInfoBoard()
    {
        var rectTransform = infoBoard.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        Text[] textComponents = infoBoard.GetComponentsInChildren<Text>();
        textComponents[0].text = description;
        textComponents[1].text = abilityName;
        textComponents[2].text = "Cooldown: " + cooldown;
        textComponents[3].text = "Damage: " + damage;
    }

    [System.Serializable]
    public class AbilityData
    {
        public string name;
        public string description;
        public string imgPath;
        public int cooldown;
        public int damage;
    }
}
