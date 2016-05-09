using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject rock;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject power1;
    public GameObject power2;
    public float rockRate = 5;
    public float enemy1Rate = 6;
    public float enemy2Rate = 10;
    public float enemy3Rate = 15;
    public float power1Rate = 5;
    public float power2Rate = 25;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CreateRocks", 1, rockRate);
        InvokeRepeating("CreateEnemy1", 5, enemy1Rate);
        InvokeRepeating("CreateEnemy2", 8, enemy2Rate);
        InvokeRepeating("CreateEnemy3", 15, enemy3Rate);
        InvokeRepeating("CreatePower1", 10, power1Rate);
        InvokeRepeating("CreatePower2", 15, power2Rate);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateRocks()
    {
        float x=Random.Range(-600,600);
        GameObject.Instantiate(rock, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    void CreateEnemy1()
    {
        float x = Random.Range(-600, 600);
        GameObject.Instantiate(enemy1, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    void CreateEnemy2()
    {
        float x = Random.Range(-600, 600);
        GameObject.Instantiate(enemy2, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    void CreateEnemy3()
    {
        float x = Random.Range(-600, 600);
        GameObject.Instantiate(enemy3, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    void CreatePower1()
    {
        float x = Random.Range(-600, 600);
        GameObject.Instantiate(power1, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }

    void CreatePower2()
    {
        float x = Random.Range(-600, 600);
        GameObject.Instantiate(power2, new Vector3(x, transform.position.y, 0), Quaternion.identity);
    }
}
