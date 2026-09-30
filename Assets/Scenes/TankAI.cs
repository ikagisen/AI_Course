using UnityEngine;

public class TankAI : MonoBehaviour
{
    public Transform target; // Цель (враг)
    public Transform cannon; // Дуло танка
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint; // Точка выстрела (на конце дула)

    public float projectileSpeed = 20f; // Начальная скорость снаряда
    public float gravity = 9.81f; // Гравитация

    // Новые переменные для задержки стрельбы
    public float fireRate = 2f; // Время между выстрелами (в секундах)
    private float nextFireTime = 0f; // Таймер

    void Update()
    {
        if (target != null)
        {
            AimAndFire();
        }
    }

    void AimAndFire()
    {
        // 1. Находим дистанцию до врага
        float distance = Vector3.Distance(transform.position, target.position);

        // 2. Рассчитываем значение для арксинуса
        float valueToArcsin = (distance * gravity) / (projectileSpeed * projectileSpeed);

        // Если цель слишком далеко — не стреляем
        if (valueToArcsin > 1f) return;

        // 3. Рассчитываем угол наклона
        float angleRadians = 0.5f * Mathf.Asin(valueToArcsin);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;

        // 4. Поворачиваем пушку
        cannon.localRotation = Quaternion.Euler(-angleDegrees, 0f, 0f);

        // 5. Логика выстрела с таймером
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Сбрасываем таймер
        }
    }

    void Shoot()
    {
        // Создаем копию снаряда в позиции и с поворотом FirePoint
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Получаем компонент физики снаряда
        Rigidbody rb = proj.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Толкаем снаряд вперед от точки выстрела с заданной скоростью
            rb.linearVelocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            Debug.LogError("На префабе снаряда нет компонента Rigidbody!");
        }
    }
}