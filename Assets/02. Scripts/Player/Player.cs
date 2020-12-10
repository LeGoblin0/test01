using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("폭탄")]
    [Tooltip("폭탄 오브젝트")]
    public GameObject Boom;
    [Tooltip("폭탄 터질때 나오는 잔상")]
    public GameObject BoomDie;
    [Tooltip("폭탄 던지는 스피드")]
    public float BoomSpeed = 10;
    [Tooltip("폭탄 터지는 파워")]
    public float BoomPower = 10;


    [Header("사람")]
    [Tooltip("넘어졌을떄 다시 일어날 속도")]
    public float WakeUpPower=10;
    [Tooltip("가만히 2초간 있는가?")]
    public float WakeupDelayTime = 2;
    [Tooltip("몇초동안 일어날 것인가.")]
    public float WakeupTime = 3;
    [Tooltip("일어나는 동안은 움직이기 가능")]
    float WakeupNowTime = -100;

    public PlayerFoot plf;

    Rigidbody2D rig;
    //GameObject Boom;//폭탄 생성하면 들어갈 변수
    Camera cam;
    Vector2 MousePosition;
    private void Start()
    {
        cam = Camera.main;
        rig = GetComponent<Rigidbody2D>();
    }
    public void End()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && plf.PutGround())//클릭=폭탄 터지고 던지고 
        {
            if (Boom.activeSelf==false)
            {
                MousePosition = Input.mousePosition;
                MousePosition = cam.ScreenToWorldPoint(MousePosition);
                //Boom.transform.position = new Vector3(transform.position.x, transform.position.y, -1);
           
                Boom.transform.position = transform.position+ (new Vector3(MousePosition.x, MousePosition.y, transform.position.z) - transform.position).normalized * 2;
                Boom.transform.position = new Vector3(Boom.transform.position.x, Boom.transform.position.y, -1);
                //Boom.GetComponent<Rigidbody2D>().velocity = (new Vector3(MousePosition.x, MousePosition.y, transform.position.z) - transform.position).normalized * BoomSpeed;
                Boom.SetActive(true);
                Invoke("BoomOFFF", 1);
            }
            //else
            //{
            //    rig.velocity = (transform.position - Boom.transform.position).normalized / (((transform.position - Boom.transform.position).sqrMagnitude < 1) ? 1 : (transform.position - Boom.transform.position).sqrMagnitude) * BoomPower;
            //    GameObject BoomDies = Instantiate(BoomDie);
            //    Destroy(BoomDies, 0.3f);
            //    BoomDies.transform.position = new Vector3(Boom.transform.position.x, Boom.transform.position.y, -1);
            //    Destroy(Boom.gameObject);
            //}
        }
    }
    void BoomOFFF()
    {
        Boom.SetActive(false);
        GameObject BoomDies = Instantiate(BoomDie);
        Destroy(BoomDies, 0.3f);
        rig.velocity = (transform.position - Boom.transform.position).normalized * BoomPower;
        BoomDies.transform.position = new Vector3(Boom.transform.position.x, Boom.transform.position.y, -1);
    }
    private void FixedUpdate()
    {
        if (rig.velocity == Vector2.zero && (transform.eulerAngles.z + 720 % 360) > 10 && (transform.eulerAngles.z + 720 % 360) < 350)//가만힌 2초간 정지해 있으면 일어남
        {
            WakeupDelayTime -= Time.deltaTime;
        }
        else
        {
            WakeupDelayTime = 2;
        }

        if (WakeupDelayTime < 0)
        {
            WakeupNowTime = WakeupTime;
        }

        if (WakeupNowTime > -99)
        {
            WakeupNowTime -= Time.deltaTime;
            if (transform.eulerAngles.z + 720 % 360 < 180)
            {
                rig.angularVelocity = WakeUpPower;
            }
            else
            {
                rig.angularVelocity = -WakeUpPower;
            }
            if (WakeupNowTime < 0 || ((transform.eulerAngles.z + 720 % 360) < 1 || (transform.eulerAngles.z + 720 % 360) > 359)) 
            {
                WakeupNowTime = -100;
                rig.angularVelocity = 0;
                rig.velocity = Vector2.zero;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

    }


}
