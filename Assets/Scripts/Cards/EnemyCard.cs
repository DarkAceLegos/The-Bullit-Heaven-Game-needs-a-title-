using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Card/EnemyCard")]
public class EnemyCard : Cards//, IDataPersistence
{
    [SerializeField] public int amountOfPacks; //{ get; private set; }
    [SerializeField] public int packsSize; //{ get; set; }
    [SerializeField] public int typeOfEnemy;  //{ get; set; }

    /*public void LoadData(GameData progression)
    {
        progression.cardInventory.TryGetValue(cardId, out );
    }

    public void SaveData(ref GameData progression)
    {
        throw new System.NotImplementedException();
    }//*/
}
