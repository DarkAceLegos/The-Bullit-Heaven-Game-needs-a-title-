using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public float moveSpeed;

    private void FixedUpdate()
    {
        move();//
    }

    private void move()
    {
        Vector2 playerVelocity = GameInputs.Instance.GetMovmentVectorNormilzed(); //moveAction.ReadValue<Vector2>();//

        rb.linearVelocity = new Vector2(playerVelocity.x * moveSpeed, playerVelocity.y * moveSpeed);//
    }
}
