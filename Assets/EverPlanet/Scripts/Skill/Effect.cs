using UnityEngine;

public class Effect : MonoBehaviour
{
    public int monsterID;

    float currentTime = 0;

    private void OnEnable()
    {
        currentTime = 0;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > 3f)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MonsterWeapon"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
