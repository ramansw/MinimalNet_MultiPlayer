using UnityEngine;

public class RemotePlayerController : MonoBehaviour
{
    public float smoothSpeed = 8f;
    private Vector3 targetPosition;

    public void ReceivePosition(short x, short z)
    {
        float realX = PositionCompressor.Decompress(x);
        float realZ = PositionCompressor.Decompress(z);

        targetPosition = new Vector3(realX, transform.localPosition.y, realZ);

        Debug.Log($"Received Position: {targetPosition}");
    }

    public void ReceivePosition(short x, short y, short z)
    {
        float realX = PositionCompressor.Decompress(x);
        float realY = PositionCompressor.Decompress(y);
        float realZ = PositionCompressor.Decompress(z);

        targetPosition = new Vector3(realX, realY, realZ);

        Debug.Log($"Received Position: {targetPosition}");
    }

    void Update()
    {
        transform.localPosition =
            Vector3.Lerp(transform.localPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
