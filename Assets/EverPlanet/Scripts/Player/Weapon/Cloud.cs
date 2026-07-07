using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    private float timer;
    public float maxTimer = 1;
    public float maxCount = 9;

    private void OnEnable()
    {
        List<GameObject> targets = PlayerAttackCommon.TargetMonstersInRange(this.gameObject.transform.position, 3,2,15);
        foreach (GameObject target in targets)
        {
            var mob = target.GetComponent<BoxCollider>();
            PlayerAttackCommon.PlayerToMonsterAttack(mob, 800, 2);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        //∏ÛΩ∫≈Õ
        if (other.gameObject.tag.Contains("Monster"))
        {
            StartCoroutine(PlayerAttackCommon.DoteAttack(other, maxTimer, 1, 50));
        }
    }
}
