﻿using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Diagnostics;
using System.CodeDom;
using static System.Net.Mime.MediaTypeNames;

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
            string[] defultNames = { "Level", "Name", "Race", "Phys", "Gun", "Fire", "Ice", "Elec", "Wind", "Psychic", "Nuclear", "Bless", "Curse" };

            ButtonDefClear();

            //check it against all the buttons to see if another critiera is selected (we want to make the text of the
            // button we press to be "text" + ↑ or ↓
            for (int i = 0; i < defultNames.Length; i++)
            {
                //initially check if its value is default, if so change it
                if (btnName == defultNames[i])
                {
                    /*                    System.Diagnostics.Debug.WriteLine(btnName);
                                        System.Diagnostics.Debug.WriteLine(defultNames[i]);*/

                    (e.Source as Button).Content = defultNames[i] + " ↓";

                    //once changed we must now set all other buttons to default (this goes for the rest of the name check if statements
                }
                //if same name is sorted decending change to accending 
                if (btnName == defultNames[i] + " ↓")
                {
                    (e.Source as Button).Content = defultNames[i] + " ↑";
                }
                //if same name is sorted to accending change to decending 
                if (btnName == defultNames[i] + " ↑")
                {
                    (e.Source as Button).Content = defultNames[i];
                }
            }
        }


        private void LoadJsonData(string name)
        {
            string jsonData = File.ReadAllText("C:\\Users\\Jackson\\Documents\\DESKTOP\\PERSONAL_PROGRAMMING\\AmalaNetwork\\JSONS\\p3fes.json");
            var data = (JObject)JsonConvert.DeserializeObject(jsonData);
            var demons = data["dList"].Children();
            foreach ( var demon in demons )
            {

                //DEMON NAME
                var dName = demon["dName"].Value<string>();
                TextBlock toAddName = columnPopString(dName);               

                //HYPERLINK ACTIVITY
                Run run1 = new Run(dName);
                Hyperlink hyperlink = new Hyperlink(run1)
                {
                    NavigateUri = new Uri("https://www.youtube.com/")
                };
                hyperlink.RequestNavigate += new System.Windows.Navigation.RequestNavigateEventHandler(hyperlink_RequestNavigate);
                hyperlink.Foreground = new SolidColorBrush(Colors.White);
                toAddName.Inlines.Clear();
                toAddName.Inlines.Add(hyperlink);
                //add name
                nameColPop.Items.Add(toAddName);

                //DEMON LEVEL
                var dLvl = demon["lvl"].Value<string>();

                lvlColPop.Items.Add(columnPopString(dLvl));

                //DEMON RACE
                var dRace = demon["race"].Value<string>();

                raceColPop.Items.Add(columnPopString(dRace));

                //DEMON AFFINITIES
                var dAff = demon["resists"];

                string str = (string)dAff;
                //ARRAY OF OUR AFFINITIES
                char[] ch = new char[str.Length];

                for(int i = 0; i < str.Length; i++)
                {
                    ch[i] = str[i];
                }

                //PHYS
                PhysColPop.Items.Add(columnPopChar(ch[1]));

                //GUN
                gunColPop.Items.Add(columnPopChar(ch[2]));

                //FIRE
                fireColPop.Items.Add(columnPopChar(ch[3]));

                //ICE
                iceColPop.Items.Add(columnPopChar(ch[4]));

                //ELEC
                elecColPop.Items.Add(columnPopChar(ch[5]));

                //WIND
                windColPop.Items.Add(columnPopChar(ch[6]));

                //BLESS
                blessColPop.Items.Add(columnPopChar(ch[7]));

                //CURSE
                curseColPop.Items.Add(columnPopChar(ch[8]));
            }
        }

        private TextBlock columnPopString(string txt)
        {
            TextBlock toAdd = new TextBlock();
            toAdd.Text = txt;
            toAdd.HorizontalAlignment = HorizontalAlignment.Center;
            toAdd.FontSize = 18;
            toAdd.Foreground = new SolidColorBrush(Colors.White);
            toAdd.Padding = new Thickness(7);

            return toAdd;
        }

        private TextBlock columnPopChar(char c)
        {
            TextBlock toAdd = new TextBlock();
            string toText = AffinityCheck(c);
            toAdd.HorizontalAlignment = HorizontalAlignment.Center;
            toAdd.FontSize = 18;

            switch (toText)
            {
                case "-":
                    toAdd.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case "Str":
                    toAdd.Foreground = new SolidColorBrush(Colors.Green);
                    break;
                case "Nu":
                    toAdd.Foreground = new SolidColorBrush(Colors.Fuchsia);
                    break;
                case "Wk":
                    toAdd.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case "Rp":
                    toAdd.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
                case "Dr":
                    toAdd.Foreground = new SolidColorBrush(Colors.Cyan);
                    break;
                default:
                    toAdd.Foreground = new SolidColorBrush(Colors.Lime);
                    break;
            }
            toAdd.Text = toText;
            toAdd.Padding = new Thickness(7);

            return toAdd;
        }

        //HELPS EXECUTE THE HYPERLINK OF THE DEMONS NAME
        private void hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri){ UseShellExecute = true});
            e.Handled = true;
        }

        private string AffinityCheck(char affinity)
        {
            string toRet;
            
            switch (affinity)
            {
                case '-':
                    toRet = "-";
                    break;

                case 's':
                    toRet = "Str";
                    break;

                case 'n':
                    toRet = "Nu";
                    break;

                case 'w':
                    toRet = "Wk";
                    break;

                case 'r':
                    toRet = "Rp";
                    break;

                case 'd':
                    toRet = "Dr";
                    break;

                default:
                    toRet = "Err";
                    break;
            }
            
            return toRet;
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
                    //LoadJson(@"..\..\..\JSONS\p3fes.json");
                    //LoadJsonData("p3fes.json");
                    LoadJsonData("test.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.Navy;
                    }
                        break;

                    case "Persona 4: Golden":
                   // LoadJson(@"..\..\..\JSONS\p4g.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkGoldenrod;
                    }
                        break;

                    case "Persona 4":
                    //LoadJson(@"..\..\..\JSONS\p4.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkOliveGreen;
                    }
                        break;

                    case "Persona 5":
                    //LoadJson(@"..\..\..\JSONS\p5.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkRed;
                    }
                        break;

                    case "Persona 5: Royal":
                    //LoadJson(@"..\..\..\JSONS\p5r.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSalmon;
                    }
                        break;

                    case "SMT III: Nocturne":
                    //LoadJson(@"..\..\..\JSONS\smt3.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSeaGreen;
                    }
                        break;

                    case "SMT IV":
                    //LoadJson(@"..\..\..\JSONS\smt4.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkGreen;
                    }
                        break;

                    case "SMT V":
                   // LoadJson(@"..\..\..\JSONS\smt5.json");

                    if (colorCheck.IsChecked == true)
                    {
                        midPanel.Background = Brushes.DarkSlateGray;
                    }
                        break;

                    default:
                    //LoadJson(@"..\..\..\JSONS\def.json");
                        midPanel.Background = Brushes.Gray;

                        break;
            }
        }
    }
}