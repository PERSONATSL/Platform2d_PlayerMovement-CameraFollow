using UnityEngine;

public class PlayerCamera : MonoBehaviour
{



    public float fellowSpeed;
    private GameObject target;
    private Vector3 distance;
    private float xLength;
    public bool death;
    public BackMusic bm;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    private void LateUpdate()
    {
        if (!death)
            cameraFellow();
        else
            cameraMoveToCenter();

    }
    private void cameraFellow()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x,
                                fellowSpeed), Mathf.Lerp(transform.position.y, target.transform.position.y,
                                    fellowSpeed / 5), transform.position.z);
    }
    private void cameraMoveToCenter()
    {
        transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, -1.5f,
                                    fellowSpeed / 2), transform.position.z);
        bm.StopPlay();
    }

}
