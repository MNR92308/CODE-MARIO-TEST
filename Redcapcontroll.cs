using UnityEngine;
using UnityEngine.InputSystem;

public class Idle_Controller : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public JumpBlock[] blocks; // ลากบล็อคได้หลายอันใน Inspector

    private int jumpCount = 0;
    private float x, sx;
    private bool jumpPressed;
    private bool isGrounded;
    private Animator am;
    private Rigidbody2D rb;

    void Start()
    {
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sx = transform.localScale.x;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // อ่านปุ่มเดินซ้าย-ขวา
        x = 0f;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            x = -1f;
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            x = 1f;

        // อ่านปุ่มกระโดด (เก็บไว้ให้ FixedUpdate ใช้)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpCount < maxJumps)
            jumpPressed = true;

        // Animation
        am.SetFloat("SPEED", Mathf.Abs(x));
        am.SetBool("JUMP", !isGrounded);

        // หันหน้าตัวละคร
        if (x > 0)
            transform.localScale = new Vector3(sx, transform.localScale.y, transform.localScale.z);
        else if (x < 0)
            transform.localScale = new Vector3(-sx, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate()
    {
        float yVelocity = rb.linearVelocity.y;

        if (jumpPressed)
        {
            yVelocity = jumpForce;
            jumpCount++;
            isGrounded = false;

            // ทุกครั้งที่กระโดด บล็อกสลับสถานะ (เปลี่ยนรูป/เหยียบไม่ได้ <-> รูปเดิม/เหยียบได้)
            foreach (JumpBlock b in blocks)
                if (b != null) b.Toggle();
        }
        jumpPressed = false;

        rb.linearVelocity = new Vector2(x * speed, yVelocity);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                jumpCount = 0;
                return;
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                if (rb.linearVelocity.y <= 0.01f)
                    jumpCount = 0;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        isGrounded = false;
    }
}