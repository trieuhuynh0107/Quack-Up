using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Duck; // Reference to the Duck GameObject
    public GameObject lifebuoyPrefab; // Reference to the lifebuoy prefab
    private GameObject myLifebuoy; // Reference to the created lifebuoy

    // Store the positions of the created lifebuoys
    private HashSet<Vector2> lifebuoyPositions = new HashSet<Vector2>();

    // Distance between lifebuoys
    public float heightOffset = 100.0f; // Minimum distance between lifebuoys
    private float nextYPosition = 100.0f; // Next Y position for the lifebuoy

    // Limits for random position generation
    public float minX = -5.5f; // Minimum X coordinate for lifebuoys
    public float maxX = 5.5f; // Maximum X coordinate for lifebuoys

    // Maximum number of lifebuoys at each height
    public int maxLifebuoysPerHeight = 5; // Limit for lifebuoys at each height

    // Count of lifebuoys created at each height
    private Dictionary<float, int> lifebuoyCountPerHeight = new Dictionary<float, int>();

    void Start()
    {
        // Initialize the starting Y position
        nextYPosition = 10.0f;
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the number of lifebuoys at this height has reached the limit
        if (lifebuoyCountPerHeight.ContainsKey(nextYPosition) && lifebuoyCountPerHeight[nextYPosition] >= maxLifebuoysPerHeight)
        {
            return; // Do not create more lifebuoys if the limit has been reached
        }

        // Create a lifebuoy at the next height
        float randomX;

        // Ensure the random position does not overlap
        do
        {
            randomX = Random.Range(minX, maxX); // Randomize the X coordinate
        } while (lifebuoyPositions.Contains(new Vector2(randomX, nextYPosition))); // Check if the position already exists

        // Create the position for the lifebuoy
        Vector2 newPosition = new Vector2(randomX, nextYPosition);

        // Add the new position to the set
        lifebuoyPositions.Add(newPosition);

        // Instantiate the lifebuoy at the new position
        myLifebuoy = Instantiate(lifebuoyPrefab, newPosition, Quaternion.identity);

        // Update the count of lifebuoys at this height
        if (lifebuoyCountPerHeight.ContainsKey(nextYPosition))
        {
            lifebuoyCountPerHeight[nextYPosition]++;
        }
        else
        {
            lifebuoyCountPerHeight[nextYPosition] = 1; // Initialize the count for this height
        }

        // Update the Y position for the next lifebuoy
        nextYPosition += heightOffset;

        // Destroy the colliding object
        Destroy(collision.gameObject);
    }
}
