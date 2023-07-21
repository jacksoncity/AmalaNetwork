// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;
using System.Collections.Generic;

public class DList
{
    public string dName { get; set; }
    public int cardlvl { get; set; }
    public string heart { get; set; }
    public string inherits { get; set; }
    public int lvl { get; set; }
    public string race { get; set; }
    public string resists { get; set; }
    public Skills skills { get; set; }
    public List<int> stats { get; set; }
}

public class Root
{
    public List<DList> dList { get; set; }
}

public class Skills
{
    [JsonProperty("Absorb Strike")]
    public int AbsorbStrike { get; set; }

    [JsonProperty("Evil Smile")]
    public int EvilSmile { get; set; }

    [JsonProperty("High Counter")]
    public int HighCounter { get; set; }

    [JsonProperty("Null Pierce")]
    public int NullPierce { get; set; }

    [JsonProperty("Null Slash")]
    public int NullSlash { get; set; }

    [JsonProperty("Null Strike")]
    public int NullStrike { get; set; }

    [JsonProperty("Weary Thrust")]
    public int WearyThrust { get; set; }
    public int? Mabufudyne { get; set; }
    public int? Makarakarn { get; set; }

    [JsonProperty("Regenerate 3")]
    public int? Regenerate3 { get; set; }

    [JsonProperty("Repel Pierce")]
    public int? RepelPierce { get; set; }

    [JsonProperty("Repel Slash")]
    public int? RepelSlash { get; set; }

    [JsonProperty("Repel Strike")]
    public int? RepelStrike { get; set; }
    public int? Charmdi { get; set; }
    public int? Dia { get; set; }
    public int? Garu { get; set; }

    [JsonProperty("Marin Karin")]
    public int? MarinKarin { get; set; }
    public int? Mudo { get; set; }
    public int? Sukunda { get; set; }
}
