using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FlightController : MonoBehaviour
{
    public float torque = 1000f;
    public float thrust = 100f;
    public float glideDrag;
    private float glide;
    private Rigidbody rb;

    // Health
    public float health = 300f;
    public Image healthUI;
    private float maxHealth;

    public GameObject explosion;
    public ParticleSystem explosionPart;

    // Back Light
    private GameObject exhaustLight;
    private Light exLt;

    void Start()
    {
        glide = 0f;
        rb = GetComponent<Rigidbody>();
        exhaustLight = this.gameObject.transform.GetChild(3).gameObject;
        exLt = exhaustLight.GetComponent<Light>();
        maxHealth = health;
    }

    void FixedUpdate() 
    {
        float roll = Input.GetAxis("Horizontal");
        float pitch = Input.GetAxis("Vertical");
        bool throttle = Input.GetKey("space");
        float intensitySmooth = 2f;

        rb.AddRelativeTorque(Vector3.back * torque * roll);
        rb.AddRelativeTorque(Vector3.right * torque * pitch);
        if(throttle) 
        {
            rb.AddRelativeForce(Vector3.forward * thrust);
            glide = thrust;
            exLt.intensity = Mathf.Lerp(exLt.intensity,100f,Time.deltaTime * intensitySmooth);

        }
        else
        {   
            rb.AddRelativeForce(Vector3.forward * glide);
            glide *= glideDrag;
            exLt.intensity = Mathf.Lerp(exLt.intensity,15f,Time.deltaTime * intensitySmooth);

        }

        
    }

    public void damage (float damageAmount)
    {
        health -= damageAmount;
        healthUI.fillAmount = health/maxHealth;

        if(health <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionPart, transform.position, Quaternion.identity);  
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }
    }
}
