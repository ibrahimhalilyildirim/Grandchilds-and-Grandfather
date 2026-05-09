using UnityEngine;

public class Consts : MonoBehaviour
{
    public struct MouseInputs
    {
        public const string MOUSE_X = "Mouse X";
        public const string MOUSE_Y = "Mouse Y";
    }
    public struct PlayerState
    {
        public const string PLAYER_IDLE = "PlayerIdle";
        public const string PLAYER_WALKING = "PlayerWalking";
    }
    public struct PlayerAnimations
    {
        public const string IS_MOVING = "IsMoving";
        public const string IS_JUMPING = "IsJumping";
    }

}
