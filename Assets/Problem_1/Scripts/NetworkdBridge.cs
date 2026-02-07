using UnityEngine;

public class NetworkBridge : MonoBehaviour
{
    public RemotePlayerController remotePlayer;
    public bool includeY = false;

    public void SendPosition(Vector3 position)
    {
        short x = PositionCompressor.Compress(position.x);
        short z = PositionCompressor.Compress(position.z);
        short y = PositionCompressor.Compress(position.y);

        int sizeBits = PositionCompressor.DataSizeBits(includeY);

        Debug.Log($"Sent Position: {position} | Data Size: {sizeBits} bits");

        if (includeY)
            remotePlayer.ReceivePosition(x, y, z);
        else
            remotePlayer.ReceivePosition(x, z);
    }
}
