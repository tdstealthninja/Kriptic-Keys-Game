using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using AYellowpaper.SerializedCollections;
using Utils;

public class DynamicPlayerController : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField]
    private SerializedDictionary<ArtifactKeycode, ArtifactBase> artifactKeys = new SerializedDictionary<ArtifactKeycode, ArtifactBase>();

    private PriorityQueue<MoveVelocity, int> movementPriorityQueue = new PriorityQueue<MoveVelocity, int>();

    private List<MoveDirection> moveDirections = new List<MoveDirection>();

    private PlayerInput playerInput;

    private Vector2 playerMovement = Vector2.zero;

    [SerializeField]
    private float baseMoveSpeed = 1f;

    [SerializeField]
    private ArtifactInventoryUI artifactInventory;

    #endregion

    #region Public Variables 

    public List<ArtifactBase> heldArtifacts = new List<ArtifactBase>();

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Key Pressed Checks

        #region Row 1
        if (playerInput.actions["Q"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.Q);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.Q);
        }

        if (playerInput.actions["W"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.W);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.W);
        }

        if (playerInput.actions["E"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.E);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.E);
        }

        if (playerInput.actions["R"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.R);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.R);
        }

        if (playerInput.actions["T"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.T);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.T);
        }

        if (playerInput.actions["Y"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.Y);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.Y);
        }

        if (playerInput.actions["U"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.U);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.U);
        }

        if (playerInput.actions["I"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.I);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.I);
        }

        if (playerInput.actions["O"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.O);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.O);
        }

        if (playerInput.actions["P"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.P);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.P);
        }

        #endregion

        #region Row 2
        if (playerInput.actions["A"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.A);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.A);
        }

        if (playerInput.actions["S"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.S);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.S);
        }

        if (playerInput.actions["D"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.D);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.D);
        }

        if (playerInput.actions["F"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.F);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.F);
        }

        if (playerInput.actions["G"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.G);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.G);
        }

        if (playerInput.actions["H"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.H);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.H);
        }

        if (playerInput.actions["J"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.J);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.J);
        }

        if (playerInput.actions["K"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.K);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.K);
        }

        if (playerInput.actions["L"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.L);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.L);
        }

        #endregion

        #region Row 3

        if (playerInput.actions["Z"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.Z);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.Z);
        }

        if (playerInput.actions["X"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.X);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.X);
        }

        if (playerInput.actions["C"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.C);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.C);
        }

        if (playerInput.actions["V"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.V);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.V);
        }

        if (playerInput.actions["B"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.B);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.B);
        }

        if (playerInput.actions["N"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.N);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.N);
        }

        if (playerInput.actions["M"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.M);
        }
        else
        {
            DeactivateArtifact(ArtifactKeycode.M);
        }

        #endregion

        #endregion

        //If a movement based artifact has been used, the action will have been added to the queue by now and the player will try to move
        // Otherwise if there are no movements queued, the Move function won't be called
        if (movementPriorityQueue.Count > 0)
            Move();
    }

    void ActivateArtifact(ArtifactKeycode keycode)
    {
        ArtifactBase artifact;
        if (artifactKeys.TryGetValue(keycode,out artifact))
        {
            artifact.ActivateArtifact(this);
        }
        KeyboardArtifactManager.KeyPressedEvent?.Invoke(this, keycode);
    }

    void DeactivateArtifact(ArtifactKeycode keycode)
    {
        ArtifactBase artifact;
        if (artifactKeys.TryGetValue(keycode, out artifact))
        {
            artifact.DeactivateArtifact(this);
        }
        KeyboardArtifactManager.KeyReleasedEvent?.Invoke(this, keycode);
    }

    public void QueueMovement(MoveVelocity movement, int priority)
    {
        movementPriorityQueue.Enqueue(movement, priority);
    }

    void Move()
    {
        MoveVelocity movement;
        int currentPriority;
        while (movementPriorityQueue.TryDequeue(out movement, out currentPriority))
        {
            if (moveDirections.Contains(movement.direction))
            {
                Debug.Log(movement.direction + " movement blocked");
                continue;
            }

            playerMovement += movement.move;
            moveDirections.Add(movement.direction);
        }
        
        transform.Translate(playerMovement * baseMoveSpeed * Time.deltaTime);
        playerMovement = Vector2.zero;
        movementPriorityQueue.Clear();
        moveDirections.Clear();
    }

    public bool AddArtifactToKeyboard(ArtifactBase artifact, ArtifactKeycode keycode)
    {
        return artifactKeys.TryAdd(keycode, artifact);
    }

    public bool RemoveArtifactFromKeyboard(ArtifactKeycode keycode)
    {
        return artifactKeys.Remove(keycode);
    }

    
    public void AddArtifactToInventory(ArtifactBase artifact)
    {
        Debug.Log("Add to Inventory");
        if (artifactInventory)
        {
            artifactInventory.AddToInventory(artifact);
            heldArtifacts.Add(artifact);
        }
    }
    
    

}


public enum ArtifactKeycode
{
    Q,W,E,R,T,Y,U,I,O,P,
    A,S,D,F,G,H,J,K,L,
    Z,X,C,V,B,N,M,
    UpArrow, DownArrow, LeftArrow, RightArrow,
    NONE
}