using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private LineRenderer ghostLinePf;
    [SerializeField] private Material ghostMaterial;

    private LineRenderer downRayRenderer;
    private GameObject[] ghosts;

    private int currentGhostIndex;
    private GameObject currentGhost;
    private Vector3[] downRayPoints = new Vector3[2];
    private GameObject downRayHitVisual;

    public void Init(GhostableBase[] ghostables)
    {
        GameObject ghostsParent = new GameObject();
        ghostsParent.name = "Ghosts";
        ghosts = new GameObject[ghostables.Length];
        for (int i = 0; i < ghostables.Length; i++)
        {
            GhostableBase ghost = Instantiate(ghostables[i], Vector3.one, Quaternion.identity, ghostsParent.transform);
            ghost.InitGhost(ghostMaterial);
            ghosts[i] = ghost.gameObject;
            ghosts[i].SetActive(false);
        }

        currentGhost = ghosts[currentGhostIndex];
        currentGhost.SetActive(true);

        downRayRenderer = Instantiate(ghostLinePf, ghostsParent.transform);
        downRayRenderer.sharedMaterial = ghostMaterial;
        downRayRenderer.useWorldSpace = true;
        downRayRenderer.positionCount = 2;
        downRayRenderer.enabled = false;

        downRayHitVisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        downRayHitVisual.name = "GhostGroundPoint";
        downRayHitVisual.transform.localScale = 0.1f * Vector3.one;
        downRayHitVisual.transform.parent = ghostsParent.transform;
        downRayHitVisual.GetComponent<MeshRenderer>().sharedMaterial = ghostMaterial;
        downRayHitVisual.GetComponent<Collider>().enabled = false;
        downRayHitVisual.SetActive(false);
    }

    public void UpdateGhostPosition(Vector3 newPos)
    {
        currentGhost.transform.position = newPos;

        if (Physics.Raycast(newPos, Vector3.down, out RaycastHit hitInfo))
        {
            downRayPoints[0] = newPos;
            downRayPoints[1] = hitInfo.point;
            downRayRenderer.SetPositions(downRayPoints);
            downRayRenderer.enabled = true;
            downRayHitVisual.SetActive(true);
            downRayHitVisual.transform.position = hitInfo.point;
        } else
        {
            downRayRenderer.enabled = false;
            downRayHitVisual.SetActive(false);
        }
    }

    public void UpdateGhostIndex(int ghostIndex)
    {
        foreach (GameObject ghost in ghosts)
        {
            ghost.SetActive(false);
        }

        currentGhost = ghosts[ghostIndex];
        currentGhost.SetActive(true);
    }
}