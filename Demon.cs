using System.Collections.Generic;
using System.Windows.Documents;
using SMTFusionappGrid;

namespace SMTFusionappGrid
{
    // THIS FILE HOLD ALL CLASSES THAT WILL BE USED TO READ AND DISPLAY THE JSON
    // FILES THAT ARE USED IN THE MAIN FILE 
    public class Game
    {
        public string Name { get; set; }
        public List<Persona> Personas { get; set; }
    }
    public class Persona
    {
        public string Race { get; set; }
        public int Level { get; set; }
        public bool isMax { get; set; }
        public string UnitName { get; set; }
        public bool isSpecial { get; set; }
    }
}