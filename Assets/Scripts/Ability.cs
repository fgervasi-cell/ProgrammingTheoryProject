using UnityEngine.UI;
using UnityEngine;

public class Ability : Pickup
{
    private Player player;
    private int cooldown;
    private Sprite image;

    protected override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cooldown = Random.Range(10, 20);
        image = GameManager.staticAbilityTextures[Random.Range(0, GameManager.staticAbilityTextures.Length)];
    }

    protected override void ReleaseEffect()
    {
        if (player.abilities.Count < 3)
        {
            player.abilities.Add(this);
            for (int i = 0; i < GameManager.staticPlayerAbilities.Length; i++)
            {
                if (!GameManager.staticPlayerAbilities[i].GetComponent<Image>().sprite)
                {
                    GameManager.staticPlayerAbilities[i].GetComponent<Image>().sprite = image;
                    GameManager.staticPlayerAbilities[i].GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    break;
                }
            }
        }
    }
}
