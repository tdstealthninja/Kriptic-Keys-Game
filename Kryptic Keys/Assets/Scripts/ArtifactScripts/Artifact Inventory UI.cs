using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtifactInventoryUI : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ArtifactHolderUI artifactHolderUIPrefab;
    private System.Predicate<ArtifactHolderUI> nullCheck = HolderDoesNotHaveArtifact;
    private List<ArtifactHolderUI> emptyHolders = new List<ArtifactHolderUI>();
    [SerializeField]
    private GameObject textObject;
    [SerializeField]
    private Image inventoryImage;
    private bool inventoryUiOpen = false;

    public List<ArtifactHolderUI> artifactHolders = new List<ArtifactHolderUI>();



    private void Awake()
    {
        inventoryImage = GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        emptyHolders = artifactHolders.FindAll(nullCheck);
        foreach (ArtifactHolderUI artifactHolder in emptyHolders)
        {
            Destroy(artifactHolder.gameObject);
        }
        emptyHolders.Clear();
        
        artifactHolders.RemoveAll(nullCheck);
    }

    public void AddToInventory(ArtifactBase artifact)
    {
        ArtifactHolderUI artifactHolder = Instantiate<ArtifactHolderUI>(artifactHolderUIPrefab, gameObject.transform);
        artifactHolder.artifact = artifact;
        artifactHolders.Add(artifactHolder);
        artifactHolder.SpawnArtifact();
    
    }

    public void ReturnToInventory(PointerEventData eventData)
    {
        KeyboardArtifactManager keyboard = eventData.pointerDrag.GetComponent<ArtifactBase>().lastHolder.GetComponentInParent<KeyboardArtifactManager>();

        if (keyboard)
        {
            ArtifactHolderUI artifactHolder = Instantiate<ArtifactHolderUI>(artifactHolderUIPrefab, gameObject.transform);
            artifactHolder.OnDrop(eventData);
            artifactHolders.Add(artifactHolder);
        }

        
    }


    private static bool HolderDoesNotHaveArtifact(ArtifactHolderUI artifactHolder)
    {
        if (artifactHolder.artifact != null)
            return false;
        return true;
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        ReturnToInventory(eventData);
    }

    public void ToggleInventoryUI()
    {
        if (inventoryUiOpen)
        {
            textObject.SetActive(false);
            inventoryImage.enabled = false;
            inventoryUiOpen = false;
        }
        else
        {
            textObject.SetActive(true);
            inventoryImage.enabled = true;
            inventoryUiOpen = true;
        }
    }
}
