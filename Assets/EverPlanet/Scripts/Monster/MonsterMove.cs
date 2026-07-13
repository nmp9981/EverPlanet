using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMove : MonoBehaviour
{
    [SerializeField]
    public Image HPBar;
    [SerializeField]
    public GameObject HPBarBack;
    [SerializeField]
    public TextMeshProUGUI mobInfo;

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

    // Update is called once per frame
    void Update()
    {
        MonsterMoving();
        PlayerChaser();
        MoveMosterUI();
    }

    /// <summary>
    /// 몬스터 이동
    /// </summary>
    void MonsterMoving()
    {
        if (isAggro) return;

    }

    /// <summary>
    /// 플레이어 추적
    /// </summary>
    void PlayerChaser()
    {
        if (isAggro)
        {
            // 몬스터와 플레이어 사이의 거리 계산
            float distanceToPlayer = Vector3.Distance(transform.position, target.gameObject.transform.position);

            //사거리 내
            if (distanceToPlayer < chaseRange)
            {
                // 플레이어 방향으로 이동
                transform.position = Vector3.MoveTowards(transform.position, target.gameObject.transform.position, speed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 몬스터 UI 이동
    /// </summary>
    void MoveMosterUI()
    {
        //HPBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        //HPBarBack.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        mobInfo.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.25f, 0));

        mobInfo.text = "[" + 80 + "] Mushroom";
    }
}
