using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactInventoryUI : MonoBehaviour
{
    [SerializeField]
    private ArtifactHolderUI artifactHolderUIPrefab;

    public List<ArtifactHolderUI> artifactHolders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToInventory(ArtifactBase artifact)
    {
        ArtifactHolderUI artifactHolder = Instantiate<ArtifactHolderUI>(artifactHolderUIPrefab, gameObject.transform);
        artifactHolder.artifact = artifact;
    
    }
}
