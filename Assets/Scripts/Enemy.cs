using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public float firingSpeed;
    [SerializeField] public GameObject bullet;
    private bool isShooting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isShooting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            StartCoroutine(Shoot());
            isShooting = false;
        }

    }


    //TO DO metodo para disparar
    IEnumerator Shoot()
    {
        isShooting = false;
        //direcion del disparo
        Vector3 direction = (target.transform.position - transform.position).normalized;
        //calculo del angulo que debe tener la bala
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //rotacion de la bala
        Quaternion rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        //instanciacion de la bala
        GameObject newBullet = PoolManager.Instance.ObtenerObjeto(bullet);
        newBullet.transform.position = transform.position;
        newBullet.transform.rotation = rotation;
        newBullet.SetActive(true);// se activa la bala
        //asignacion de la velocidad a la bala y el movimiento
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = direction * firingSpeed;
        //coldown del disparo
        yield return new WaitForSeconds(firingSpeed);
        isShooting = true;

    }
}
