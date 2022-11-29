using UnityEngine;

public class InputController : MonoBehaviour
{
    private Solder player;

    public void Setup(Solder player)
    {
        this.player = player;
    }
    private void Update()
    {
        var direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0);

        player.Movement.UpdateDirection(direction);
        player.Rotation.UpdateRotation(SearchPositionMouse());

        if (Input.GetMouseButtonDown(0))
        {
            player.Gun.Shoot(player.Rotation.direction);
        }

    }
    private Vector3 SearchPositionMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }
}
