using Unity.Netcode;
using UnityEngine;

[System.Serializable]
public class ItemList //: INetworkSerializable
{
    public Item item;
    public string name;
    public int stacks;

    public ItemList(Item newItem, string newName, int NetStacks)
    {
        item = newItem;
        name = newName;
        stacks = NetStacks;
    }

    /*public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        //serializer.SerializeValue(ref item);
        serializer.SerializeValue(ref name);
        serializer.SerializeValue(ref stacks);
    }*/
}
