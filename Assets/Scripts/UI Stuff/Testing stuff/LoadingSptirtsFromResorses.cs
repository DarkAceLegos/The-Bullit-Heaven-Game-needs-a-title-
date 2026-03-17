using UnityEngine;
using UnityEngine.UI;

public class LoadingSptirtsFromResorses : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Image;

    private void Awake()
    {
        Image.sprite = Resources.Load<Sprite>("Sprites/CardsBase");
    }
}
