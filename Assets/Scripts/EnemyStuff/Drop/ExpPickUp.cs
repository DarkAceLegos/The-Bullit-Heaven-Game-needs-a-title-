using UnityEngine;

public class ExpPickUp : MonoBehaviour
{
    //[SerializeField] private GameObject thisPrefab;
    [SerializeField] private int amountOfExp;

    public void SpawnExp(int value, Vector3 pos)
    {
        amountOfExp = value;

        ObjectPooler.SpawnObject(gameObject, pos, Quaternion.identity);
    }

    private void Pickup()
    {
        ObjectPooler.ReturnObjectToPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.TryGetComponent(out PlayerHealth playerHealth) || !playerHealth.IsOwner)
        { return; }

        LevelManager.Instance.AddExpRpc((amountOfExp + Player.LoaclInstance.additiveExperience) * Player.LoaclInstance.percentageExperience);

        Pickup();
    }
}
