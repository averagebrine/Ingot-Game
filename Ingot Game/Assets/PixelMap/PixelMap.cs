using Common;
using UnityEngine;

namespace Editor
{
    public class PixelMap : ScriptableObject
    {
        public SerializedDictionary<Color32, Vector2Int> lookup = new SerializedDictionary<Color32, Vector2Int>();
        public Color32[] data;
    }
}