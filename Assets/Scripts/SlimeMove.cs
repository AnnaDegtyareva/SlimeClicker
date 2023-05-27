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

    void OnMouseDown()
    {
        offset = gameObject.transform.position -
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        SlimeGenerator.Instance.money += slimePrice;
        SlimeGenerator.Instance.textUpd();
    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
                }
            }
        }
    }


}
