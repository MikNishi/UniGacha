using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator am;
    private Movement pm;
    private SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleMovementAnimation();
        HandleSpriteDirection();
    }

    void HandleMovementAnimation()
    {
        bool isMoving = pm.moveDir.x != 0 || pm.moveDir.y != 0;
        am.SetBool("Move", isMoving);
    }

    void HandleSpriteDirection()
    {
        if (pm.moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else if (pm.moveDir.x > 0)
        {
            sr.flipX = false;
        }
        // если x == 0 Ч не мен€ем направление
    }
}
