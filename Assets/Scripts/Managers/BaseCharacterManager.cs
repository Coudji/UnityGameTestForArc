using UnityEngine;

public abstract class BaseCharacterManager : OwnerBehaviour
{
    protected CharacterManager _character;
    private bool _isInitialized = false;

    public virtual void Init(CharacterManager character)
    {
        _character = character;
        _isInitialized = true;
    }

    public virtual void Start()
    {
        if (!_isInitialized)
        {
            Debug.LogError(
                $"{GetType().Name} not initialized. Call Init() with a Character instance."
            );
        }
    }
}
