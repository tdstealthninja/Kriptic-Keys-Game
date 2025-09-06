using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using Utils;
using UnityEngine.Events;

public class KeyboardArtifactManager : MonoBehaviour
{

    [SerializeField]
    private SerializedDictionary<ArtifactKeycode, ArtifactHolderUI> artifactKeys = new SerializedDictionary<ArtifactKeycode, ArtifactHolderUI>();

    [SerializeField]
    private DynamicPlayerController playerController;

    public UnityEvent<ArtifactHolderUI> UpdateArtifactEvent;

    public static UnityEvent<DynamicPlayerController, ArtifactKeycode> KeyPressedEvent;
    public static UnityEvent<DynamicPlayerController, ArtifactKeycode> KeyReleasedEvent;


    private void Awake()
    {
        playerController = FindObjectOfType<DynamicPlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (UpdateArtifactEvent == null)
            UpdateArtifactEvent = new UnityEvent<ArtifactHolderUI>();

        UpdateArtifactEvent.AddListener(UpdateArtifact);


        if (KeyPressedEvent == null)
            KeyPressedEvent = new UnityEvent<DynamicPlayerController, ArtifactKeycode>();

        KeyPressedEvent.AddListener(PressKey);

        if (KeyReleasedEvent == null)
            KeyReleasedEvent = new UnityEvent<DynamicPlayerController, ArtifactKeycode>();

        KeyReleasedEvent.AddListener(ReleaseKey);
    }
    private void OnDisable()
    {
        UpdateArtifactEvent.RemoveAllListeners();
        KeyPressedEvent.RemoveAllListeners();
        KeyReleasedEvent.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateArtifact(ArtifactHolderUI artifactHolder)
    {
        bool success = false;
        if (playerController)
        {
            if (artifactHolder.artifact)
            {
                success = playerController.AddArtifactToKeyboard(artifactHolder.artifact, artifactHolder.keyCode);
            }
            else
            {
                success = playerController.RemoveArtifactFromKeyboard(artifactHolder.keyCode);
            }
        }
        
    }

    void PressKey(DynamicPlayerController controller, ArtifactKeycode artifactKeycode)
    {
        if (controller == playerController)
        {
            if (artifactKeys.ContainsKey(artifactKeycode))
            {
                artifactKeys[artifactKeycode].ArtifactPressed();
            }
        }
    }

    void ReleaseKey(DynamicPlayerController controller, ArtifactKeycode artifactKeycode)
    {
        if (controller == playerController)
        {
            if (artifactKeys.ContainsKey(artifactKeycode))
            {
                artifactKeys[artifactKeycode].ArtifactReleased();
            }
        }
    }

    public DynamicPlayerController GetPlayerController()
    {
        return playerController;
    }
}
