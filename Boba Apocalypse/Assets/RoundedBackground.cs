using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class RoundedBackground : MonoBehaviour
{
    public Color backgroundColor = new Color(0.1f, 0.1f, 0.2f, 0.9f); // Dark purple-ish
    public float cornerRadius = 20f;

    void Start()
    {
        CreateRoundedBackground();
    }

    void CreateRoundedBackground()
    {
        Image image = GetComponent<Image>();

        // Create a rounded rectangle texture
        int width = 400;
        int height = 120;
        Texture2D texture = new Texture2D(width, height);

        // Fill with rounded rectangle
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                bool isInside = IsInsideRoundedRect(x, y, width, height, cornerRadius);
                texture.SetPixel(x, y, isInside ? Color.white : Color.clear);
            }
        }

        texture.Apply();

        // Create sprite from texture
        Sprite sprite = Sprite.Create(
            texture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            100,
            0,
            SpriteMeshType.FullRect,
            new Vector4(cornerRadius, cornerRadius, cornerRadius, cornerRadius)
        );

        image.sprite = sprite;
        image.type = Image.Type.Sliced;
        image.color = backgroundColor;
    }

    bool IsInsideRoundedRect(int x, int y, int width, int height, float radius)
    {
        // Check if point is in corner regions
        bool inLeftCorner = x < radius;
        bool inRightCorner = x >= width - radius;
        bool inTopCorner = y >= height - radius;
        bool inBottomCorner = y < radius;

        // Bottom-left corner
        if (inLeftCorner && inBottomCorner)
        {
            float dx = radius - x;
            float dy = radius - y;
            return (dx * dx + dy * dy) <= (radius * radius);
        }
        // Bottom-right corner
        else if (inRightCorner && inBottomCorner)
        {
            float dx = x - (width - radius);
            float dy = radius - y;
            return (dx * dx + dy * dy) <= (radius * radius);
        }
        // Top-left corner
        else if (inLeftCorner && inTopCorner)
        {
            float dx = radius - x;
            float dy = y - (height - radius);
            return (dx * dx + dy * dy) <= (radius * radius);
        }
        // Top-right corner
        else if (inRightCorner && inTopCorner)
        {
            float dx = x - (width - radius);
            float dy = y - (height - radius);
            return (dx * dx + dy * dy) <= (radius * radius);
        }

        // Not in corner region, so it's inside
        return true;
    }
}