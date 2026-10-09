using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item_controll : MonoBehaviour
{
    [Header("UI Settings")]
    public Text scoreText;

    private bool isCollected = false; // ป้องกันการเก็บซ้ำ

    void Start()
    {
        Collider2D c = GetComponent<Collider2D>();

        if (c == null)
        {
            c = gameObject.AddComponent<BoxCollider2D>();
        }

        c.isTrigger = true;

        if (scoreText == null)
        {
            scoreText = GameObject.FindObjectOfType<Text>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // เช็คว่าเป็น Player และยังไม่เคยถูกเก็บมาก่อน
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // ล็อคไว้ไม่ให้ทำงานซ้ำ

            // 1. เพิ่มคะแนน
            GameManager.nScore++;

            // 2. อัปเดต UI
            if (scoreText != null)
            {
                scoreText.text = GameManager.nScore.ToString();
            }

            // 3. เล่นเสียงเก็บไอเทม
            AudioSource audio = GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.Play();
            }

            // 4. ซ่อนภาพไอเทมและปิด Collider ทันที (เพื่อให้ดูเหมือนไอเทมหายไปแล้ว)
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            if (sprite != null) sprite.enabled = false;

            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null) collider.enabled = false;

            // 5. รอ 0.5 วินาทีเพื่อให้เสียงเล่นจบ แล้วค่อยทำลาย GameObject
            Destroy(gameObject, 0.5f);
        }
    }
}