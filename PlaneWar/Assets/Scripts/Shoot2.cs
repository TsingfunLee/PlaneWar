using UnityEngine;
using System.Collections;

public class Shoot2 : MonoBehaviour {

    public static Shoot2 Instance = null;

    public GameObject projectile;
    public float rate = 0.1f;
    public AudioSource audio;
    public AudioClip clip;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        OpenFire();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenFire()
    {
        InvokeRepeating("LaunchProjectile", 1, rate);
    }

    public void StopFire()
    {
        CancelInvoke("LaunchProjectile");
    }

    void LaunchProjectile()
    {
        GameObject.Instantiate(projectile, transform.position, Quaternion.identity);
        audio.PlayOneShot(clip);
    }
}
