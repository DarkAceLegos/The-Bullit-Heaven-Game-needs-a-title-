using Unity.Netcode;
using UnityEngine;

public abstract class Attack : NetworkBehaviour
{
    public AttackData data { get; private set; }
    public int level { get; private set; }

    public void initialize(AttackData data, int level)
    {
        this.data = data;
        this.level = level;
        OnInitialize();
    }

    protected abstract void OnInitialize();
    public abstract void Tick(NetworkObject player);
}
