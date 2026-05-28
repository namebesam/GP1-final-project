using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    [Header("ProjectileSettings")]
    public GameObject patronusProjectile;
    public GameObject reductoProjectile;
    public GameObject alohomoraProjectile;
    public GameObject defaultProjectile;

    public float projectileSpeed = 100;

    public AudioClip spellSFX;

    public float spellRange = 20;

    [Header("ReticleSettings")]
    public Image reticleImage;

    public Color targetColorDementor;

    Color originalReticleColor;

    public float animationSpeed = 3;

    Vector3 originalReticleScale;

    GameObject currentProjectile;

    Color currentReticleColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalReticleColor = reticleImage.color;
        originalReticleScale = reticleImage.transform.localScale;

        if (defaultProjectile) {
            currentProjectile = defaultProjectile;
        }

        currentReticleColor = Color.yellow;
        UpdateReticleColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if(!reticleImage)
        {
            return;
        }
        InteractiveEffect();
    }

    void Shoot()
    {
        if(currentProjectile)
        {
            GameObject spell = Instantiate(currentProjectile, transform.position + transform.forward * 0.25f, transform.rotation);
            //added a bit of code to spawn the projectiles slightly in front of the player 
            //to fix issue of  projectile hitting inside of player collider

            Rigidbody rb = spell.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
            }

            if (spellSFX)
            {
                AudioSource.PlayClipAtPoint(spellSFX, transform.position);
            }

           // spell.transform.SetParent(transform);
        }
    }

    void InteractiveEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, spellRange))
        {
            Debug.Log("Hit something" + hit.collider.name);
            if (hit.collider.CompareTag("Dementor"))
            {
                currentProjectile = patronusProjectile;
                UpdateReticleColor();
                //animate reticle
                ReticleAnimation(originalReticleScale / 2, currentReticleColor, animationSpeed);
            }
            else if (hit.collider.CompareTag("Reducto"))
            {
                currentProjectile = reductoProjectile;
                UpdateReticleColor();
                ReticleAnimation(originalReticleScale / 2, currentReticleColor, animationSpeed);
            }
            else if (hit.collider.CompareTag("Alohomora"))
            {
                currentProjectile = alohomoraProjectile;
                UpdateReticleColor();
            }
        }
        else
        {
            currentProjectile = defaultProjectile;
            UpdateReticleColor();
            ReticleAnimation(originalReticleScale, originalReticleColor, animationSpeed);
        }
    }

    void ReticleAnimation(Vector3 targetScale, Color targetColor, float speed)
    {
        var step = speed * Time.deltaTime;
        reticleImage.color = Color.Lerp(reticleImage.color, targetColor, step);
        reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, targetScale, step);
    }

    void UpdateReticleColor()
    {
        currentReticleColor = currentProjectile.GetComponent<Renderer>().sharedMaterial.color;
    }
}
