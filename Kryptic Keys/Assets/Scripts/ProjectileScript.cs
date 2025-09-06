using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class ProjectileScript : MonoBehaviour, IProjectile
{
    public float projectileSpeed = 600f;
    public float projectileLifetime = 30f;
    public float projectileTimer = 0f;
    public float projectileDecaySpeed = 0.01f;
    public float projectileDecaySpeedMultiplier = 1f;
    public bool playerProjectile { get; set; }
    public bool canCombo { get; set; }

    

    [HideInInspector]
    public static string PlayerProjectileLayerName = "PlayerProjectile";
    [HideInInspector]
    public static string EnemyProejctileLayerName = "EnemyProjectile";

    [SerializeField]
    protected Rigidbody2D projectileRigidbody2D;
    protected bool isPlayerProjectile = false;
    [SerializeField]
    protected int numBounces = 1;
    [SerializeField]
    protected int damage = 1;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        if (projectileRigidbody2D == null)
            projectileRigidbody2D = GetComponent<Rigidbody2D>();
        playerProjectile = isPlayerProjectile;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        ProjectileDecay();
    }

    void ProjectileDecay()
    {
        if (projectileTimer < projectileLifetime)
            projectileTimer += projectileDecaySpeed * projectileDecaySpeedMultiplier;
        else
            Destroy(gameObject);
    }

    public void OnHit()
    {
        
    }

    public void Combo(ProjectileScript other)
    {
        
    }

    public void DestroyProjectle()
    {
        Destroy(gameObject);
    }

    public void SetDamage(int dam)
    {
        damage = dam;
    }
    public int GetDamage()
    {
        return damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (playerProjectile)
        {
            if (numBounces > 0)
            {
                numBounces -= 1;
                projectileRigidbody2D.velocity *= -1;

            }
            else if (numBounces <= 0)
            {
                DestroyProjectle();
            }
        }
        else
        {
            ProjectileScript otherProjectile = collision.gameObject.GetComponent<ProjectileScript>();
            if (otherProjectile)
            {
                otherProjectile.OnHit();
                OnHit();
            }
            else if (numBounces > 0)
            {
                numBounces -= 1;
                projectileRigidbody2D.velocity *= -1;

            }
            else if (numBounces <= 0)
            {
                DestroyProjectle();
            }
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable hit = collision.GetComponent<IDamagable>();
        if (playerProjectile)
        {
            if (collision.GetComponent<DynamicPlayerController>())
            {
                //Do nothing
            }
            else
            {

            }
        }
        else
        {
            if (hit != null)
            {
                hit.Damage(damage);
            }
            
        }
    }

    
}
