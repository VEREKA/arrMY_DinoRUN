using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public float tileWidth = 30.09375f;

    private void Update()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.State != GameState.Playing)
            return;

        float speed = SpeedManager.Instance != null ? SpeedManager.Instance.CurrentSpeed : 5f;
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -tileWidth)
        {
            transform.position += new Vector3(tileWidth * 2f, 0f, 0f);
        }
    }
}
