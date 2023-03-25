using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectableType
{
    Building, Unit
}

public interface ISelectable
{
    SelectableType type { get; }
}
