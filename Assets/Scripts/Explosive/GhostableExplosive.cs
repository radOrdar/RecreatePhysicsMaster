using UnityEngine;


[RequireComponent(typeof(Explosive))]
public class GhostableExplosive : GhostableBase
{
    public override void InitGhost(Material ghostMaterial)
    {
        base.InitGhost(ghostMaterial);
        GetComponent<Explosive>().enabled = false;
    }
}