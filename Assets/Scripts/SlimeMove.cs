using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class SlimeMove : MonoBehaviour
{
    private Vector3 offset;
    public Rigidbody2D rb;

    [SerializeField] public int slimePrice;
    [SerializeField] public int slimeType;

    public int index;
    Vector3 TargetPos;

    public int speed;

    public float TimeBetween;

    void OnMouseDown()
    {
        offset = gameObject.transform.position -
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        SlimeGenerator.Instance.money += slimePrice;
        SlimeGenerator.Instance.textUpd();

        SlimeGenerator.Instance.CreateText(slimePrice, new Vector3(Input.mousePosition.x - 960, Input.mousePosition.y - 540, 10.0f));
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        if(offset.x <= 10.5f && offset.y <= 6.5f)
        {
            transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
        }    
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChangeTargetPos", 0.2f, TimeBetween);
    }

    public void ChangeTargetPos()
    {
        TargetPos = new Vector3(transform.position.x + Random.Range(-1.5f, 1.5f), transform.position.y + Random.Range(-1.5f, 1.5f), 0);
        TimeBetween = Random.Range(3f, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Slime")
        {
            SlimeMove sm = collision.gameObject.GetComponent<SlimeMove>();
            if (sm.slimeType == slimeType)
            {             
                
                if(index > sm.index)
                {
                    if (SlimeGenerator.Instance.newSlime(slimeType, transform.position, collision.transform.position))
                    {
                        Destroy(gameObject);
                        Destroy(collision.gameObject);
                    }
                    else
                    {
                        ChangeTargetPos();
                    }
                }
            }
            else
            {
                ChangeTargetPos();
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DarkHole")
        {
            YandexGame.savesData.allSlimes[slimeType]--;
            YandexGame.SaveProgress();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
    }


}
