using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player _player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        _player.AnimationTrigger();
    }
}
