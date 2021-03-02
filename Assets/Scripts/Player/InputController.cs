using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class InputController : MonoBehaviour
{

    [SerializeField] private KeyCode PCDebugKey;
    private MovementController movement;

    // Start is called before the first frame update
    void Start()
    {
        this.movement = GetComponent<MovementController>();
    }


    void FixedUpdate()
    {
        int count = Input.touchCount == 0 ? (Input.GetKey(PCDebugKey) ? 1 : 0) : 1;
        if (count > 0) this.movement.SetX(1);
        else this.movement.SetX(0);
    }
}
