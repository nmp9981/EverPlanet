using TMPro;
using UnityEngine;

public class PlayerStatUI : MonoBehaviour
{
    //주요 UI
    [Header("주요 UI")]
    [SerializeField] TextMeshProUGUI playerJobText;
    [SerializeField] TextMeshProUGUI playerLvText;
    [SerializeField] TextMeshProUGUI playerHPText;
    [SerializeField] TextMeshProUGUI playerMPText;
    [SerializeField] TextMeshProUGUI playerEXPText;

    [Header("AP UI")]
    [SerializeField] TextMeshProUGUI playerSTRText;
    [SerializeField] TextMeshProUGUI playerDEXText;
    [SerializeField] TextMeshProUGUI playerINTText;
    [SerializeField] TextMeshProUGUI playerLUKText;

    //상세 UI
    [Header("상세 UI")]
    [SerializeField] TextMeshProUGUI playerAttackText;
    [SerializeField] TextMeshProUGUI playerPhyDEFText;
    [SerializeField] TextMeshProUGUI playerMagicText;
    [SerializeField] TextMeshProUGUI playerMagicDEFext;
    [SerializeField] TextMeshProUGUI playerAccText;
    [SerializeField] TextMeshProUGUI playerAvoidText;
    [SerializeField] TextMeshProUGUI playerHandText;
    [SerializeField] TextMeshProUGUI playerMoveText;
    [SerializeField] TextMeshProUGUI playerJumpText;

    /// <summary>
    /// 플레이어 스탯 정보 업데이트
    /// </summary>
    public void UpdatePlayerStat()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            playerJobText.text = $"Knight";
            playerLvText.text = $"Lv. {PlayerInfo.playerLv}";
            playerHPText.text = $"HP : {PlayerInfo.curHP} / {PlayerInfo.maxHP}";
            playerMPText.text = $"MP : {PlayerInfo.curMP} / {PlayerInfo.maxMP}";

            int expRate = (int)(PlayerInfo.curExp / PlayerInfo.maxExp * 100f);
            playerEXPText.text = $"{PlayerInfo.curExp} {expRate}%";

            playerSTRText.text = $"STR : {PlayerInfo.playerSTR}";
            playerDEXText.text = $"DEX : {PlayerInfo.playerDEX}";
            playerINTText.text = $"INT : {PlayerInfo.playerINT}";
            playerLUKText.text = $"LUK : {PlayerInfo.playerLUK}";

            playerAttackText.text = $"공격력 : {PlayerInfo.attackPower}";
            playerPhyDEFText.text = $"물리방어력 : {PlayerInfo.phyDEF}";
            playerMagicText.text = $"마법공격력 : {PlayerInfo.magicPower}";
            playerMagicDEFext.text = $"마법방어력 : {PlayerInfo.magicDEF}";
            playerAccText.text = $"명중률 : {PlayerInfo.accuracy}";
            playerAvoidText.text = $"회피율 : {PlayerInfo.avoidance}";
            playerHandText.text = $"손재주 : {PlayerInfo.handSpeed}";
            playerMoveText.text = $"이동속도 : {PlayerInfo.moveSpeed}";
            playerJumpText.text = $"점프력 : {PlayerInfo.jumpPower}";
        }
    }
}
