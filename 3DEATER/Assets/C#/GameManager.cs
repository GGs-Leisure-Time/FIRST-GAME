using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("道具")]
    public GameObject[] props;
    [Header("道具總數")]
    public Text textCount;
    [Header("倒數時間")]
    public Text textTime;
    [Header("結束畫面標題")]
    public Text textTitle;
    [Header("結束畫面")]
    public CanvasGroup final;

    /// <summary>
    /// 道具總數
    /// </summary>
    private int countTotal;


    /// <summary>
    /// 取得道具數量
    /// </summary>
    private int countProp;
    /// <summary>
    /// 遊戲時間
    /// </summary>
    private float gameTime = 10;

    #region 方法

    private int CreateProp(GameObject prop, int count)
    {
        int total = count + Random.Range(-3, 3);
        //for迴圈
        for (int i = 0; i < total; i++)
        {
            //座標 = (隨機,1.5,隨機)
            Vector3 pos = new Vector3(Random.Range(-9, 9), 1.0f, Random.Range(-9, 9));
            //生成(物件,座標,角度)
            Instantiate(prop, pos, Quaternion.Euler(90,0,0));
        }

        return total;
    }

    private void CountTime()
    {
        //遊戲時間 遞減
        gameTime -= Time.deltaTime;
        textTime.text = "倒數時間:" + gameTime.ToString("f2");

    }

    #endregion

    #region 事件

    private void Start()
    {
        CreateProp(props[0], 20);
        textCount.text = "道具數量 0 /" + countTotal;

        CreateProp(props[1], 8);
    }

    private void Update()
    {
        CountTime();
    }
    #endregion

}
