using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDataProvider<T> : ScriptableObject
{
  public abstract T Get();
}
