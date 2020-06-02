using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度"),Range(1,1000)]
    public float speed = 10;
    [Header("跳躍高度"),Range(1,5000)]
    public float height;

    /// <summary>
    /// 是否在地板上
    /// </summary>
    private bool isGround
    {
        get 
        {
            if (transform.position.y < 0.051f) return true;
            else return false;
        }           
    }

    private Vector3 direction;
    private Animator ani;
    private Rigidbody rig;
    private AudioSource aud;
    private GameManager gm;
    /// <summary>
    /// 跳躍力道：從0開始增加
    /// </summary>
    private float jump;

    [Header("金幣音效")]
    public AudioClip soundCoin;
    [Header("炸彈音效")]
    public AudioClip soundBomb;

    #region 方法操作
    /// <summary>
    /// 移動：透過鍵盤
    /// </summary>
    private void Move()
    {
        #region 移動
        //浮點數 前後值 = 輸入類別.取得軸向("垂直") 垂直WS上下
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        //剛體, 添加推力(x,y,z) - 世界座標
        //rig.AddForce(0, 0, speed*v);
        //剛體.添加推力(三維向量) - 區域座標
        /*前方 transform.forward - z
          右方 transform.right   - x
          上方 transform.up      - y */

        rig.AddForce(transform.forward*speed*Mathf.Abs(v));
        rig.AddForce(transform.forward*speed*Mathf.Abs(h));

        //動畫設定布林值(跑步參數,布林值)
        ani.SetBool("跑步開關", Mathf.Abs(v) > 0|| Mathf.Abs(h) > 0);
        #endregion

        #region 轉向
 
        if (v == 1) direction = new Vector3(0, 0, 0);
        if (v == -1) direction = new Vector3(0, 180, 0);
        if (h == 1) direction = new Vector3(0, 90, 0);
        if (h == -1) direction = new Vector3(0, 270, 0);

        transform.eulerAngles = direction;

        #endregion

    }
    /// <summary>
    /// 跳躍：判斷在地板上並按下空白鍵時跳躍
    /// </summary>
    private void Jump() 
    {
        if (isGround && Input.GetButtonDown("Jump"))
        {
            //每次跳躍都從0開始
            jump = 0;
            //剛體.推力(0,跳躍高度,0)
            rig.AddForce(0, height, 0);
        }
        if (!isGround)
        {
            jump += Time.deltaTime;
        }
        //動畫.設定浮點數("跳躍參數",跳躍時間)
        ani.SetFloat("跳躍", jump);
    }

    /// <summary>
    /// 碰到道具：碰到帶有標籤的物件
    /// </summary>
    private void HitProp(GameObject prop) 
    {
        if (prop.tag == "SilverCoin")
        {
            aud.PlayOneShot(soundCoin, 2);
            Destroy(prop);
        }
        else if (prop.tag == "Bomb")
        {
            aud.PlayOneShot(soundBomb, 2);
            Destroy(prop);
        }

        //告知GM取得道具
        gm.GetProp(prop.tag);
    }
    #endregion

    #region 事件
    private void Start()
    {
        //GetComponent<泛型>() 泛型方法 泛型 所有類型 Rigidbody,Transform,Collider
        //剛體 = 取得元件<剛體>();
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        //FOOT 僅限於場景上只有一個類別存在時使用
        //例如：場上只有一個GameManager時可以使用他取得
        gm = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    private void Update()
    {
        Jump();        
    }

    //碰撞事件：當物件碰撞開始時執行一次(沒有勾選 is trigger)
    //Collision 碰到物件的物碰撞資訊 is trigger 是否穿透
    private void OnCollisionEnter(Collision collision)
    {

    }
    //碰撞事件：當物件碰撞離開時執行一次(沒有勾選 is trigger)
    private void OnCollisionExit(Collision collision)
    {

    }
    //碰撞事件：當物件碰撞開始時持續執行(沒有勾選 is trigger)
    private void OnCollisionStay(Collision collision)
    {

    }

    //觸發事件：當物件碰撞開始時執行一次(有勾選is trigger)
    private void OnTriggerEnter(Collider other)
    {
        //碰到道具(碰撞資訊.遊戲物件)
        HitProp(other.gameObject);
    }
    //觸發事件：當物件碰撞離開時執行一次(有勾選is trigger)
    private void OnTriggerExit(Collider other)
    {

    }
    //觸發事件：當物件碰撞開始時持續執行(有勾選is trigger)
    private void OnTriggerStay(Collider other)
    {

    }
}
