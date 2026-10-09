using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    float moveSpeed = 4f;
    bool moveRight = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 1. ถ้าเดินขวาไปจนเกิน 33.27f (จุดสูงสุดทางขวา) ให้สลับไปเดินซ้าย
        if (transform.position.x > 33.27f)
            moveRight = false;

        // 2. ถ้าเดินซ้ายไปจนต่ำกว่า 22.54226f (จุดต่ำสุดทางซ้าย) ให้สลับไปเดินขวา
        if (transform.position.x < 22.54226f)
            moveRight = true;

        // 3. สั่งเคลื่อนที่ตามทิศทาง
        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
    }
}