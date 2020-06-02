using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//場景管理API

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
    private float gameTime = 30;

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

        //遊戲時間 = 數學.夾住(遊戲時間,最小值,最大值)
        gameTime = Mathf.Clamp(gameTime, 0, 100);

        textTime.text = "倒數時間:" + gameTime.ToString("f2");

        Lose();

    }

    public void GetProp(string prop)
    {
        if (prop == "SilverCoin")
        {
            countProp++;
            textCount.text = "道具數量：" + countProp + "/" + countTotal;

            Win();
        }
        else if (prop == "Bomb")
        {
            gameTime -= 2;
            textTime.text = "倒數時間：" + gameTime.ToString("f2");
        }
    }

    private void Win()
    {
        if (countProp == countTotal)
        {
            //顯示結束畫面，啟動互動、啟動遮擋
            final.alpha = 1;
            final.interactable = true;
            final.blocksRaycasts = true;
            textTitle.text = "You Win";
            FindObjectOfType<Player>().enabled = false;
        }
    }

    private void Lose()
    {
        if (gameTime == 0)
        {
            final.alpha = 1;
            final.interactable = true;
            final.blocksRaycasts = true;
            textTitle.text = "You Lose";
            //取得玩家.啟動 = false
            FindObjectOfType<Player>().enabled = false;
        }
    }

    public void Replay()
    {
        SceneManager.LoadScene("GAME");
    }

    public void Quit()
    {
        Application.Quit();
    }

    #endregion

    #region 事件

    private void Start()
    {
        //道具總數 = 生成道具(道具一號,指定數量)
        countTotal = CreateProp(props[0], 10);

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
