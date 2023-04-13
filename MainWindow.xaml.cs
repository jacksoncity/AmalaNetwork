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
            popFilter(races, raceTag);
            popFilter(levels, levelTag);
            popFilter(elements, weakTag);
            popFilter(elements, nullTag);
            popFilter(elements, strongTag);

            gameFilter(titles, gameDropDown);

            //List<Demon> items = null;
            //loadJson();
            

        }

        //LOADING THE JSON TO PARSE FOR FUSION CALC
        public void loadJson()
        {
            using (StreamReader r = new StreamReader("data5.js"))
            {
                string json = r.ReadToEnd();
                List<Demon> items = JsonConvert.DeserializeObject<List<Demon>>(json);

            }
        }

        public List<string> btnNames()
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
        public void popFilter(String[] objArray, TreeViewItem tagName)
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

        public void gameFilter(String[] objArray, ComboBox dropDown)
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
        private void noneBtnClick(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            // Act Accordingly
            switch (btn.Name)
            {
                case "noneBtn":
                    popClear(raceTag);
                    popClear(levelTag);
                    popClear(weakTag);
                    popClear(nullTag);
                    popClear(strongTag);
                    break;

                case "raceBtn":
                    selectAllBtn(raceTag);
                    break;
                case "levelBtn":
                    selectAllBtn(levelTag);
                    break;
                case "weakBtn":
                    selectAllBtn(weakTag);
                    break;
                case "nullBtn":
                    selectAllBtn(nullTag);
                    break;
                case "strongBtn":
                    selectAllBtn(strongTag);
                    break;

                default: 
                    Console.WriteLine("Error on button press"); 
                    break;
            }


        }

        private void selectAllBtn(TreeViewItem item)
        {
            foreach (CheckBox checkbox in item.Items.OfType<CheckBox>())
            {
                checkbox.IsChecked = true;
            }
        }

        private void popClear(TreeViewItem item)
        {
            foreach(CheckBox checkbox in item.Items.OfType<CheckBox>())
            {
                checkbox.IsChecked = false;
            }
        }


        private void panelBtnSort(object sender, RoutedEventArgs e)
        {   
            //get the buttons text content
            string btnName = (e.Source as Button).Content.ToString();
            string[] namesRaw = btnNames().ToArray();
            //check it against all the buttons to see if another critiera is selected (we want to make the text of the
            // button we press to be "text" + ↑ or ↓
            for(int i = 0; i < namesRaw.Length; i++)
            {

                //initially check if its value is default, if so change it
                if(btnName == namesRaw[i])
                {
                    (e.Source as Button).Content = btnName + " ↓";
                    
                    //once changed we must now set all other buttons to default (this goes for the rest of the name check if statements

                }
                //if same name is sorted decending change to accending 
                if(btnName == btnName + " ↓")
                {
                    (e.Source as Button).Content = btnName + " ↑";
                }
                //if same name is sorted to accending change to decending 
                if(btnName == btnName + " ↑")
                {
                    (e.Source as Button).Content = btnName + " ↓";
                }
            }


        }

        //GAME DROPDOWN RESPONSE
        private void gameDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string gameTitle = ((ComboBoxItem)gameDropDown.SelectedItem).Content.ToString();
            gameTitlePanel.Text = gameTitle;
            if ((bool)colorCheck.IsChecked)
            {
                switch (gameTitle)
                {
                    case "Persona 3:FES":
                        midPanel.Background = Brushes.Navy;
                        break;
                    case "Persona 4: Golden":
                        midPanel.Background = Brushes.DarkGoldenrod;
                        break;
                    case "Persona 4":
                        midPanel.Background = Brushes.DarkOliveGreen;
                        break;
                    case "Persona 5":
                        midPanel.Background = Brushes.DarkRed;
                        break;
                    case "Persona 5: Royal":
                        midPanel.Background = Brushes.DarkSalmon;
                        break;
                    case "SMT III: Nocturne":
                        midPanel.Background = Brushes.DarkSeaGreen;
                        break;
                    case "SMT IV":
                        midPanel.Background = Brushes.DarkGreen;
                        break;
                    case "SMT V":
                        midPanel.Background = Brushes.DarkSlateGray;
                        break;
                    default:
                        midPanel.Background = Brushes.Gray;
                        break;
                }
            }
        }
    }
}
