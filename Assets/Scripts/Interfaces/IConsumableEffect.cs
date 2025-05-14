public interface IConsumableEffect
{
    void ApplyEffect(PlayerController player, MouseLook look);
    void RemoveEffect(PlayerController player, MouseLook look);
}
