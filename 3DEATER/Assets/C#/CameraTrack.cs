using UnityEngine;

public class CameraTrack : MonoBehaviour
{
    #region 欄位與屬性
    /// <summary>
    /// 玩家變形元件
    /// </summary>
    private Transform player;
    [Header("追蹤速度"), Range(0.1f, 50.5f)]
    public float speed = 1.5f;
    #endregion

# region 方法
    /// <summary>
    /// 追蹤玩家
    /// </summary>
    private void Track()
    {
        //攝影機與小明y軸距離 5.05-0.05 = 5
        //攝影機與小明z軸距離 -4-0 = -4
        Vector3 posTrack = player.position;
        posTrack.y += 5f;
        posTrack.z += -4f;

        //攝影機座標 = 變形.座標
        Vector3 posCam = transform.position;
        //攝影機座標 = 三維向量(Animation,Behaviour,百分比)
        posCam = Vector3.Lerp(posCam, posTrack, 0.5f * Time.deltaTime * speed);
        //變形.座標 = 攝影機座標
        transform.position = posCam;
    }

    #endregion

    #region
    private void Start()
    {
        player = GameObject.Find("MAN").transform;
    }
    #endregion

    private void LateUpdate()
    {
        Track();
    }

}
