using UnityEngine;

public abstract class NPC_State_Base
{
    protected BaseNPC _npc;

    public NPC_State_Base(BaseNPC npc)
    {
        this._npc = npc;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}
