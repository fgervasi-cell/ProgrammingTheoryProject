using UnityEngine.UI;
using UnityEngine;

public class InfoBoard : MonoBehaviour
{
    public AudioClip buttonClick;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        Button cancelButton = GameObject.Find("CheckButton").GetComponent<Button>();
        cancelButton.onClick.AddListener(CloseInfoBoard);
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void CloseInfoBoard()
    {
        GetComponentInChildren<AudioSource>().PlayOneShot(buttonClick);
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 10000);
        if (player.abilities.Count > 3)
        {
            ExchangeDialog.ShowDialog();
        }
    }
}
