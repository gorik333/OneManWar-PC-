public class GlobalDefVals
{
    #region Default values

    public const float PLAYER_MAX_MOVE_SPEED = 3.7f; // default 3.7f
    public const float PLAYER_JUMP_FORCE = 4.3f; // default 4.3f
    public const float FIRE_RATE = 1f; // default 1
    public const float MAX_HIT_POINTS = 100f; // default 100
    public const float PLAYER_RELOAD_TIME = 2.3f; // default 2.3
    public const float DISAPPEARANCE_DELAY = 2f;
    public const float RESPAWN_DELAY = 2f;
    public const float JUMP_OFFSET = 0.2f;
    public const float PLAYER_TIME_BETWEEN_SHOTS = 0.2f;
    public const float PLAYER_MAX_BULLET_SPREAD_ANGLE = 3f; // default 3
    public const float CHANGE_AMMO_TYPE_DELAY = 1.1f;

    public const float COMMON_BULLET_LIFETIME = 0.265f;
    public const float STRENGTHENED_BULLET_LIFETIME = 0.245f;

    public const float ENEMY_RELOAD_TIME = 2.3f;
    public const float ENEMY_TIME_BETWEEN_SHOTS = 0.4f; // default 0.4
    public const float ENEMY_MAX_BULLET_SPREAD_ANGLE = 2.3f;
    public const int ENEMY_TIME_TO_REVERT = 3;
    public const float ENEMY_MOVE_SPEED = 1.2f;
    public const float ENEMY_DETECT_BOOST_MULTIPLIER = 2.5f;
    public const float ENEMY_DETECTION_DISTANCE = 17f;

    public const float MOVE_SPEED_TO_RUN_ANIM = 0.12f;

    public const int HEADSHOT_SPAWN_SPREAD = 25;
    public const int COMMON_BULLET_SPEED = 155;
    public const int STRENGTHENED_BULLET_SPEED = 170;
    public const int TOTAL_COMMON_AMMO = 108;
    public const int TOTAL_STRENGTHENED_AMMO = 108;
    public const float PLAYER_DAMAGE = 12f; // default 12
    public const int MAGAZINE_CAPACITY = 27; // default 27

    public const int START_SKILLPOINT_CHANCE = 5; // default 5
    public const float JUMP_PLATFORM_FORCE = 10.6f;

    public const int STRENGTHENED_AMMO_DECREASE = 30;

    public const int COMMON_AMMO_PRICE = 1;
    public const int STRENGTHENED_AMMO_PRICE = 4;

    public const float SHAKE_INTENSITY = 0.105f;
    public const float SHAKE_DURATION = 0.1f;


    public static readonly int[] HEAL_PRICE_ARRAY = { 18, 63, 105, 180 };
    public static readonly int[] SHIELD_PRICE_ARRAY = { 75, 150, 200, 300, 500 };

    public static readonly float[] SHIELD_DURABILITY_ARRAY = { 25, 50, 80, 125, 165 }; // 25, 45, 65, 85, 100 - default

    #endregion
}
