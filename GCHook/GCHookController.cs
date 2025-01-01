using UnityEngine;

namespace GCHook
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class GCHookController : MonoBehaviour
    {
        public static GCHookController Instance { get; private set; }
    }
}
