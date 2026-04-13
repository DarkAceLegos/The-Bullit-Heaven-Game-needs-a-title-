using UnityEngine;

public class PickUpRange : MonoBehaviour
{
    private void Start()
    {
        //GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.transform.TryGetComponent(out ExpPickUp expPickUp)) { return; }

        //expPickUp.transform.
    }
}
