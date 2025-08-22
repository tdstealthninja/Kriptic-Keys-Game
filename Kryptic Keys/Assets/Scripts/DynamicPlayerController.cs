using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEditor;
using AYellowpaper.SerializedCollections;
using Utils;
using System;

public class DynamicPlayerController : MonoBehaviour, IDamagable
{
    #region Private Variables
    
    [SerializeField]
    private SerializedDictionary<ArtifactKeycode, ArtifactBase> artifactKeys = new SerializedDictionary<ArtifactKeycode, ArtifactBase>();

    private PriorityQueue<MoveVelocity, int> movementPriorityQueue = new PriorityQueue<MoveVelocity, int>();

    private List<MoveDirection> moveDirections = new List<MoveDirection>();

    private PlayerInput playerInput;
    [SerializeField]
    private bool playerInputActive = true;

    private Vector2 playerMovement = Vector2.zero;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    [SerializeField]
    private float baseMoveSpeed = 1f;
    [SerializeField]
    private int baseHealth = 10;

    //[SerializeField]
    //private float rotationSpeed = 90f;

    [SerializeField]
    private ArtifactInventoryUI artifactInventory;
    //[SerializeField]
    //private InputActionReference actionReference;

    #endregion

    #region Public Variables 

    public List<ArtifactBase> heldArtifacts = new List<ArtifactBase>();

    public GameObject projectileSpawnpoint;

    public int Health { get; set; }

    [HideInInspector]
    public static string PlayerLayerName = "Player";
    [HideInInspector]
    public static string EnemyLayerName = "Enemy";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        Health = baseHealth;
        playerInput = GetComponent<PlayerInput>();
        playerInput.camera = Camera.main;
        playerInput.uiInputModule = FindObjectOfType<InputSystemUIInputModule>();
        artifactInventory = FindObjectOfType<ArtifactInventoryUI>();
        playerRigidbody2D = GetComponentInParent<Rigidbody2D>();
    }

    private void OnDisable()
    {
        heldArtifacts.Clear();
        artifactKeys.Clear();
        moveDirections.Clear();
        movementPriorityQueue.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInputActive)
        {
            return;
        }
        #region New Input Checks
        foreach (InputAction input in playerInput.actions.actionMaps[0])  // NOTE: This solution works because the names of the inputs in the InputActions
        {                                                                  // are the same as the values of the ArtifactKeycode, simplifying the process                                                                          
        
            if (input.IsPressed())
            {
                System.Object obj; //System object for result of enum parse
                bool check = Enum.TryParse(typeof(ArtifactKeycode), input.name, out obj); //Checks if input name matches value in ArtifactKeycode,
                                                                                          //returns true or false and sets the obj to the result 
                ArtifactKeycode keycode = ArtifactKeycode.NONE;
                if (check) // If TryParse worked, make above keycode the result from the parse,
                           // casted as ArtifactKeycode and Activate the artifact
                {
                    keycode = (ArtifactKeycode)obj;
                    
                    ActivateArtifact(keycode);
                }
                
            }
            else if (input.WasReleasedThisFrame())
            {
                System.Object obj; //System object for result of enum parse
                bool check = Enum.TryParse(typeof(ArtifactKeycode), input.name, out obj); //Checks if input name matches value in ArtifactKeycode,
                                                                                          //returns true or false and sets the obj to the result
                ArtifactKeycode keycode = ArtifactKeycode.NONE;
                if (check) // If TryParse worked, make above keycode the result from the parse,
                           // casted as ArtifactKeycode and Deactivate the artifact
                {
                    keycode = (ArtifactKeycode)obj;
                    
                    DeactivateArtifact(keycode);
                }
            }
        }
        #endregion

        #region Old Key Pressed Checks 
        // Old way of doing key checks, was unoptimized and was missing the WasReleasedThisFrame part,
        // meaning DeactivateArtifact was happening every frame the player wasn't pushing the button that frame
        /*
        #region Row 1
        if (playerInput.actions["Q"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.Q);
        }
        else if (playerInput.actions["Q"].WasReleasedThisFrame())
        {
            Debug.Log("Q released");
            DeactivateArtifact(ArtifactKeycode.Q);
        }

        if (playerInput.actions["W"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.W);
        }
        else if (playerInput.actions["W"].WasReleasedThisFrame())
        {
            DeactivateArtifact(ArtifactKeycode.W);
        }

        if (playerInput.actions["E"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.E);
        }
        else if (playerInput.actions["E"].WasReleasedThisFrame())
        {
            DeactivateArtifact(ArtifactKeycode.E);
        }

        if (playerInput.actions["R"].IsPressed())
        {
            ActivateArtifact(ArtifactKeycode.R);
        }
        else if (playerInput.actions["R"].WasReleasedThisFrame())
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
        */
        #endregion

    }

    private void FixedUpdate()
    {
        //If a movement based artifact has been used, the action will have been added to the queue by now and the player will try to move
        // Otherwise if there are no movements queued, the Move function won't be called
        if (movementPriorityQueue.Count > 0)
            Move();
        else
            playerRigidbody2D.velocity = Vector2.zero;
    }

    void ActivateArtifact(ArtifactKeycode keycode)
    {
        ArtifactBase artifact;
        if (artifactKeys.TryGetValue(keycode,out artifact))
        {
            if (artifact)
            {
                artifact.ActivateArtifact(this);
            }
            
        }
        KeyboardArtifactManager.KeyPressedEvent?.Invoke(this, keycode);
    }

    void DeactivateArtifact(ArtifactKeycode keycode)
    {
        ArtifactBase artifact;
        if (artifactKeys.TryGetValue(keycode, out artifact))
        {
            Debug.Log(artifact);
            if (artifact)
            {
                artifact.DeactivateArtifact(this);
            }
            
        }
        KeyboardArtifactManager.KeyReleasedEvent?.Invoke(this, keycode);
    }

    public void QueueMovement(MoveVelocity movement, int priority)
    {
        Debug.Log("Queue " + movement.direction.ToString() + " movement");
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

        playerRigidbody2D.velocity = playerMovement * baseMoveSpeed * Time.deltaTime;
        
        //transform.Translate(playerMovement * baseMoveSpeed * Time.deltaTime, Space.World);

        RotateWithMove(playerMovement);

        playerMovement = Vector2.zero;
        movementPriorityQueue.Clear();
        moveDirections.Clear();
    }

    void RotateWithMove(Vector2 movement)
    {
        ///Rotate object with movement
        //Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, playerMovement.normalized);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = Quaternion.LookRotation(Vector3.forward, movement.normalized);
        
    }
    

    public bool AddArtifactToKeyboard(ArtifactBase artifact, ArtifactKeycode keycode)
    {
        Debug.Log("Artifact " + keycode + " added");
        bool added = artifactKeys.TryAdd(keycode, artifact);
        if (added)
        {
            artifact.SetAdjecentKeycodes(keycode);
        }
        return added;
    }

    public bool RemoveArtifactFromKeyboard(ArtifactKeycode keycode)
    {
        Debug.Log("Artifact " + keycode + " removed");
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
    
    public Vector2 GetPlayerMovementDirection()
    {
        return playerRigidbody2D.velocity;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;
    }

    public void PlayerControllsActive(bool toggle)
    {
        playerInputActive = toggle;
    }

    public bool IsPlayerControllsActive()
    {
        return playerInputActive;
    }

}


public enum ArtifactKeycode
{
    Q,W,E,R,T,Y,U,I,O,P,
    A,S,D,F,G,H,J,K,L,col,
    Z,X,C,V,B,N,M,
    UpArrow, DownArrow, LeftArrow, RightArrow,
    NONE
}

public enum ArtifactSynergys
{
    NONE,
    GOLD
}