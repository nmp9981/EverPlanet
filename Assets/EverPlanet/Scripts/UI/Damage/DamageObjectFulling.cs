using System.Collections.Generic;
using UnityEngine;

public class DamageObjectFulling : MonoBehaviour
{
    public static DamageObjectFulling DamageSkinInstance = null;

    //데미지 이미지
    public List<Sprite> damageImage = new List<Sprite>();
    public List<Sprite> criticalDamageImage = new List<Sprite>();
    public List<Sprite> hitDamageImage = new List<Sprite>();
    public List<Sprite> missDamageImage = new List<Sprite>();

    //프리팹 준비
    const int blockMaxCount = 27;
    const int blockKinds = 32;
    public GameObject[] blockPrefabs;

    //오브젝트 배열
    GameObject[][] blocks;
    GameObject[] targetPool;

    void Awake()
    {
        DamageSkinObjectLoad();

        blocks = new GameObject[blockKinds][]
        {
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount]
        };
        Generate();
    }

    /// <summary>
    /// 싱글톤으로 제작
    /// </summary>
    void DamageSkinObjectLoad()
    {
        if (DamageSkinInstance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            DamageSkinInstance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (DamageSkinInstance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }
    void Generate()
    {
        //블록
        for (int i = 0; i < blockKinds; i++)
        {
            for (int j = 0; j < blockMaxCount; j++)
            {
                blocks[i][j] = Instantiate(blockPrefabs[i]);
                blocks[i][j].SetActive(false);
                blocks[i][j].transform.parent = transform;//자식 오브젝트로 생성
            }
        }
    }
    //오브젝트 생성
    public GameObject MakeObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);//활성화 후 넘김
                return targetPool[i];
            }
        }
        return null;//없으면 빈 객체
    }
    //오브젝트 존재여부 확인
    public bool IsActiveObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (targetPool[i].activeSelf) return true;//활성화된게 있으면
        }
        return false;//없으면 빈 객체
    }
    //오브젝트 존재하면 가져오기
    public GameObject GetObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (targetPool[i].activeSelf) return targetPool[i];//활성화된게 있으면
        }
        return null;//없으면 빈 객체
    }
    //오브젝트 배열 가져오기
    public GameObject[] GetPool(int num)//지정한 오브젝트 풀 가져오기
    {
        targetPool = blocks[num];
        return targetPool;
    }
    //오브젝트들 비활성화
    public void OffObj()
    {
        for (int i = 0; i < blockKinds; i++)
        {
            for (int j = 0; j < blockMaxCount; j++)
            {
                if (blocks[i][j].activeSelf) blocks[i][j].SetActive(false);
            }
        }
    }
}
