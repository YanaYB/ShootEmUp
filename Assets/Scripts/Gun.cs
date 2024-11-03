using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float rotationSpeed = 50f; // �������� �������� ����
    public float minAngle = -45f; // ����������� ���� �������� (������ ������)
    public float maxAngle = 45f; // ������������ ���� �������� (������� ������)
    public GameObject bulletPrefab;  // ������ ����
    public Transform firePoint;      // �����, ������ ����� �������� ����
    public float bulletSpeed = 10f;  // �������� ����

    void Update()
    {
        // ������������ ����� � �������
        AimAtMouse();

        // ������ ����
        if (Input.GetButtonDown("Fire1")) // Fire1 - ����������� ������ ��� �������� (����� ������ ����)
        {
            Shoot();
        }
    }

    void AimAtMouse()
    {
        // �������� ������������ ���� �� ������������ (�������� �� -1 �� 1)
        float verticalInput = Input.GetAxis("Vertical");

        // ������������ ��������� ���� �������� �� ������ �������� � �������
        float rotationAmount = verticalInput * rotationSpeed * Time.deltaTime;

        // ������� ���� �������� ���� (���� � ������������ Z)
        float currentAngle = transform.eulerAngles.z;

        // ����������� ���� �� ��������� [0, 360] � �������� [-180, 180] ��� ��������
        if (currentAngle > 180) currentAngle -= 360;

        // ������������ ����� ����, �������� �������
        float newAngle = Mathf.Clamp(currentAngle + rotationAmount, minAngle, maxAngle);

        // ��������� ����� ���� �������� � ��� Z
        transform.rotation = Quaternion.Euler(0, 0, newAngle);
    }

    void Shoot()
    {
        // ������� ���� � ����� firePoint � ��������, ��� � ����
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // ������� ����������� �� firePoint �� ������� ������� ����
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // ��������� ����������� � ����
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * bulletSpeed; // ������ �������� ���� � ������ �����������
        }
    }



}
