using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour
{
    //맵 관리
    public MapManage mapManage;
    //포탈 번호
    public int portalNum;
    public int nextNum;
    //포탈 맵 이동 플래그
    public MapType nextMapType;

    //플레이어 태그
    public string playerString = "Player";

    //플레이어
    public GameObject playerObj;

    private void Awake()
    {
        playerObj = GameObject.Find("Player");
        mapManage = GameObject.Find("Map").GetComponent<MapManage>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(playerString))
        {
            playerObj.gameObject.transform.position = mapManage.portalList[nextNum].transform.position+0.5f*Vector3.up+2*Vector3.forward;

            //어느 맵에 입장했는가?
            mapManage.mapType = nextMapType;
            mapManage.BridgeModeManager(mapManage.mapType);
            SoundManager._sound.MapBGMSetting(mapManage.mapType);
        }
    }
}
