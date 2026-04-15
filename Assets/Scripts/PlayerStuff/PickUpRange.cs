using UnityEngine;

public class PickUpRange : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ExpPickUp>(out ExpPickUp expPickUp))
        {
            expPickUp.SetTarget(transform.parent.position);
        }

        if (collision.gameObject.TryGetComponent<CoinPickUp>(out CoinPickUp coinPickUp))
        {
            coinPickUp.SetTarget(transform.parent.position);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ExpPickUp>(out ExpPickUp expPickUp))
        {
            expPickUp.SetTarget(transform.parent.position, 0f);
        }

        if (collision.gameObject.TryGetComponent<CoinPickUp>(out CoinPickUp coinPickUp))
        {
            coinPickUp.SetTarget(transform.parent.position, 0f);
        }
    }
}
