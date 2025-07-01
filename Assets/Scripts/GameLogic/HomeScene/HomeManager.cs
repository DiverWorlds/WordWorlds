using UnityEngine;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField] private Transform worldPreviewParent;
    [SerializeField] private DiveController diveController;
    [SerializeField] private ItemWordInventoryUI inventoryUI;
    private GameObject worldPreview;
    public SearchWorld RecalledWorld { get; set; }

    void Start()
    {
        if (worldPreviewParent == null)
        {
            Debug.LogError("WorldPreviewParent is not assigned in HomeManager.");
        }
        if (diveController == null)
        {
            Debug.LogError("DiveController is not assigned in HomeManager.");
        }
        if (inventoryUI == null)
        {
            Debug.LogError("ItemWordInventoryUI is not assigned in HomeManager.");
        }
        if (diveController != null)
        {
            diveController.OnRecallingCanceled += RemoveWorldPreview;
            diveController.OnRecallingCanceled += inventoryUI.ResetPlacedElements;
        }
        if (inventoryUI != null)
        {
            inventoryUI.OnWorldPredicted += OnWorldPredicted;
            inventoryUI.Initialize(true);
        }
    }
    private void OnWorldPredicted(SearchWorld searchWorld)
    {
        diveController.RecalledWorld = searchWorld;
        DisplayWorldPreview(searchWorld);
        diveController.gameObject.SetActive(true);
    }
    private void DisplayWorldPreview(SearchWorld searchWorld)
    {
        worldPreview = Instantiate(searchWorld.WorldPreview, worldPreviewParent);
        worldPreview.transform.localPosition = Vector3.zero;
        worldPreview.transform.localRotation = Quaternion.identity;
    }
    private void RemoveWorldPreview()
    {
        Destroy(worldPreview);
    }
}