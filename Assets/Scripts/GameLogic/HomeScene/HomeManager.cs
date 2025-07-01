using UnityEngine;

public class HomeManager : Singleton<HomeManager>
{
    //TODO: ButtonはInspectorでアタッチするのではなくScriptでAddListenerで追加するようにする
    [SerializeField] private Transform WorldPreviewParent;
    [SerializeField] private DiveController diveController;
    [SerializeField] private ItemWordInventoryUI inventoryUI;
    private SearchWorld recalledWorld;
    private GameObject worldPreview;
    public SearchWorld RecalledWorld { get; set; }

    void Start()
    {
        if (WorldPreviewParent == null)
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
            diveController.OnRecallingCanceled += OnRecallingCanceled;
            diveController.OnRecallingCanceled += inventoryUI.ResetElementsPlace;
        }
        if (inventoryUI != null)
        {
            inventoryUI.OnWorldPredicted += OnWorldPredicted;
            inventoryUI.Initialize(true);
        }
    }
    public void OnWorldPredicted(SearchWorld searchWorld)
    {
        recalledWorld = searchWorld;
        diveController.RecalledWorld = recalledWorld;
        DisplayWorldPreview();
        diveController.gameObject.SetActive(true);
    }
    public void OnRecallingCanceled()
    {
        RemoveWorldPreview();
    }
    
    private void DisplayWorldPreview()
    {
        worldPreview = Instantiate(recalledWorld.WorldPreview, WorldPreviewParent);
        worldPreview.transform.localPosition = Vector3.zero;
        worldPreview.transform.localRotation = Quaternion.identity;
    }
    private void RemoveWorldPreview()
    {
        Destroy(worldPreview);
    }
}