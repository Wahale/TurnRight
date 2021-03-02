using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    public Action<Collision2D> OnCollisionEnter2DAction;
    public Action<Collider2D> OnTriggerEnter2DAction;


    private void OnCollisionEnter2D(Collision2D collision) => OnCollisionEnter2DAction?.Invoke(collision);
    private void OnTriggerEnter2D(Collider2D collider) => OnTriggerEnter2DAction?.Invoke(collider);

}
