using TMPro;
using UnityEngine;

public class PlayerXPUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI xpText;

    float curTime = 0;
    float destroyTime = 3;
    
    void Update()
    {
        this.gameObject.transform.position += Time.deltaTime * Vector3.up*80;

        OffXPUI();
        curTime += Time.deltaTime;
    }

    /// <summary>
    /// 경험치 설정
    /// </summary>
    public void SetXP(int xp)
    {
        GameObject player = GameObject.Find("Player");
        this.gameObject.transform.position = Camera.main.WorldToScreenPoint(player.transform.position)+Vector3.up*100;
        xpText.text = $"XP {xp}";
        curTime = 0;
    }

    /// <summary>
    /// XP UI 끄기
    /// </summary>
    void OffXPUI()
    {
        if(curTime > destroyTime) this.gameObject.SetActive(false);
    }
}
