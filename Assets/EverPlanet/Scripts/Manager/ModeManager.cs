using TMPro;
using UnityEngine;

public class ModeManager : MonoBehaviour
{
    float curTime = 0f;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] MapManage mapManage;

    private void Update()
    {
        curTime -= Time.deltaTime;   
        if(curTime < 0f) UnShowTimeUI();
        UpdateRestTime();
    }

    /// <summary>
    /// 시간 UI 활성화
    /// </summary>
    public void ShowTimeUI()
    {
        timeText.transform.parent.gameObject.SetActive(true);
        curTime = 100f;
    }

    /// <summary>
    /// 남은 시간 보이기
    /// </summary>
    public void UpdateRestTime()
    {
        timeText.text = curTime.ToString("F0");
    }

    /// <summary>
    /// 시간 UI 비활성화
    /// </summary>
    public void UnShowTimeUI()
    {
        timeText.transform.parent.gameObject.SetActive(false);
        curTime = 0f;
        mapManage.OnPortal(mapManage.portalList[5]);
    }
}
