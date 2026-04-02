using UnityEngine;

public class HurtSprite : MonoBehaviour
{
    [SerializeField] public float time;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(time < 0)
        {
            //time = 0;
            this.gameObject.SetActive(false);
        }

        time -= Time.deltaTime;
    }
}
