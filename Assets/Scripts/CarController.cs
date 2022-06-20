using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    public Player player = new Player();

    private float _horizontalInput;
    private float _verticalInput;
    private bool _isBraking;
    private float steeringAngle = 0.0f;

    private Button _brakeButton;

    [SerializeField] private Joystick _joystickHorizontal;
    [SerializeField] private Joystick _joystickVertical;

    [SerializeField] private float _motorForce = 100.0f;
    [SerializeField] private float _brakeTorque = 100.0f;
    [SerializeField] private float _maxSteeringAngle = 45.0f;

    [SerializeField] private WheelCollider _frontRightWheelCollider;
    [SerializeField] private WheelCollider _frontLeftWheelCollider;
    [SerializeField] private WheelCollider _rearRightWheelCollider;
    [SerializeField] private WheelCollider _rearLeftWheelCollider;

    [SerializeField] private Transform _frontRightWheelTransform;
    [SerializeField] private Transform _frontLeftWheelTransform;
    [SerializeField] private Transform _rearRightWheelTransform;
    [SerializeField] private Transform _rearLeftWheelTransform;

    private void Start()
    {
        player.SetHealth();
        player.maxSpeed = _motorForce * player.speedMultiplier;
        _maxSteeringAngle *= player.control;

        if (_joystickHorizontal == null)
            return;

        if (_joystickVertical == null)
            return;
    }

    private void FixedUpdate()
    {
        GetInput();
        ApplyMotorTorque();
        Steer();
        UpdateWheel();
    }

    private void GetInput()
    {
        _horizontalInput = _joystickHorizontal.Horizontal;
        _verticalInput = _joystickVertical.Vertical;
    }

    private void ApplyMotorTorque()
    {
        _frontRightWheelCollider.motorTorque = player.maxSpeed * _verticalInput;
        _frontLeftWheelCollider.motorTorque = player.maxSpeed * _verticalInput;

        //ApplyBrakes();
    }

    private void ApplyBrakes()
    {
        if (_isBraking)
        {
            _frontRightWheelCollider.brakeTorque = _brakeTorque;
            _frontLeftWheelCollider.brakeTorque = _brakeTorque;
            _rearRightWheelCollider.brakeTorque = _brakeTorque;
            _rearLeftWheelCollider.brakeTorque = _brakeTorque;
        }

        else
        {
            _frontRightWheelCollider.brakeTorque = 0.0f;
            _frontLeftWheelCollider.brakeTorque = 0.0f;
            _rearRightWheelCollider.brakeTorque = 0.0f;
            _rearLeftWheelCollider.brakeTorque = 0.0f;
        }
    }

    private void Steer()
    {
        steeringAngle = _maxSteeringAngle * _horizontalInput;

        _frontRightWheelCollider.steerAngle = steeringAngle;
        _frontLeftWheelCollider.steerAngle = steeringAngle;
    }

    private void UpdateWheel()
    {
        UpdateSingleWheel(_frontLeftWheelCollider, _frontLeftWheelTransform);
        UpdateSingleWheel(_frontRightWheelCollider, _frontRightWheelTransform);
        UpdateSingleWheel(_rearLeftWheelCollider, _rearLeftWheelTransform);
        UpdateSingleWheel(_rearRightWheelCollider, _rearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;

        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);

        wheelTransform.position = wheelPosition;
        wheelTransform.rotation = wheelRotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Pickup"))
        {
            Pickups pickup = other.GetComponent<Pickups>();

            if (pickup.healthPickUp)
            {
                //player = other.gameObject.GetComponent<CarController>();

                if (player.CurrentHealth() < player.maxHealth)
                {
                    player.AddHealth(pickup.health);
                    other.gameObject.SetActive(false);
                }
            }

            else if (pickup.ammoPickUp)
            {
               Gun weapon = this.gameObject.GetComponentInChildren<Gun>();

                if (weapon.weapon.CurrentAmmo() < weapon.weapon.maxAmmo)
                {
                    weapon.weapon.AddAmmo(pickup.ammo);
                    other.gameObject.SetActive(false);
                }
            }
        }
    }
}
