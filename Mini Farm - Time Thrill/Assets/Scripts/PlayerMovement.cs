using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    public Joystick joystick;

    private Vector2 movement;

    void Update()
    {
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (movement.x > 0)
        {
            animator.SetBool("right", true);
            animator.SetBool("left", false);
            animator.SetBool("back", false);
            animator.SetBool("front", false);
        }
        if (movement.x < 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", true);
            animator.SetBool("back", false);
            animator.SetBool("front", false);
        }
        if (movement.y > 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("back", true);
            animator.SetBool("front", false);
        }
        if (movement.y < 0)
        {
            animator.SetBool("right", false);
            animator.SetBool("left", false);
            animator.SetBool("back", false);
            animator.SetBool("front", true);
        }

    }

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

        transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, moveSpeed * Time.deltaTime);

    }
}
