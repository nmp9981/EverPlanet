using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image expBar;
    [SerializeField] TextMeshProUGUI expText;

    private void Start()
    {
        UpdatePlayerInfo();
    }

    /// <summary>
    /// 플레이어 정보 업데이트
    /// </summary>
    public void UpdatePlayerInfo()
    {
        expBar.fillAmount = (float)PlayerInfo.curExp / PlayerInfo.maxExp;
        expText.text = $"{PlayerInfo.curExp} / {PlayerInfo.maxExp} [{expBar.fillAmount*100f:F1}%]";
    }
}
