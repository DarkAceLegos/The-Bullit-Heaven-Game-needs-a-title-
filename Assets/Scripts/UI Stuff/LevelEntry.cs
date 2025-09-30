using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelEntry : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;

    private AttackData _attack;
    private LevelingUp _levelingUp;

    public void Init(AttackData attack, LevelingUp levelingUp)
    {
        if(AttackHandler.LoaclInstance == null) return;

        _attack = attack;
        _levelingUp = levelingUp;
        icon.sprite = attack.icon;
        nameText.text = attack.attackName;
        descriptionText.text = attack.GetLevelDescription(AttackHandler.LoaclInstance.getLevel(attack.attackId) + 1);
    }

    public void PickUpgrade()
    {

        Debug.Log(AttackHandler.LoaclInstance);
        if (AttackHandler.LoaclInstance == null) return;

        AttackHandler.LoaclInstance.addAttack(_attack);
        _levelingUp.SetReady();
    }
}
