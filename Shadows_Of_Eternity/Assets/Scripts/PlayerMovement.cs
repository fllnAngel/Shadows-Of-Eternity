using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public float runSpeed = 10f;
    private float currentSpeed;
    Vector2 movement;

    public Animator animator;

    public Image staminaBar;
    public float stamina = 100f, maxStamina = 100f;
    public float runCost = 10f;
    public bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            isRunning = true;
            currentSpeed = runSpeed;
            stamina -= runCost * Time.deltaTime;
        }
        else
        {
            isRunning = false;
            currentSpeed = speed;
            stamina += runCost * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        staminaBar.fillAmount = stamina / maxStamina;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
