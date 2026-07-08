using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float timer;
    public float maxTimer = 5;
    public Vector3 startPos;
    [SerializeField] List<Collider> mobCollide = new List<Collider>();
    [SerializeField] List<GameObject> targets = new List<GameObject>();

    /// <summary>
    /// √ ±‚»≠
    /// </summary>
    /// <param name="startPos"></param>
    public void Init(Vector3 startPos)
    {
        this.startPos = startPos;

        mobCollide.Clear();
        targets.Clear();

        targets = PlayerAttackCommon.TargetMonstersInRange(startPos, 5,2,15);
        foreach (GameObject target in targets)
        {
            var mob = target.GetComponent<BoxCollider>();
            PlayerAttackCommon.PlayerToMonsterAttack(mob, 900, 2);
            mobCollide.Add(mob);
        }
        DoteAttackInrange();
        timer = 0;
    }

    private void Update()
    {
        if(timer > maxTimer)
        {
            Destroy(this.gameObject);
        }
        timer += Time.deltaTime;
    }

    void DoteAttackInrange()
    {
        foreach (var mob in mobCollide)
        {
            StartCoroutine(PlayerAttackCommon.DoteAttack(mob, maxTimer, 1, 50));
        }
    }
}
