using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
  public float anglePerFrame = .5f;
  private Vector3 yAxis = new Vector3(0, 1, 0);

  private void Update()
  {
    transform.rotation = Quaternion.AngleAxis(anglePerFrame, yAxis) * transform.rotation;
  }
}
