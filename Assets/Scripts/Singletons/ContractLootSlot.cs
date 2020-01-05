using UnityEngine;
using UnityEngine.UI;

public class ContractLootSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;

    public void SetIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }
}
