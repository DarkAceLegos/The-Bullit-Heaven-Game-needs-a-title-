using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Icollectible>(out Icollectible collectible))
        {
            collectible.Collect();
        }
    }
}
