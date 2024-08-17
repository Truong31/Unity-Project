using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }
    public float duration;
    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    // Kích hoạt đối tượng trong khoảng thời gian đã được định sẵn
    public void Enable()
    {
        Enable(this.duration);
    }

    // Kích hoạt đối tượng trong khoảng thời gian cụ thể, sẽ bị vô hiệu hóa khi hết thời gian
    public virtual void Enable(float duration)
    {
        this.enabled = true;
        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }
    public virtual void Disable()
    {
        this.enabled = false;
        CancelInvoke();
    }
}
