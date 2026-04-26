using UnityEngine;
using UnityEngine.UI;

public class PartyPlayerHilightColor : MonoBehaviour
{
    [SerializeField] Image image;

    private Material material;

    private void Awake()
    {
        material = new Material(image.material);
        image.material = material;
    }

    public void SetPlayerColor(Color color)
    {
        material.color = color;
    }
}
