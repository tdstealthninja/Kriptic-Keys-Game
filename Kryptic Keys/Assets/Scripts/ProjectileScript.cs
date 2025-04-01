using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public float projectileSpeed = 600f;
    public float projectileLifetime = 30f;
    public float projectileTimer = 0f;
    public float projectileDecaySpeed = 0.01f;
    public float projectileDecaySpeedMultiplier = 1f;

    [SerializeField]
    private Rigidbody2D projectileRigidbody2D;


    private void Awake()
    {
        if (projectileRigidbody2D == null)
            projectileRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileTimer < projectileLifetime)
            projectileTimer += projectileDecaySpeed * projectileDecaySpeedMultiplier;
        else
            Destroy(gameObject);
    }
}
