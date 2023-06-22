using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace SMTFusionappGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] titles = { "Persona 3:FES", "Persona 4", "Persona 4: Golden", "Persona 5", 
                "Persona 5: Royal", "SMT III: Nocturne", "SMT IV", "SMT V" };

            string[] races = { "Avatar", "Avian", "Beast", "Brute",
            "Deity", "Divine", "Dragon", "Drake", "Element", "Fairy",
            "Fallen", "Femme", "Foul", "Fury", "Genma", "Haunt", "Holy",
            "Jaki", "Jirae", "Kishin", "Kunitsu", "Lady", "Night", "Raptor",
            "Herald", "Snake", "Tyrant", "Vile", "Wargod", "Wilder", "Yoma",
            "Megami", "Fiend"};

            string[] levels = {"1-10", "11-20", "21-30", "31-40", "41-50", "51-60",
                "61-70", "71-80", "81-90", "91-100" };

            string[] elements = {"Phys", "Fire", "Force", "Ice", "Electic", "Light"
            , "Dark", "Ailment"};


            //Populating Filter Tree
            PopFilter(races, raceTag);
            PopFilter(levels, levelTag);
            PopFilter(elements, weakTag);
            PopFilter(elements, nullTag);
            PopFilter(elements, strongTag);

            GameFilter(titles, gameDropDown);

        }

        //LOADING THE JSON TO PARSE FOR FUSION CALC
        public List<Game> LoadJson(string gameJson)
        {
            using StreamReader reader = new(gameJson);
            var json = reader.ReadToEnd();
            List<Game> games = JsonConvert.DeserializeObject<List<Game>>(json);

            return games;
        }

        public List<string> BtnNames()
        {
            List<string> btnList = new List<string>
            {
                levelSort.Content.ToString(),
                nameSort.Content.ToString(),
                raceSort.Content.ToString(),
                physSort.Content.ToString(),
                gunSort.Content.ToString(),
                fireSort.Content.ToString(),
                iceSort.Content.ToString(),
                elecSort.Content.ToString(),
                windSort.Content.ToString(),
                psychicSort.Content.ToString(),
                nuclearSort.Content.ToString(),
                blessSort.Content.ToString(),
                curseSort.Content.ToString()
            };

            return btnList;
        }

        //Method to populate a tree with Checkbox items and games dropdown menu
        public void PopFilter(String[] objArray, TreeViewItem tagName)
        {
            //rest of content
            for (int i = 0; i < objArray.Length; i++)
            {
                var checkboxed = new CheckBox();
                checkboxed.Content = objArray[i];
                checkboxed.IsChecked = true;
                checkboxed.Margin = new Thickness(0, 3, 0, 3);
                checkboxed.Focusable = false;
                checkboxed.Foreground = Brushes.White;
                tagName.Items.Add(checkboxed);
            }
        }

        public static void GameFilter(String[] objArray, ComboBox dropDown)
        {
            for (int i = 0; i < objArray.Length; i++)
            {
                var toAdd = new ComboBoxItem();
                toAdd.Content = objArray[i];
                dropDown.Items.Add(toAdd);
            }

        }


        //SIDEBAR BUTTON PROPERTIES

        // NONE BUTTON EVENT HANDLER
        private void FilterHandler(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // clear all checks based off 
            switch (btn.Name)
            {
                case "noneBtn":
                    PopClear(raceTag);
                    PopClear(levelTag);
                    PopClear(weakTag);
                    PopClear(nullTag);
                    PopClear(strongTag);
                    break;
                case "checkAllBtn":
                    SelectAllBtn(raceTag);
                    SelectAllBtn(levelTag);
                    SelectAllBtn(weakTag);
                    SelectAllBtn(nullTag);
                    SelectAllBtn(strongTag);
                    break;
                case "raceBtn":
                    SelectAllBtn(raceTag);
                    break;
                case "levelBtn":
                    SelectAllBtn(levelTag);
                    break;
                case "weakBtn":
                    SelectAllBtn(weakTag);
                    break;
                case "nullBtn":
                    SelectAllBtn(nullTag);
                    break;
                case "strongBtn":
                    SelectAllBtn(strongTag);
                    break;

                default: 
                    Console.WriteLine("Error on button press"); 
                    break;
            }
        }


        private void SelectAllBtn(TreeViewItem item)
        {
            foreach (CheckBox checkbox in item.Items.OfType<CheckBox>())
            {
                checkbox.IsChecked = true;
            }
        }

        private void PopClear(TreeViewItem item)
        {
            foreach(CheckBox checkbox in item.Items.OfType<CheckBox>())
            {
                checkbox.IsChecked = false;
            }
        }

        //brute forced this a bit. theres probably a better way to do it but right now i just need it to work lol
        private void ButtonDefClear()
        {
            string[] defultNames = { "Level", "Name", "Race", "Phys", "Gun", "Fire", "Ice", "Elec", "Wind", "Psychic", "Nuclear", "Bless", "Curse" };
            levelSort.Content = defultNames[0];
            nameSort.Content = defultNames[1];
            raceSort.Content = defultNames[2];
            physSort.Content = defultNames[3];  
            gunSort.Content = defultNames[4];
            fireSort.Content = defultNames[5];
            iceSort.Content = defultNames[6];   
            elecSort.Content = defultNames[7];
            windSort.Content = defultNames[8];
            psychicSort.Content = defultNames[9];
            nuclearSort.Content = defultNames[10];
            blessSort.Content = defultNames[11];
            curseSort.Content = defultNames[12];
        }

        private void PanelBtnSort(object sender, RoutedEventArgs e)
        {   
            //get the buttons text content
            string btnName = (e.Source as Button).Content.ToString();
            string[] defultNames = {"Level", "Name", "Race", "Phys", "Gun", "Fire", "Ice", "Elec", "Wind", "Psychic", "Nuclear", "Bless", "Curse"};

            ButtonDefClear();

            //check it against all the buttons to see if another critiera is selected (we want to make the text of the
            // button we press to be "text" + ↑ or ↓
            for(int i = 0; i < defultNames.Length; i++)
            {
                //initially check if its value is default, if so change it
                if(btnName == defultNames[i])
                {
/*                    System.Diagnostics.Debug.WriteLine(btnName);
                    System.Diagnostics.Debug.WriteLine(defultNames[i]);*/

                    (e.Source as Button).Content = defultNames[i] + " ↓";
                    
                    //once changed we must now set all other buttons to default (this goes for the rest of the name check if statements
                }
                //if same name is sorted decending change to accending 
                if(btnName == defultNames[i] + " ↓")
                {
                    (e.Source as Button).Content = defultNames[i] + " ↑";
                }
                //if same name is sorted to accending change to decending 
                if(btnName == defultNames[i] + " ↑")
                {
                    (e.Source as Button).Content = defultNames[i];
                }
            }
        }

        //GAME DROPDOWN RESPONSE
        private void GameDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string gameTitle = (gameDropDown.SelectedItem as ComboBoxItem).Content.ToString();
            gameTitlePanel.Text = gameTitle;
                switch (gameTitle)
                {
                    case "Persona 3:FES":
                    LoadJson(@"..\..\..\JSONS\p3fes.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.Navy;
                    }
                        break;

                    case "Persona 4: Golden":
                    LoadJson(@"..\..\..\JSONS\p4g.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkGoldenrod;
                    }
                        break;

                    case "Persona 4":
                    LoadJson(@"..\..\..\JSONS\p4.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkOliveGreen;
                    }
                        break;

                    case "Persona 5":
                    LoadJson(@"..\..\..\JSONS\p5.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkRed;
                    }
                        break;

                    case "Persona 5: Royal":
                    LoadJson(@"..\..\..\JSONS\p5r.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSalmon;
                    }
                        break;

                    case "SMT III: Nocturne":
                    LoadJson(@"..\..\..\JSONS\smt3.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSeaGreen;
                    }
                        break;

                    case "SMT IV":
                    LoadJson(@"..\..\..\JSONS\smt4.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkGreen;
                    }
                        break;

                    case "SMT V":
                    LoadJson(@"..\..\..\JSONS\smt5.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSlateGray;
                    }
                        break;

                    default:
                    LoadJson(@"..\..\..\JSONS\def.json");
                        midPanel.Background = Brushes.Gray;

                        break;
            }
        }
    }
}