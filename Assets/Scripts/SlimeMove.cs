using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
public class SlimeMove : MonoBehaviour
{
    private Vector3 offset;
    public Rigidbody2D rb;

    [SerializeField] public int slimePrice;
    [SerializeField] public int slimeMoney;
    [SerializeField] public int slimeType;

    [SerializeField] public string slimeNameRu;
    [SerializeField] public string slimeNameEn;

    public int index;
    Vector3 TargetPos;

    public int speed;

    public float TimeBetween;

    public float cooldownTime = 1.5f;
    public bool canDoAction;

   
    private IEnumerator ICooldown()
    {
        canDoAction = false;
        yield return new WaitForSeconds(cooldownTime);
        canDoAction = true;
    }

    void OnMouseDown()
    {
        offset = gameObject.transform.position -
        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        if (canDoAction)
        {
            SlimeGenerator.Instance.CreateText(slimeMoney, Input.mousePosition);
            StartCoroutine(ICooldown());
        }

    }

    void OnMouseDrag()
    {
        Vector3 newPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;
          
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TimeBetween = Random.Range(.5f, 3f);
        InvokeRepeating("ChangeTargetPos", 0.2f, TimeBetween);

        StartCoroutine(ICooldown());
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
    }
        
   

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Slime")
        {

            if (YandexGame.savesData.sounds)
            {
                GameCanvas.instance.slimeAudio.clip = GameCanvas.instance.slimeAudios[2];
                GameCanvas.instance.slimeAudio.Play();
            }
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
        if (collision.gameObject.tag == "Food")
        {
            float count = collision.gameObject.GetComponent<Food>().count;
            transform.localScale = new Vector3(transform.localScale.x + count, transform.localScale.y + count, transform.localScale.z + count);
            Destroy(collision.gameObject);
            
            if(transform.localScale.x >= 3)
            {
                Vector2 pos = new Vector2(transform.position.x+2, transform.position.y+2);
                SlimeGenerator.Instance.CreateSlime(slimeType, pos);
                transform.localScale = new Vector3(1, 1, 1);
            }

            if (YandexGame.savesData.sounds)
            {
                GameCanvas.instance.slimeAudio.clip = GameCanvas.instance.slimeAudios[4];
                GameCanvas.instance.slimeAudio.Play();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DarkHole")
        {
            if (YandexGame.savesData.sounds)
            {
                GameCanvas.instance.slimeAudio.clip = GameCanvas.instance.slimeAudios[3];
                GameCanvas.instance.slimeAudio.Play();
            }
            YandexGame.savesData.allSlimes[slimeType]--;
            YandexGame.SaveProgress();
            Destroy(gameObject);
        }
        
    }

    private void Update()
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x ,GameCanvas.instance.pos1.x, GameCanvas.instance.pos2.x), Mathf.Clamp(transform.position.y, GameCanvas.instance.pos1.y, GameCanvas.instance.pos2.y));

        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetPos, step);
        //if ((transform.position.x >= 11f || transform.position.y >= 5f) || (transform.position.x <= -11f || transform.position.y <= -5f))
        //{
        //    transform.position = new Vector2(1,1);
        //}
    }


}
