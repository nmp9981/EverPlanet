using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterMoveType
{
    Stop,
    Horizontal,
    Vertical,
    Circle,
    Count
}

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    public Image HPBar;
    [SerializeField]
    public GameObject HPBarBack;
    [SerializeField]
    public TextMeshProUGUI mobInfo;

    //몬스터 이동 유형
    [SerializeField]
    public MonsterMoveType moveType;

    //몬스터 크기
    public Bounds mobSize;

    [SerializeField]
    public bool isAggro;//어그로 여부
    [SerializeField]
    public GameObject target;//타겟
    public float speed = 2f;
    public float chaseRange = 10f; // 추적을 시작할 거리


    private void Awake()
    {
        isAggro = false;
        target = GameObject.Find("Player");
    }
    private void OnEnable()
    {
        mobSize = target.GetComponent<BoxCollider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.gameObject.transform.position);
        MonsterMoving();
        PlayerChaser(distanceToPlayer);
        MoveMosterUI(distanceToPlayer);
    }

    /// <summary>
    /// 몬스터 이동
    /// </summary>
    void MonsterMoving()
    {
        if (isAggro) return;

        //각 유형별 이동
    }

    /// <summary>
    /// 플레이어 추적
    /// </summary>
    void PlayerChaser(float dist)
    {
        if (isAggro)
        {
            //사거리 내
            if (dist < chaseRange)
            {
                // 플레이어 방향으로 이동
                transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, speed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 몬스터 UI 이동
    /// </summary>
    void MoveMosterUI(float dist)
    {
        //HPBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        //HPBarBack.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        mobInfo.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, mobSize.size.y*0.5f+1, 0));

        //사거리 내
        if (dist < chaseRange)
        {
            // 몬스터 정보 보이게
            mobInfo.text = "[" + 80 + "] Mushroom";
        }
        else
        {
            mobInfo.text = string.Empty;
        }
    }
}
