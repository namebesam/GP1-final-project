using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    [Header("ProjectileSettings")]
    public GameObject patronusProjectile;

    public float projectileSpeed = 100;

    public AudioClip spellSFX;

    public float spellRange = 20;

    [Header("ReticleSettings")]
    public Image reticleImage;

    public Color targetColorDementor;

    Color originalReticleColor;

    public float animationSpeed = 3;

    Vector3 originalReticleScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalReticleColor = reticleImage.color;
        originalReticleScale = reticleImage.transform.localScale;
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
        ReticleEffect();
    }

    void Shoot()
    {
        if(patronusProjectile)
        {
            GameObject spell = Instantiate(patronusProjectile, transform.position + transform.forward * 0.25f, transform.rotation);
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

            //spell.transform.SetParent(transform);
        }
    }

    void ReticleEffect()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, spellRange))
        {
            Debug.Log("Hit something" + hit.collider.name);
            if (hit.collider.CompareTag("Dementor"))
            {
                //animate reticle
                var step = animationSpeed * Time.deltaTime;
                reticleImage.color = Color.Lerp(reticleImage.color, targetColorDementor, step);
                reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, originalReticleScale / 2, step);
            }
            else
            {

            }
        }
        else
        {
            var step = animationSpeed * Time.deltaTime;
            reticleImage.color = Color.Lerp(reticleImage.color, originalReticleColor, step);
            reticleImage.transform.localScale = Vector3.Lerp(reticleImage.transform.localScale, originalReticleScale, step);
        }
    }
}
