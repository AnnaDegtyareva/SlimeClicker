using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSlimeMove : MonoBehaviour
{
    private Vector3 offset;
    public Rigidbody2D rb;
    Vector3 TargetPos;
    public int speed;
    public float TimeBetween;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TimeBetween = Random.Range(.5f, 3f);
        InvokeRepeating("ChangeTargetPos", 0.2f, TimeBetween);
    }

    public void ChangeTargetPos()
    {
        if (TargetPos.x <= 10.5f && TargetPos.y <= 5.5f)
        {
            TargetPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-1.5f, 1.5f), 0);
        }
        else
        {
            TargetPos = new Vector3(transform.position.x - Random.Range(-1.5f, 1.5f), transform.position.y - Random.Range(-1.5f, 1.5f), 0);
        }

        TimeBetween = Random.Range(.5f, 3f);
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Slime")
        {
            ChangeTargetPos();
        }
    }
}
