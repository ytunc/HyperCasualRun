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
    public void FixedUpdate()
    {
        transform.position += moveDirection * Time.deltaTime * speedPower;

        if (!_jumping)
        {
            rigidbody.useGravity = false;
            _jumpPower = _jumpPower + Time.deltaTime;
            this.gameObject.transform.position = new Vector3(transform.position.x,
                 _jumpPower, transform.position.z);
        }
        else
        {
            rigidbody.useGravity = true;
            animator.SetBool("IsRunning", true);
        }
        if (leftMove)
        {
            this.gameObject.transform.eulerAngles = -rotationAngle;
            this.gameObject.transform.position = new Vector3(transform.position.x - rotationPower,
                 _jumpPower, transform.position.z);
        }
        if (rightMove)
        {
            this.gameObject.transform.eulerAngles = rotationAngle;
            this.gameObject.transform.position = new Vector3(transform.position.x + rotationPower,
                 _jumpPower, transform.position.z);
        }
        if (!leftMove && !rightMove)
        {
            this.gameObject.transform.eulerAngles = new Vector3(0,0,0);
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
            totalNumberObject.SetTotalNumberObject(1);
            energyBar.SetEnergy(5);
        }
        else if (other.gameObject.CompareTag("punishment"))
        {
            energyBar.SetEnergy(-5);
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
        yield return new WaitForSeconds(1);
        _jumping = true;
        _jumpPower = 0;
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
}
