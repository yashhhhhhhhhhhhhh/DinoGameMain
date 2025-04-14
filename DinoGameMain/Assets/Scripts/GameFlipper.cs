using UnityEngine;

public class GameFlipper : MonoBehaviour
{
    private float flipTime = 6f;
    private bool isFlipped = false;

    void Start()
    {
        Invoke("FlipGame", flipTime);
    }

    void FlipGame()
    {
        if (!isFlipped)
        {
            isFlipped = true;

            // Flip the gravity for all objects in the game
            Physics2D.gravity = new Vector2(0, -Physics2D.gravity.y);

            // Flip the camera
            Camera.main.transform.Rotate(0, 0, 180);

            // Flip all objects tagged "Flippable"
            GameObject[] flippableObjects = GameObject.FindGameObjectsWithTag("Flippable");
            foreach (GameObject obj in flippableObjects)
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x, -obj.transform.localScale.y, obj.transform.localScale.z);
            }
        }
    }
}
