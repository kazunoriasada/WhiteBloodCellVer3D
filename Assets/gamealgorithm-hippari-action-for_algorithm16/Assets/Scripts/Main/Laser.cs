using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HippariAction.Main
{
    public class Laser : MonoBehaviour
    {
        int power;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                var enemy = collision.GetComponent<Enemy>();
                enemy.Damage(power);
            }
        }

        public void StartAnimate(int power)
        {
            this.power = power;
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 0.3f)
                     .OnComplete(() =>
                     {
                         transform.localScale = Vector3.one;
                         Destroy(gameObject);
                     });
        }
    }
}
