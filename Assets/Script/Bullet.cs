using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float bulletSpped = 10f;
    public Enemy enemy;
    public float lifeTime = 5f;
    float currentTime;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            enemy.SetHp(damage);
            Destroy(this.gameObject);
        }
        if(other.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
        this.transform.Translate(Vector3.forward * bulletSpped * Time.deltaTime);
        if(lifeTime < currentTime)
        {
            Destroy(this.gameObject);
        }
    }


}
