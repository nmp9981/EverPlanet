using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotateAngle = 18000f;//회전각
    public GameObject centerObj;//중심 오브젝트

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = centerObj.transform.position;
        transform.Rotate(0, rotateAngle, 0);
    }
}
