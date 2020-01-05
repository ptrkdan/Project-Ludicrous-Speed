using UnityEngine;
using TMPro;

public class StatsView : Overlay
{
    [SerializeField] private TextMeshProUGUI hull;
    [SerializeField] private TextMeshProUGUI shield;
    [SerializeField] private TextMeshProUGUI engine;
    [SerializeField] private TextMeshProUGUI weapon;
    [SerializeField] private TextMeshProUGUI aux;

    private PlayerSingleton player;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerSingleton>();
        hull.text = player.GetStat(StatType.Hull).Value.ToString();
        shield.text = player.GetStat(StatType.Shield).Value.ToString();
        engine.text = player.GetStat(StatType.Engine).Value.ToString();
        weapon.text = player.GetStat(StatType.Weapon).Value.ToString();
        aux.text = player.GetStat(StatType.Aux).Value.ToString();
    }
}
