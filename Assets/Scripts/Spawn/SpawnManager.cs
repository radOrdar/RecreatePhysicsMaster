using DG.Tweening;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GhostableBase[] objectsToSpawnPf;

    [SerializeField] private GhostController ghostController;
    [SerializeField] private float minSpawnRange = 2;
    [SerializeField] private float maxSpawnRange = 8;

    [SerializeField] private float mouseScrollSensitivity = 0.1f;

    private int currentObjIndex = 0;
    private float currentSpawnDistance;

    private Camera mainCamera;
    private Transform spawnParent;

    private void Start()
    {
        mainCamera = Camera.main;
        spawnParent = GameObject.Find("Objects").transform;

        ghostController.Init(objectsToSpawnPf);
    }

    private void Update()
    {
        currentSpawnDistance = Mathf.Clamp(currentSpawnDistance + Input.mouseScrollDelta.y * mouseScrollSensitivity,
            minSpawnRange, maxSpawnRange);
        
        Vector3 spawnPos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, currentSpawnDistance));

        AlternateObjectsToSpawn();
        if (Input.GetMouseButtonDown(0))
        {
            SpawnObject(spawnPos);
        }

        ghostController.UpdateGhostPosition(spawnPos);
    }

    private void SpawnObject(Vector3 pos)
    {
        var go = Instantiate(objectsToSpawnPf[currentObjIndex], pos, Quaternion.identity, spawnParent);
        SpawnEffects(go.transform);
    }

    private static void SpawnEffects(Transform go)
    {
        AudioManager.instance.PlaySpawnAudio(go.position);
        VFXManager.instance.PlaySpawnFx(go.position);
        go.DOScale(0, .25f).From();
    }

    private void AlternateObjectsToSpawn()
    {
        if (Input.GetKeyDown(KeyCode.V) == false) return;

        currentObjIndex++;
        currentObjIndex = currentObjIndex > objectsToSpawnPf.Length - 1 ? 0 : currentObjIndex;

        ghostController.UpdateGhostIndex(currentObjIndex);
    }
}