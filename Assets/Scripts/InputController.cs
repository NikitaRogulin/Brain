using UnityEngine;

public class InputController : MonoBehaviour
{
    private Player player;

    public void Setup(Player player)
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
            player.Shoot();
        }

    }
    private Vector3 SearchPositionMouse()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePos;
    }
}
