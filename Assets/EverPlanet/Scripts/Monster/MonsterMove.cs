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

    // Update is called once per frame
    void Update()
    {
        HPBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        HPBarBack.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.7f, 0));
        mobInfo.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 0.25f, 0));

        mobInfo.text = "[" + 80 + "] Mushroom";
    }
}
