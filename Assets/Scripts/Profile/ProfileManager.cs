using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    Game Game;
    public ProfileEntry activeProfile;
    // Start is called before the first frame update
    void Start()
    {
        Game = Game.Get();
        TextAsset jsonProfile = Resources.Load<TextAsset>("profile");
        var jsonDeserialized = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonProfile.ToString());

        activeProfile = new ProfileEntry();
        activeProfile.name = (string)jsonDeserialized["name"];
    }
}
