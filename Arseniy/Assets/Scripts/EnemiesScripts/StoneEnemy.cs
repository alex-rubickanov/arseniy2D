using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneEnemy : MonoBehaviour
{
    [SerializeField] private int _health = 100; // �������� ���������
    [SerializeField] private float _speed = 2f; // �������� ���������
    [SerializeField] private int _stopSteps = 10; // ���������� ����� ����� �����������
    [SerializeField] private float _stopDuration = 2f; // ����������������� ���������
    [SerializeField] private GameObject _rockPrefab; // ������ ������
    [SerializeField] private Transform _throwPoint; // ����� ������ ������
    [SerializeField] private Slider _healthBar;

    private int _steps = 0; // ������� �����
    private bool _isStopping = false; // ���� ���������
    private float _stopTime = 0f; // ����� ���������
    private Rigidbody2D _rigidbody; // ��������� ������

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _throwPoint.position = transform.position;
    }

    private void Update()
    {

        CheckDeath();

        if (!_isStopping)
        {
            // ���� �� ����������, ��������� ������
            Move();
            _steps++;

            // ���� ���������� ������ ���������� �����, ���������������
            if (_steps >= _stopSteps)
            {
                Stop();
            }
        }
        else
        {
            // ���� �����������, ���� ��������� �����
            if (Time.time >= _stopTime)
            {
                _isStopping = false;

                // ������� ����� � ������� ��� � �����
                ThrowRock();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _healthBar.value = _health;
    }

    private void Stop()
    {
        _isStopping = true;
        _steps = 0;
        _stopTime = Time.time + _stopDuration;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, 0, 0), _speed * Time.deltaTime);
    }

    private void ThrowRock()
    {
        GameObject rock = Instantiate(_rockPrefab, _throwPoint.position, Quaternion.identity);
        Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();
        rockRigidbody.AddForce(Vector2.left * 500f);
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
