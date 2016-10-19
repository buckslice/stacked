using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class InkEffect : MonoBehaviour {
    public Material InkBleedMaterial;
    RenderTexture previousFrameBuffer;
    void Awake() {
        previousFrameBuffer = new RenderTexture(Screen.width, Screen.height, 1);
        previousFrameBuffer.Create();
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst) {
        RenderTexture temp = RenderTexture.GetTemporary(src.width, src.height, 0);

        InkBleedMaterial.SetTexture("_PrevFrame", previousFrameBuffer);
        Graphics.Blit(src, temp, InkBleedMaterial);
        Graphics.Blit(temp, previousFrameBuffer);
        Graphics.Blit(temp, dst);

        RenderTexture.ReleaseTemporary(temp);
    }
}