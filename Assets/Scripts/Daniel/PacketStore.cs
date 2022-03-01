using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketStore : MonoBehaviour
{
    public GameObject packet;

    public GameObject getPacket()
    {
        return packet;
    }

    public void setPacket(GameObject packet)
    {
        this.packet = packet;
    }
}
