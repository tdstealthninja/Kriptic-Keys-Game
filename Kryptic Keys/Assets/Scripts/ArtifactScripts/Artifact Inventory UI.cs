using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArtifactInventoryUI : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private ArtifactHolderUI artifactHolderUIPrefab;
    private System.Predicate<ArtifactHolderUI> nullCheck = HolderDoesNotHaveArtifact;
    private List<ArtifactHolderUI> emptyHolders = new List<ArtifactHolderUI>();


    public List<ArtifactHolderUI> artifactHolders = new List<ArtifactHolderUI>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        ArtifactHolderUI artifactHolder = Instantiate<ArtifactHolderUI>(artifactHolderUIPrefab, gameObject.transform);
        artifactHolder.OnDrop(eventData);
        artifactHolders.Add(artifactHolder);
        
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
}
