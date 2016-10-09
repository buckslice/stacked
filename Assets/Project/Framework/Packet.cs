using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// helper class for sending and receiving serialized data across network
public class Packet {

    private byte[] buffer;

    private MemoryStream stream;
    private BinaryFormatter formatter = new BinaryFormatter();

    /// <summary>
    /// Build a packet for writing data with default buffer size of 1024 bytes
    /// </summary>
    public Packet() {
        buffer = new byte[1024];
        stream = new MemoryStream(buffer, true);
    }
    /// <summary>
    /// Build a packet for writing data with specified buffer size in bytes
    /// </summary>
    public Packet(int bufsize) {
        buffer = new byte[bufsize];
        stream = new MemoryStream(buffer, true);
    }
    /// <summary>
    /// Build a packet for reading data
    /// </summary>
    /// <param name="buffer">data to be read</param>
    public Packet(byte[] buffer) {
        stream = new MemoryStream(buffer, false);
    }

    public Packet(object buffer) {
        stream = new MemoryStream((byte[])buffer, false);
    }

    public void Write(Vector3 v) {
        formatter.Serialize(stream, v.x);
        formatter.Serialize(stream, v.y);
        formatter.Serialize(stream, v.z);
    }
    public void Write(Quaternion q) {
        formatter.Serialize(stream, q.x);
        formatter.Serialize(stream, q.y);
        formatter.Serialize(stream, q.z);
        formatter.Serialize(stream, q.w);
    }
    public void Write(Color32 c) {
        formatter.Serialize(stream, c.r);
        formatter.Serialize(stream, c.g);
        formatter.Serialize(stream, c.b);
        formatter.Serialize(stream, c.a);
    }
    public void Write(object graph) {
        formatter.Serialize(stream, graph);
    }

    public string ReadString() {
        return (string)formatter.Deserialize(stream);
    }
    public byte ReadByte() {
        return (byte)formatter.Deserialize(stream);
    }
    public bool ReadBool() {
        return (bool)formatter.Deserialize(stream);
    }
    public int ReadInt() {
        return (int)formatter.Deserialize(stream);
    }
    public float ReadFloat() {
        return (float)formatter.Deserialize(stream);
    }
    public Vector3 ReadVector3() {
        return new Vector3(ReadFloat(), ReadFloat(), ReadFloat());
    }
    public Quaternion ReadQuaternion() {
        return new Quaternion(ReadFloat(), ReadFloat(), ReadFloat(), ReadFloat());
    }
    public Color32 ReadColor() {
        return new Color32(ReadByte(), ReadByte(), ReadByte(), ReadByte());
    }
    public object ReadObject() {
        return formatter.Deserialize(stream);
    }

    /// <summary>
    /// Returns packet data to be sent over network
    /// </summary>
    /// <returns></returns>
    public byte[] getData() {
        return buffer;
    }
    /// <summary>
    /// Gets length of data
    /// </summary>
    /// <returns></returns>
    public int getSize() {
        return (int)stream.Position;
    }

}
