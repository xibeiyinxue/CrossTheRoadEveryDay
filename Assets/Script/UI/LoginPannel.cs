using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginPannel : MonoBehaviour
{

    [SerializeField]/*在外界获取 InputField 组件*/
    private InputField inputField = null;
    private PlayerData playerdata;/*获取玩家数据库*/

    [SerializeField]/*下一场景命*/
    private string loadSceneName = null;

    [SerializeField]/*在外界获取提示文本*/
    private Text prompt = null;

    private void Awake()
    {
        playerdata = Resources.Load<PlayerData>("PlayerData");
        UIManager.Instance.FaderOn(false, 1f);
        if (prompt != null)
            prompt.gameObject.SetActive(false);
    }

    public void SetPlayerName()
    {
        //将 InputField 组件内获取的 text 文件赋给玩家数据库的名字
        playerdata.playerName = inputField.text;

        if (playerdata.playerName == "")
        {
            //如果没有输入玩家姓名，提示开启，不进入下一关卡
            prompt.gameObject.SetActive(true);
            return;
        }

        StartCoroutine(Login());
    }

    public IEnumerator Login()
    {
        UIManager.Instance.FaderOn(true, 1f); /*过渡图片开启*/
        yield return new WaitForSeconds(1f);
        LoadSceneManager.LoadScene(loadSceneName);
    }
}
