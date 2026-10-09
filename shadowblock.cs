using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public Sprite otherSprite; // รูปบล็อกอื่น

    private SpriteRenderer sr;
    private Collider2D col;
    private Sprite originalSprite;
    private bool isOther = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) sr = GetComponentInChildren<SpriteRenderer>();

        col = GetComponent<Collider2D>();
        if (col == null) col = GetComponentInChildren<Collider2D>();

        if (sr == null)
        {
            Debug.LogWarning("JumpBlock: ไม่เจอ SpriteRenderer");
            return;
        }
        if (col == null) Debug.LogWarning("JumpBlock: ไม่เจอ Collider2D");

        originalSprite = sr.sprite;
    }

    // สลับสถานะทุกครั้งที่ตัวละครกระโดด
    public void Toggle()
    {
        if (sr == null) return;

        isOther = !isOther;
        sr.sprite = isOther ? otherSprite : originalSprite;
        if (col != null) col.enabled = !isOther; // เป็นรูปอื่น = เหยียบไม่ได้
    }
}