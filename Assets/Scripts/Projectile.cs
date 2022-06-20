using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private float _force = 50.0f;

    [SerializeField] private float _damage = 3.0f;

    private Weapon weapon;
    private Player player;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            player = collision.transform.GetComponent<CarController>().player;

            player.UpdateHealth(_damage);

            Destroy (this.gameObject);
        }

        else if (collision.transform.CompareTag("Enemy"))
        {
            weapon = GameObject.FindGameObjectWithTag("PlayerGun").transform.GetComponentInChildren<Gun>().weapon;
            player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<CarController>().player;

            if(weapon.CriticalDamage(weapon.critRate))
            {
                collision.transform.GetComponent<AI>().enemy.UpdateEnemyHealth(weapon.TotalDamage() * 3);
                Debug.Log("CritDam");
            }

            else
            {
                collision.transform.GetComponent<AI>().enemy.UpdateEnemyHealth(weapon.TotalDamage());
            }

            Destroy (this.gameObject);
        }

        else
        {
            Destroy (this.gameObject);
        }
    }
}
