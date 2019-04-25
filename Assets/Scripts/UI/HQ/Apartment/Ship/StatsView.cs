using UnityEngine;
using TMPro;

public class StatsView : Overlay
{
    [SerializeField] TextMeshProUGUI hull; 
    [SerializeField] TextMeshProUGUI shield; 
    [SerializeField] TextMeshProUGUI engine; 
    [SerializeField] TextMeshProUGUI weapon; 
    [SerializeField] TextMeshProUGUI aux;

    PlayerSingleton player;

    private void OnEnable() {
        player = FindObjectOfType<PlayerSingleton>();
        hull.text = player.GetStat(StatType.Hull).GetCalcValue().ToString();
        shield.text = player.GetStat(StatType.Shield).GetCalcValue().ToString();
        engine.text = player.GetStat(StatType.Engine).GetCalcValue().ToString();
        weapon.text = player.GetStat(StatType.Weapon).GetCalcValue().ToString();
        aux.text = player.GetStat(StatType.Aux).GetCalcValue().ToString();
    }
}
