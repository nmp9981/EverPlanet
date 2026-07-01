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
       
    }

    /// <summary>
    /// 휘두르기 공격
    /// </summary>
    public IEnumerator SwingAttack()
    {
        GameObject swardObj = objFulling.MakeObj(1);
        swardObj.transform.position = playerDirObjectTransform.position;//캐릭터 위치에서 날리기 시작

        float curRotate = 15;
        float maxRotate = 165;
        while (curRotate < maxRotate)
        {
            swardObj.transform.rotation = Quaternion.Euler(0, curRotate, 0);
            curRotate += 15;
            yield return new WaitForSeconds(0.05f);
        }
        swardObj.gameObject.SetActive(false);
    }

}
