using UnityEngine;
using System.Collections;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ImageBuilder : MonoBehaviour {
    [SerializeField]
    protected int width = 512;
    [SerializeField]
    protected int height = 512;
    [SerializeField]
    protected string fileName = "Output";

    protected abstract void BuildImage(Texture2D target);

    public void CreateImage() {
        Texture2D result = new Texture2D(width, height);
        BuildImage(result);
        result.Apply();

        byte[] bytes = result.EncodeToPNG();
        File.WriteAllBytes(string.Format("{0}/{1}.png", Application.dataPath, fileName), bytes);
    }
}

public class CreateShotgunFlowMat : ImageBuilder {

    // Use this for initialization
    protected override void BuildImage(Texture2D target) {
        for (int x = 0; x < target.width; x++) {
            for (int y = 0; y < target.height; y++) {

                Vector2 position = new Vector2((target.height - 1) - x, y);
                position.Normalize();
                position.y += 1;
                position.x = 1 - position.x;
                position /= 2;

                Color pixelColor = new Color(position.x, position.y, 0, 1);
                target.SetPixel(x, y, pixelColor);
            }
        }
    }
}


#if UNITY_EDITOR

[CustomEditor(typeof(CreateShotgunFlowMat))]
public class CreateShotgunFlowMatEditor : ImageBuilderEditor { }

[CustomEditor(typeof(ImageBuilder))]
public class ImageBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ImageBuilder myScript = (ImageBuilder)target;
        if (GUILayout.Button("Create Image"))
        {
            myScript.CreateImage();
        }
    }
}
#endif