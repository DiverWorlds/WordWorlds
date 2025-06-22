using UnityEngine;

public class HomeManager : Singleton<HomeManager>
{
    [SerializeField] private Transform WorldPreviewParent;
    [SerializeField] private DiveController diveController;
    [SerializeField] private ItemWordInventoryUI inventoryUI;
    private GameObject worldPreview;

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
    }
    public void DisplayWorldPreview(SearchWorld searchWorld)
    {
        if (worldPreview != null)
        {
            Destroy(worldPreview);
        }

        worldPreview = Instantiate(searchWorld.WorldPreview, WorldPreviewParent);
        worldPreview.transform.localPosition = Vector3.zero;
        worldPreview.transform.localRotation = Quaternion.identity;
    }
    public void ShowDiveController()
    {
        diveController.gameObject.SetActive(true);
    }
    public void HideDiveController()
    {
        diveController.gameObject.SetActive(false);
    }
}