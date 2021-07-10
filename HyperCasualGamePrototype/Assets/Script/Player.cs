using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private FloorObjectPools floorObjectPools;

    [SerializeField] private float speedPower;
    [SerializeField] private Vector3 moveDirection;
    [SerializeField] private Animator animator;
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private TotalNumberObject totalNumberObject;
    [SerializeField] private UiManager uiManager;

    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private bool _jumping;
    [SerializeField] private float _jumpPower = 1;
    [SerializeField] private bool leftMove, rightMove;

    [SerializeField] private float rotationPower;
    [SerializeField] private Vector3 rotationAngle;

    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Vector3 vector3Joystick;
    [SerializeField] private float joystickSensitivity;

    [SerializeField] private Material defaultRendererMat;
    [SerializeField] private Material punishmentRendererMat;

    [SerializeField] private bool tackle;

    public void FixedUpdate()
    {
        transform.position += moveDirection * Time.deltaTime * speedPower;
        transform.position =new Vector3(transform.position.x +
            (variableJoystick.Horizontal / 3),transform.position.y, this.transform.position.z);

        this.gameObject.transform.eulerAngles = new Vector3(0,variableJoystick.Horizontal * joystickSensitivity, 0);

        
        if (!_jumping)
        {
            rigidbody.useGravity = false;
            _jumpPower = _jumpPower + Time.deltaTime;
            transform.position = new Vector3(transform.position.x,
                 _jumpPower, transform.position.z);
        }
        else if (tackle == false)
        {
            rigidbody.useGravity = true;
            animator.SetBool("IsRunning", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            floorObjectPools.StartCoroutine(nameof(floorObjectPools.Spawn));
        }
        else if (other.gameObject.CompareTag("wall"))
        {
            uiManager.ChangePanel(UiManager.panels.gameOver);
        }
        else if (other.gameObject.CompareTag("prize"))
        {
            other.gameObject.GetComponent<PrizeAndPunishmentController>().StartParticle();
            totalNumberObject.SetTotalNumberObject(1);
            energyBar.SetEnergy(5);
        }
        else if (other.gameObject.CompareTag("punishment"))
        {
            StartCoroutine(nameof(ChangeRendererMaterial));
            energyBar.SetEnergy(-5);
            other.gameObject.SetActive(false);
        }
    }


    public void Dead()
    {
        animator.SetBool("dead", true);
    }

    public void Jump()
    {
        _jumping = false;
        StartCoroutine(nameof(JumpMechanic));
    }

    private IEnumerator JumpMechanic()
    {
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJumping", true);
        yield return new WaitForSeconds(1);
        _jumping = true;
        _jumpPower = 0;
    }

    public void Tackle()
    {
        StartCoroutine(nameof(TackleMechanic));
    }

    private IEnumerator TackleMechanic()
    {
        tackle = true;
        animator.SetBool("IsTackle", true);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsJumping", false);
        yield return new WaitForSeconds(1);
        tackle = false;
        animator.SetBool("IsTackle", false);
        animator.SetBool("IsRunning", true);
    }

    public void StartGame()
    { 
        this.gameObject.transform.position = Vector3.zero;
        animator.SetBool("IsRunning" , true);
        speedPower = 5;
        moveDirection.z = 1;
    }

    public void LeftMoveStart()
    {
        leftMove = true;
    }
    public void LeftMoveEnd()
    {
        leftMove = false;
    }
    public void RightMoveStart()
    {
        rightMove = true;
    }
    public void RightMoveEnd()
    {
        rightMove = false;
    }

    private IEnumerator ChangeRendererMaterial()
    {
        RenderSettings.skybox = punishmentRendererMat;
        yield return new WaitForSeconds(1);
        RenderSettings.skybox = defaultRendererMat;
    }
}
