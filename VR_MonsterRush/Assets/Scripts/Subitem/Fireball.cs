using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fireball : MonoBehaviour
{
    private float _damage;
    private float _speed = 10;
    private GameObject _model;

    private void Update()
    {
        transform.LookAt(Managers.Game.Tower.transform);
        transform.Translate(Vector3.forward * _speed * Time.deltaTime, Space.Self);
        _model.transform.Rotate((Vector3.forward + Vector3.right) * 360 * Time.deltaTime);
    }

    public void Init(float damage)
    {
        _damage = damage;
        _model = transform.Find("Model").gameObject;
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Check");
        if (other.CompareTag("Tower"))
        {
            other.GetComponent<Tower>().OnDamaged(_damage);
            Managers.Resource.Instantiate("Effect/ExplosionFireball", transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
