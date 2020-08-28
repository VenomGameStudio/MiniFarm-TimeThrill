using UnityEngine;

public class CamaraController : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    public Vector2 minPosition;
    public Vector2 maxPosition;


    void LateUpdate()
    {        
        if (transform.position != target.position)
        {
            Vector3 targetPostion = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPostion.x = Mathf.Clamp(target.position.x, minPosition.x, maxPosition.x);
            targetPostion.y = Mathf.Clamp(target.position.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPostion, smoothing);
        }
    }
}
