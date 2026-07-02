using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] ObjectFulling objFulling;
    [SerializeField] Transform playerDirObjectTransform;

    /// <summary>
    /// 일반 공격
    /// </summary>
    public void GeneralAttack()
    {
        GameObject projecTile = objFulling.MakeObj(0);
        projecTile.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        projecTile.GetComponent<Projectile>().hitNum = 2;
    }
    /// <summary>
    /// 던지기 공격
    /// </summary>
    public IEnumerator ThrowAttack()
    {
        for(int i = 0; i < 6; i++)
        {
            GameObject projecTile = objFulling.MakeObj(2);
            projecTile.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
            projecTile.GetComponent<Dagger>().hitNum = i + 2;
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 휘두르기 공격
    /// </summary>
    public IEnumerator SwingAttack()
    {
        GameObject swardObj = objFulling.MakeObj(1);
        Sward sward = swardObj.GetComponent<Sward>();
        sward.targetCount = 0;
        sward.maxTarget = 6;
        swardObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작
        swardObj.transform.rotation = transform.rotation;//캐릭터가 바라보는 위치로 회전

        float angleRotate = 15;
        float restRotate = 165;
        while (restRotate>0)
        {
            swardObj.transform.Rotate(0, angleRotate, 0);
            restRotate -= angleRotate;
            yield return new WaitForSeconds(0.05f);
        }
        swardObj.gameObject.SetActive(false);
    }

}
