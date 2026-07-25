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

public class MonsterMove : MonsterInfo
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
    //몬스터 스폰 지점
    public Vector3 spawnPosition;
    private float diameter;

    //몬스터 크기
    public Bounds mobSize;

    //방향 전환 타임
    private float curLineTime = 0;
    private float maxLineTime = 3;
    private float moveDir = 1;

    //각도 이동
    private float prevAngle = 0;
    private float nextAngle = 0;

    [SerializeField]
    public bool isAggro;//어그로 여부
    [SerializeField]
    public GameObject target;//타겟
    public float speed = 2f;
    public float chaseRange = 10f; // 추적을 시작할 거리


    private void Awake()
    {
        isAggro = false;
        prevAngle = 0;
        nextAngle = Time.deltaTime;
        target = GameObject.Find("Player");
    }
    private void OnEnable()
    {
        InitHP_UISet();
        mobSize = target.GetComponent<BoxCollider>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.gameObject.transform.position);
        MonsterMoving();
        PlayerChaser(distanceToPlayer);
        MoveMosterUI(distanceToPlayer);
        TimeFlow();
    }

    /// <summary>
    /// 몬스터 이동
    /// </summary>
    void MonsterMoving()
    {
        if (isAggro) return;

        //각 유형별
        switch (moveType)
        {
            case MonsterMoveType.Stop:
                break;
            case MonsterMoveType.Horizontal:
                if (curLineTime > maxLineTime)
                {
                    moveDir *= -1;
                    curLineTime = 0;
                }else this.gameObject.transform.position += Vector3.left * Time.deltaTime * speed * moveDir;
                break;
            case MonsterMoveType.Vertical:
                if (curLineTime > maxLineTime)
                {
                    moveDir *= -1;
                    curLineTime = 0;
                }
                else this.gameObject.transform.position += Vector3.forward * Time.deltaTime * speed * moveDir;
                break;
            case MonsterMoveType.Circle:
                MoveCircleOrbit();
                break;
            default:
                break;
        }
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
        mobInfo.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, mobSize.size.y*0.5f+1, 0));

        //사거리 내
        if (dist < chaseRange)
        {
            // 몬스터 정보 보이게
            mobInfo.text = $"[{MobLv}] {MobName}";
        }
        else
        {
            mobInfo.text = string.Empty;
        }
    }

    /// <summary>
    /// 반지름 설정
    /// </summary>
    public void SetDiameter()
    {
        diameter = (this.gameObject.transform.position - spawnPosition).magnitude;
    }

    /// <summary>
    /// 원궤도 이동
    /// </summary>
    void MoveCircleOrbit()
    {
        float nextXPos = diameter*(Mathf.Cos(nextAngle)-Mathf.Cos(prevAngle));
        float nextZPos = diameter * (Mathf.Sin(nextAngle) - Mathf.Sin(prevAngle));
        this.gameObject.transform.position += new Vector3(nextXPos, 0, nextZPos);
    }

    /// <summary>
    /// 시간 흐름
    /// </summary>
    void TimeFlow()
    {
        curLineTime += Time.deltaTime;
        prevAngle = nextAngle;
        nextAngle += Time.deltaTime;
    }

    /// <summary>
    /// 피격
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            long damageValue = Random.Range(mobAttack*90/100, mobAttack*110/100);
            other.GetComponent<PlayerInfoUpdate>().DecreasePlayerHP((int)damageValue);
            PlayerAttackCommon.ShowDamageAsSkin(damageValue,other.gameObject); 
        }
    }
}
