using UnityEngine;
using System.Collections;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CreateBarrierFlowMat : ImageBuilder {

    // Use this for initialization
    protected override void BuildImage(Texture2D target) {
        for (int x = 0; x < target.width; x++) {
            for (int y = 0; y < target.height; y++) {

                Vector2 position = new Vector2(x / (float)target.width, y / (float)target.height);
                position *= 2;
                position -= Vector2.one;
                position.Normalize();
                position += Vector2.one;
                position /= 2;

                Color pixelColor = new Color(position.x, position.y, 0, 1);
                target.SetPixel(x, y, pixelColor);
            }
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(CreateBarrierFlowMat))]
public class CreateBarrierFlowMatEditor : ImageBuilderEditor { }

#endif