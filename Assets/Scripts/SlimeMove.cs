using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMove : MonoBehaviour
{
    private Vector3 offset;
    public Rigidbody2D rb;

    [SerializeField] public int slimePrice;
    [SerializeField] public int slimeType;

    void OnMouseDown()
    {
        offset = gameObject.transform.position -
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        Game.instance.money += slimePrice;
        Game.instance.textUpd();
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
            if(collision.gameObject.GetComponent<SlimeMove>().slimeType == slimeType)
            {
                //create new slime
                //money++

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }

    public void AnimationFlyingCoins()
    {

    }

}
