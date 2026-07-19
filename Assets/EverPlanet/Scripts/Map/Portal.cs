using UnityEngine;

public class Portal : MonoBehaviour
{
    //맵 관리
    public MapManage mapManage;
    //포탈 번호
    public int portalNum;
    public int nextNum;
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
        }
    }
}
