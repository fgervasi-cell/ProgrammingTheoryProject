using UnityEngine.UI;
using UnityEngine;

public class ExchangeDialog : MonoBehaviour
{
    private static GameObject[] abilityButtons;
    private GameObject[] borders;
    private static RectTransform rectTransform;
    public AudioClip buttonClick;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        abilityButtons = GameObject.FindGameObjectsWithTag("AbilityButton");
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            int x = i;
            abilityButtons[i].GetComponent<Button>().onClick.AddListener(() => SelectButton(x));
        }
        var buttons = gameObject.GetComponentsInChildren<Button>();
        buttons[buttons.Length - 2].onClick.AddListener(Cancel);
        buttons[buttons.Length - 1].onClick.AddListener(Accept);
        borders = GameObject.FindGameObjectsWithTag("AbilityBorder");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public static void ShowDialog()
    {
        rectTransform = GameObject.Find("Dialog").GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            Image buttonImg = abilityButtons[i].GetComponent<Image>();
            buttonImg.sprite = GameManager.playerAbilityPlaceholders[i].GetComponent<Image>().sprite;
        }
    }

    void SelectButton(int id)
    {
        GetComponent<AudioSource>().PlayOneShot(buttonClick);
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (i == id)
            {
                var border = borders[i].GetComponent<RectTransform>();
                border.sizeDelta = new Vector2(100, 100);
            }
            else
            {
                var border = borders[i].GetComponent<RectTransform>();
                border.sizeDelta = new Vector2(0, 100);
            }
        }
    }

    void Accept()
    {
        // change the selected ability to the ability that was just found
        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (borders[i].GetComponent<RectTransform>().sizeDelta == new Vector2(100, 100))
            {
                Sprite newSprite = player.abilities[player.abilities.Count - 1].image;
                GameManager.playerAbilityPlaceholders[i].GetComponent<Image>().sprite = newSprite;
                player.abilities.RemoveAt(i);
                break;
            }
        }
        Cancel();
    }

    void Cancel()
    {
        rectTransform.anchoredPosition = new Vector2(0, 10000);
        GetComponent<AudioSource>().PlayOneShot(buttonClick);
    }
}
