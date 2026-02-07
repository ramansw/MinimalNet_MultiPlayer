using UnityEngine;

public class LocalPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public NetworkBridge networkdBridge;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v);
        transform.localPosition += move * speed * Time.deltaTime;

        networkdBridge.SendPosition(transform.localPosition);
    }
}
