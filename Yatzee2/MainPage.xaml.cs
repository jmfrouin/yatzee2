using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Yatzee2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Data Members
        int[] Dices;
        bool[] Lock;
        bool ShootAgain;
        int[,] Games;
        bool[,] Played;
        int Nb;
        int Total;
        int Ascending;
        int Descending;
        bool Fix;
        Random rndNumbers;
        int NormalSmallScore;
        int NormalBiggerScore;
        int NormalSubTotal1;
        int NormalSubTotal2;
        int NormalTotal;
        int NormalBonus;
        int AscendingSmallScore;
        int AscendingBiggerScore;
        int AscendingSubTotal1;
        int AscendingSubTotal2;
        int AscendingTotal;
        int AscendingBonus;
        int DescendingSmallScore;
        int DescendingBiggerScore;
        int DescendingSubTotal1;
        int DescendingSubTotal2;
        int DescendingTotal;
        int DescendingBonus;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            Dices = new int[5];
            Lock = new bool[5];
            Games = new int[5, 45];
            Played = new bool[3, 13];
            Nb = 3;
            Total = 39;
            Ascending = 0;
            Descending = 12;
            ShootAgain = true;
            rndNumbers = new Random();
            //Set Background colors
            //BackColor = Color.Lavender;
        }

        #region SelectXDiceImage
        private ImageSource SelectRDicesImage(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice1R.png"));
                case 2:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice2R.png"));
                case 3:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice3R.png"));
                case 4:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice4R.png"));
                case 5:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice5R.png"));
                default:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice6R.png"));
            }
        }

        private ImageSource SelectDicesImage(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice1B.png"));
                case 2:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice2B.png"));
                case 3:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice3B.png"));
                case 4:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice4B.png"));
                case 5:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice5B.png"));
                default:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice6B.png"));
            }
        }

        private ImageSource SelectSmallDicesImage(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice1S.png"));
                case 2:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice2S.png"));
                case 3:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice3S.png"));
                case 4:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice4S.png"));
                case 5:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice5S.png"));
                default:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice6S.png"));
            }
        }
        #endregion

        private void Roll_Dices_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Nb > 0)
            {
                Fix = true;
                ShootAgain = true;
                Nb--;
                if (Nb == 0)
                {
                    this.Roll_Dices.IsEnabled = false;
                    //LaunchDicesButton.Hide();
                    Fix = false;
                }
            }

            //Launch dices
            int dicesTotal = 0;
            for (int i = 0; i < 5; i++)
            {
                if (!Lock[i])
                {
                    Dices[i] = (rndNumbers.Next() % 6) + 1;
                }
                dicesTotal += Dices[i];
            }

            //Display dices
            if(!Lock[0])
                this.picDice1.Source = SelectDicesImage(Dices[0]);
            if (!Lock[1]) 
                this.picDice2.Source = SelectDicesImage(Dices[1]);
            if (!Lock[2])
                this.picDice3.Source = SelectDicesImage(Dices[2]);
            if (!Lock[3]) 
                this.picDice4.Source = SelectDicesImage(Dices[3]);
            if (!Lock[4]) 
                this.picDice5.Source = SelectDicesImage(Dices[4]);

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            //Update labels
            this.labelRemainingLaunch.Text = loader.GetString("Remaining") + Nb.ToString();
            this.labelDicesTotal.Text = "Total : " + dicesTotal;
        }

        private void picDice1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Fix)
            {
                Lock[0] = !Lock[0];
                this.picDice1.Source = SelectRDicesImage(Dices[0]);
            }
        }

        private void picDice2_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Fix)
            {
                Lock[1] = !Lock[1];
                this.picDice2.Source = SelectRDicesImage(Dices[1]);
            }
        }

        private void picDice3_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Fix)
            {
                Lock[2] = !Lock[2];
                this.picDice3.Source = SelectRDicesImage(Dices[2]);
            }
        }

        private void picDice4_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Fix)
            {
                Lock[3] = !Lock[3];
                this.picDice4.Source = SelectRDicesImage(Dices[3]);
            }
        }

        private void picDice5_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (Fix)
            {
                Lock[4] = !Lock[4];
                this.picDice5.Source = SelectRDicesImage(Dices[4]);
            }
        }

        private void FinalizeATurn()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            Nb = 3;
            Total--;
            if (Total == 0)
            {
                //MessageBox.Show("!!\nPlease click on New game.", "Attention");
                /*
                
                // Create the message dialog and set its content
                var messageDialog = new Windows.UI.Popups.MessageDialog(loader.GetString("PartyIsOver"));

                // Add commands and set their callbacks; both buttons use the same callback function instead of inline event handlers
                //messageDialog.Commands.Add(new UICommand("Close", null)));

                // Set the command that will be invoked by default
                messageDialog.DefaultCommandIndex = 0;

                // Set the command to be invoked when escape is pressed
                messageDialog.CancelCommandIndex = 1;

                // Show the message dialog
                await messageDialog.ShowAsync();*/

                //return;
            }

            for (int i = 0; i < 5; i++)
            {
                Lock[i] = false;
            }

            if (NormalSubTotal1 >= 40)
            {
                NormalBonus = 20;
            }

            if (AscendingSubTotal1 >= 40)
            {
                AscendingBonus = 20;
            }

            if (DescendingSubTotal1 >= 40)
            {
                DescendingBonus = 20;
            }

            NormalTotal = NormalSubTotal1 + NormalSubTotal2 + NormalBonus;
            AscendingTotal = AscendingSubTotal1 + AscendingSubTotal2 + AscendingBonus;
            DescendingTotal = DescendingSubTotal1 + DescendingSubTotal2 + DescendingBonus;

            //Update scores
            textNormalSubTotal1.Text = loader.GetString("SubTotal") + " : " + NormalSubTotal1.ToString();
            textNormalSubTotal2.Text = loader.GetString("SubTotal") + " : " + NormalSubTotal2.ToString();
            textNormalBonus.Text = loader.GetString("Bonus") + " : " + NormalBonus.ToString();
            textNormalTotal.Text = loader.GetString("WholeTotal") + " : " + NormalTotal.ToString();

            textAscendingSubTotal1.Text = loader.GetString("SubTotal") + " : " + AscendingSubTotal1.ToString();
            textAscendingSubTotal2.Text = loader.GetString("SubTotal") + " : " + AscendingSubTotal2.ToString();
            textAscendingBonus.Text = loader.GetString("Bonus") + " : " + AscendingBonus.ToString();
            textAscendingTotal.Text = loader.GetString("WholeTotal") + " : " + AscendingTotal.ToString();

            textDescendingSubTotal1.Text = "SubTotal : " + DescendingSubTotal1.ToString();
            textDescendingSubTotal2.Text = "SubTotal : " + DescendingSubTotal2.ToString();
            textDescendingBonus.Text = "Bonus : " + DescendingBonus.ToString();
            textDescendingTotal.Text = "Total : " + DescendingTotal.ToString();

            int BigTotal = NormalTotal + AscendingTotal + DescendingTotal;
            textGrandTotal.Text = "Score = " + BigTotal.ToString();

            this.Roll_Dices.IsEnabled = true;

            //Reset dices
            /*picDice1.Image = Properties.Resources.BBlank;
            picDice2.Image = Properties.Resources.BBlank;
            picDice3.Image = Properties.Resources.BBlank;
            picDice4.Image = Properties.Resources.BBlank;
            picDice5.Image = Properties.Resources.BBlank;*/

            //Reset infos
            this.labelRemainingLaunch.Text = "";
            this.labelDicesTotal.Text = "";
        }

        #region ScoreNormalOnes
        private void ScoreNormalOnes()
        {
            if (Played[0, 0]) return;

            this.Dice11.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice12.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice13.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice14.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice15.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 1 ? 1 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalOne.Text = loader.GetString("TheOnes") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 0] = true;
        }

        private void Dice11_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalOnes();
        }

        private void Dice12_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalOnes();
        }

        private void Dice13_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalOnes();
        }

        private void Dice14_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalOnes();
        }

        private void Dice15_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalOnes();
        }
        #endregion

        #region ScoreDescOnes
        private void ScoreDescOnes()
        {
            if (Played[1, 0]) return;
            if (Descending != 0) return;
            Descending++;

            this.Dice141.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice142.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice143.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice144.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice145.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 1 ? 1 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescOne.Text = loader.GetString("TheOnes") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 0] = true;
        }

        private void Dice141_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescOnes();
        }

        private void Dice142_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescOnes();
        }

        private void Dice143_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescOnes();
        }

        private void Dice144_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescOnes();
        }

        private void Dice145_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescOnes();
        }
        #endregion

        #region ScoreAscOnes
        private void ScoreAscOnes()
        {
            if (Played[2, 0]) return;
            if (Ascending != 0) return;

            this.Dice271.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice272.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice273.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice274.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice275.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 1 ? 1 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscOne.Text = loader.GetString("TheOnes") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 0] = true;
        }

        private void Dice271_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscOnes();
        }

        private void Dice272_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscOnes();
        }

        private void Dice273_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscOnes();
        }

        private void Dice274_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscOnes();
        }

        private void Dice275_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscOnes();
        }
        #endregion


        #region ScoreNormalTwos
        private void ScoreNormalTwos()
        {
            if (Played[0, 1]) return;

            this.Dice21.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice22.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice23.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice24.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice25.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 2 ? 2 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalTwo.Text = loader.GetString("TheTwos") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 1] = true;
        }

        private void Dice21_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalTwos();
        }

        private void Dice22_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalTwos();
        }

        private void Dice23_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalTwos();
        }

        private void Dice24_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalTwos();
        }

        private void Dice25_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalTwos();
        }
        #endregion

        #region ScoreDescTwos
        private void ScoreDescTwos()
        {
            if (Played[1, 1]) return;
            if (Descending != 1) return;
            Descending++;

            this.Dice151.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice152.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice153.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice154.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice155.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 2 ? 2 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescTwo.Text = loader.GetString("TheTwos") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 1] = true;
        }

        private void Dice151_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescTwos();
        }

        private void Dice152_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescTwos();
        }

        private void Dice153_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescTwos();
        }

        private void Dice154_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescTwos();
        }

        private void Dice155_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescTwos();
        }
        #endregion

        #region ScoreAscTwos
        private void ScoreAscTwos()
        {
            if (Played[2, 1]) return;
            if (Ascending != 1) return;
            Ascending--;

            this.Dice281.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice282.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice283.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice284.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice285.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 2 ? 2 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscTwo.Text = loader.GetString("TheTwos") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 1] = true;
        }

        private void Dice281_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscTwos();
        }

        private void Dice282_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscTwos();
        }

        private void Dice283_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscTwos();
        }

        private void Dice284_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscTwos();
        }

        private void Dice285_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscTwos();
        }
        #endregion


        #region ScoreNormalThrees
        private void ScoreNormalThrees()
        {
            if (Played[0, 2]) return;

            this.Dice31.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice32.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice33.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice34.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice35.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 3 ? 3 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalThree.Text = loader.GetString("TheThrees") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 2] = true;
        }

        private void Dice31_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalThrees();
        }

        private void Dice32_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalThrees();
        }

        private void Dice33_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalThrees();
        }

        private void Dice34_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalThrees();
        }

        private void Dice35_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalThrees();
        }
        #endregion

        #region ScoreDescThrees
        private void ScoreDescThrees()
        {
            if (Played[1, 2]) return;
            if (Descending != 2) return;
            Descending++;

            this.Dice161.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice162.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice163.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice164.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice165.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 3 ? 3 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescThree.Text = loader.GetString("TheThrees") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 2] = true;
        }

        private void Dice161_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescThrees();
        }

        private void Dice162_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescThrees();
        }

        private void Dice163_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescThrees();
        }

        private void Dice164_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescThrees();
        }

        private void Dice165_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescThrees();
        }
        #endregion

        #region ScoreAscThrees
        private void ScoreAscThrees()
        {
            if (Played[2, 2]) return;
            if (Ascending != 2) return;
            Ascending--;

            this.Dice291.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice292.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice293.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice294.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice295.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 3 ? 3 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscThree.Text = loader.GetString("TheThrees") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 2] = true;
        }

        private void Dice291_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscThrees();
        }

        private void Dice292_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscThrees();
        }

        private void Dice293_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscThrees();
        }

        private void Dice294_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscThrees();
        }

        private void Dice295_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscThrees();
        }
        #endregion
        

        #region ScoreNormalFours
        private void ScoreNormalFours()
        {
            if (Played[0, 3]) return;

            this.Dice41.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice42.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice43.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice44.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice45.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 4 ? 4 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalFour.Text = loader.GetString("TheFours") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 3] = true;
        }

        private void Dice41_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFours();
        }

        private void Dice42_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFours();
        }

        private void Dice43_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFours();
        }

        private void Dice44_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFours();
        }

        private void Dice45_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFours();
        }
        #endregion

        #region ScoreDescFours
        private void ScoreDescFours()
        {
            if (Played[1, 3]) return;
            if (Descending != 3) return;
            Descending++;

            this.Dice171.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice172.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice173.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice174.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice175.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 4 ? 4 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescFour.Text = loader.GetString("TheFours") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 3] = true;
        }

        private void Dice171_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFours();
        }

        private void Dice172_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFours();
        }

        private void Dice173_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFours();
        }

        private void Dice174_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFours();
        }

        private void Dice175_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFours();
        }
        #endregion

        #region ScoreAscFours
        private void ScoreAscFours()
        {
            if (Played[2, 3]) return;
            if (Ascending != 3) return;
            Ascending--;

            this.Dice301.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice302.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice303.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice304.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice305.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 4 ? 4 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscFour.Text = loader.GetString("TheFours") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 3] = true;
        }

        private void Dice301_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFours();
        }

        private void Dice302_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFours();
        }

        private void Dice303_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFours();
        }

        private void Dice304_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFours();
        }

        private void Dice305_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFours();
        }
        #endregion
        

        #region ScoreNormalFives
        private void ScoreNormalFives()
        {
            if (Played[0, 4]) return;

            this.Dice51.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice52.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice53.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice54.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice55.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 5 ? 5 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalFive.Text = loader.GetString("TheFives") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 4] = true;
        }

        private void Dice51_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice52_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice53_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice54_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice55_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }
        #endregion

        #region ScoreDescFives
        private void ScoreDescFives()
        {
            if (Played[1, 4]) return;
            if (Descending != 4) return;
            Descending++;

            this.Dice181.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice182.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice183.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice184.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice185.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 5 ? 5 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescFive.Text = loader.GetString("TheFives") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 4] = true;
        }

        private void Dice181_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFives();
        }

        private void Dice182_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFives();
        }

        private void Dice183_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFives();
        }

        private void Dice184_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFives();
        }

        private void Dice185_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFives();
        }
        #endregion

        #region ScoreAscFives
        private void ScoreAscFives()
        {
            if (Played[2, 4]) return;
            if (Ascending != 4) return;
            Ascending--;

            this.Dice311.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice312.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice313.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice314.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice315.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 5 ? 5 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscFive.Text = loader.GetString("TheFives") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 4] = true;
        }

        private void Dice311_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFives();
        }

        private void Dice312_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFives();
        }

        private void Dice313_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFives();
        }

        private void Dice314_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFives();
        }

        private void Dice315_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFives();
        }
        #endregion
        

        #region ScoreNormalSixs
        private void ScoreNormalSixs()
        {
            if (Played[0, 5]) return;

            this.Dice61.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice62.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice63.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice64.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice65.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 6 ? 6 : 0;
            }
            NormalSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalSix.Text = loader.GetString("TheSixs") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 5] = true;
        }

        private void Dice61_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice62_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice63_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice64_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }

        private void Dice65_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFives();
        }
        #endregion

        #region ScoreDescSixs
        private void ScoreDescSixs()
        {
            if (Played[1, 5]) return;
            if (Descending != 5) return;
            Descending++;

            this.Dice191.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice192.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice193.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice194.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice195.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 6 ? 6 : 0;
            }
            DescendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescSix.Text = loader.GetString("TheSixs") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 5] = true;
        }

        private void Dice191_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSixs();
        }

        private void Dice192_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSixs();
        }

        private void Dice193_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSixs();
        }

        private void Dice194_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSixs();
        }

        private void Dice195_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSixs();
        }
        #endregion

        #region ScoreAscSixs
        private void ScoreAscSixs()
        {
            if (Played[2, 5]) return;
            if (Ascending != 5) return;
            Ascending--;

            this.Dice321.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice322.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice323.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice324.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice325.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i] == 6 ? 6 : 0;
            }
            AscendingSubTotal1 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscSix.Text = loader.GetString("TheSixs") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 5] = true;
        }

        private void Dice321_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSixs();
        }

        private void Dice322_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSixs();
        }

        private void Dice323_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSixs();
        }

        private void Dice324_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSixs();
        }

        private void Dice325_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSixs();
        }
        #endregion


        #region ScoreNormalLess11
        private void ScoreNormalLess11()
        {
            if (Played[0, 6]) return;

            this.Dice71.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice72.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice73.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice74.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice75.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }
            if (Temp < 11)
            {
                Temp = 20;
            }
            else 
            {
                Temp = 0;
            }
            NormalSubTotal2 += Temp;
            
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalLess11.Text = loader.GetString("Less11") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 6] = true;
        }

        private void Dice71_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalLess11();
        }

        private void Dice72_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalLess11();
        }

        private void Dice73_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalLess11();
        }

        private void Dice74_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalLess11();
        }

        private void Dice75_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalLess11();
        }
        #endregion

        #region ScoreDescLess11
        private void ScoreDescLess11()
        {
            if (Played[1, 6]) return;
            if (Descending != 6) return;
            Descending++;

            this.Dice201.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice202.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice203.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice204.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice205.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }
            if (Temp < 11)
            {
                Temp = 20;
            }
            else
            {
                Temp = 0;
            }
            DescendingSubTotal2 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescLess11.Text = loader.GetString("Less11") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 6] = true;
        }

        private void Dice201_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescLess11();
        }

        private void Dice202_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescLess11();
        }

        private void Dice203_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescLess11();
        }

        private void Dice204_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescLess11();
        }

        private void Dice205_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescLess11();
        }
        #endregion

        #region ScoreAscLess11
        private void ScoreAscLess11()
        {
            if (Played[2, 6]) return;
            if (Ascending != 6) return;
            Ascending--;

            this.Dice331.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice332.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice333.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice334.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice335.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }
            if (Temp < 11)
            {
                Temp = 20;
            }
            else
            {
                Temp = 0;
            }
            AscendingSubTotal2 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscLess11.Text = loader.GetString("Less11") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 6] = true;
        }

        private void Dice331_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscLess11();
        }

        private void Dice332_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscLess11();
        }

        private void Dice333_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscLess11();
        }

        private void Dice334_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscLess11();
        }

        private void Dice335_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscLess11();
        }
        #endregion


        #region ScoreNormalSmall
        private void ScoreNormalSmall()
        {
            if (Played[0, 7]) return;

            this.Dice81.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice82.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice83.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice84.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice85.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[0, 8])
            {
                NormalSmallScore = Temp;
                NormalSubTotal2 += NormalSmallScore;
            }
            else
            {
                if (Temp < NormalBiggerScore)
                {
                    NormalSmallScore = Temp;
                    NormalSubTotal2 += NormalSmallScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalSmall.Text = loader.GetString("SmallStr") + " : " + NormalSmallScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 7] = true;
        }

        private void Dice81_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalSmall();
        }

        private void Dice82_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalSmall();
        }

        private void Dice83_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalSmall();
        }

        private void Dice84_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalSmall();
        }

        private void Dice85_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalSmall();
        }
        #endregion

        #region ScoreDescSmall
        private void ScoreDescSmall()
        {
            if (Played[1, 7]) return;
            if (Descending != 7) return;
            Descending++;

            this.Dice211.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice212.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice213.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice214.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice215.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[0, 8])
            {
                DescendingSmallScore = Temp;
                DescendingSubTotal2 += DescendingSmallScore;
            }
            else
            {
                if (Temp < DescendingBiggerScore)
                {
                    DescendingSmallScore = Temp;
                    DescendingSubTotal2 += DescendingSmallScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescSmall.Text = loader.GetString("SmallStr") + " : " + DescendingSmallScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 7] = true;
        }

        private void Dice211_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSmall();
        }

        private void Dice212_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSmall();
        }

        private void Dice213_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSmall();
        }

        private void Dice214_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSmall();
        }

        private void Dice215_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescSmall();
        }
        #endregion

        #region ScoreAscSmall
        private void ScoreAscSmall()
        {
            if (Played[2, 7]) return;
            if (Ascending != 7) return;
            Ascending--;

            this.Dice341.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice342.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice343.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice344.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice345.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[0, 8])
            {
                AscendingSmallScore = Temp;
                AscendingSubTotal2 += AscendingSmallScore;
            }
            else
            {
                if (Temp < AscendingBiggerScore)
                {
                    AscendingSmallScore = Temp;
                    AscendingSubTotal2 += AscendingSmallScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscSmall.Text = loader.GetString("SmallStr") + " : " + AscendingSmallScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 7] = true;
        }

        private void Dice341_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSmall();
        }

        private void Dice342_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSmall();
        }

        private void Dice343_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSmall();
        }

        private void Dice344_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSmall();
        }

        private void Dice345_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscSmall();
        }
        #endregion


        #region ScoreNormalBigger
        private void ScoreNormalBigger()
        {
            if (Played[0, 8]) return;

            this.Dice91.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice92.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice93.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice94.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice95.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;

            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[0, 7])
            {
                NormalBiggerScore = Temp;
                NormalSubTotal2 += NormalBiggerScore;
            }
            else
            {
                if (Temp > NormalSmallScore)
                {
                    NormalBiggerScore = Temp;
                    NormalSubTotal2 += NormalBiggerScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalBig.Text = loader.GetString("Bigger") + " : " + NormalBiggerScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 8] = true;
        }

        private void Dice91_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalBigger();
        }

        private void Dice92_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalBigger();
        }

        private void Dice93_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalBigger();
        }

        private void Dice94_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalBigger();
        }

        private void Dice95_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalBigger();
        }
        #endregion

        #region ScoreDescBigger
        private void ScoreDescBigger()
        {
            if (Played[1, 8]) return;
            if (Descending != 8) return;
            Descending++;

            this.Dice221.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice222.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice223.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice224.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice225.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;

            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[1, 7])
            {
                DescendingBiggerScore = Temp;
                DescendingSubTotal2 += DescendingBiggerScore;
            }
            else
            {
                if (Temp > DescendingSmallScore)
                {
                    DescendingBiggerScore = Temp;
                    DescendingSubTotal2 += DescendingBiggerScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescBig.Text = loader.GetString("Bigger") + " : " + DescendingBiggerScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 8] = true;
        }

        private void Dice221_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescBigger();
        }

        private void Dice222_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescBigger();
        }

        private void Dice223_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescBigger();
        }

        private void Dice224_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescBigger();
        }

        private void Dice225_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescBigger();
        }
        #endregion

        #region ScoreAscBigger
        private void ScoreAscBigger()
        {
            if (Played[2, 8]) return;
            if (Ascending != 8) return;
            Ascending--;

            this.Dice351.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice352.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice353.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice354.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice355.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;

            for (int i = 0; i < 5; i++)
            {
                Temp += Dices[i];
            }

            if (!Played[2, 7])
            {
                AscendingBiggerScore = Temp;
                AscendingSubTotal2 += AscendingBiggerScore;
            }
            else
            {
                if (Temp > AscendingSmallScore)
                {
                    AscendingBiggerScore = Temp;
                    AscendingSubTotal2 += AscendingBiggerScore;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscBig.Text = loader.GetString("Bigger") + " : " + AscendingBiggerScore.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 8] = true;
        }

        private void Dice351_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscBigger();
        }

        private void Dice352_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscBigger();
        }

        private void Dice353_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscBigger();
        }

        private void Dice354_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscBigger();
        }

        private void Dice355_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscBigger();
        }
        #endregion

        
        #region ScoreNormalFourOfAKind
        private void ScoreNormalFourOfAKind()
        {
            if (Played[0, 9]) return;

            this.Dice101.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice102.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice103.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice104.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice105.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] >= 4)
                {
                    Temp = 30 + ((i + 1) << 2);
                }
            }
            NormalSubTotal2 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalFourOfAKind.Text = loader.GetString("FourOfAKindStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 9] = true;
        }

        private void Dice101_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFourOfAKind();
        }

        private void Dice102_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFourOfAKind();
        }

        private void Dice103_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFourOfAKind();
        }

        private void Dice104_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFourOfAKind();
        }

        private void Dice105_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFourOfAKind();
        }
        #endregion

        #region ScoreDescFourOfAKind
        private void ScoreDescFourOfAKind()
        {
            if (Played[1, 9]) return;
            if (Descending != 9) return;
            Descending++;

            this.Dice231.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice232.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice233.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice234.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice235.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] >= 4)
                {
                    Temp = 30 + ((i + 1) << 2);
                }
            }
            DescendingSubTotal2 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescFourOfAKind.Text = loader.GetString("FourOfAKindStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 9] = true;
        }

        private void Dice231_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFourOfAKind();
        }

        private void Dice232_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFourOfAKind();
        }

        private void Dice233_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFourOfAKind();
        }

        private void Dice234_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFourOfAKind();
        }

        private void Dice235_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFourOfAKind();
        }
        #endregion

        #region ScoreAscFourOfAKind
        private void ScoreAscFourOfAKind()
        {
            if (Played[2, 9]) return;
            if (Ascending != 9) return;
            Ascending--;

            this.Dice361.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice362.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice363.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice364.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice365.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] >= 4)
                {
                    Temp = 30 + ((i + 1) << 2);
                }
            }
            AscendingSubTotal2 += Temp;

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscFourOfAKind.Text = loader.GetString("FourOfAKindStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 9] = true;
        }

        private void Dice361_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFourOfAKind();
        }

        private void Dice362_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFourOfAKind();
        }

        private void Dice363_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFourOfAKind();
        }

        private void Dice364_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFourOfAKind();
        }

        private void Dice365_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFourOfAKind();
        }
        #endregion


        #region ScoreNormalFullHousse
        private void ScoreNormalFullHousse()
        {
            if (Played[0, 10]) return;

            this.Dice111.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice112.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice113.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice114.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice115.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            int Find3 = 0, Find2 = 0;

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 3)
                {
                    Find3 = ((i + 1) << 1) + (i + 1);
                }
                if (Values[i] == 2)
                {
                    Find2 = (i + 1) << 1;
                }
            }

            if (Find2 != 0 & Find3 != 0)
            {
                Temp = 40 + Find2 + Find3;
                NormalSubTotal2 += Temp;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalFull.Text = loader.GetString("FullHouseStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 10] = true;
        }

        private void Dice111_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFullHousse();
        }

        private void Dice112_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFullHousse();
        }

        private void Dice113_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFullHousse();
        }

        private void Dice114_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFullHousse();
        }

        private void Dice115_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalFullHousse();
        }
        #endregion

        #region ScoreDescFullHousse
        private void ScoreDescFullHousse()
        {
            if (Played[1, 10]) return;
            if (Descending != 10) return;
            Descending++;

            this.Dice241.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice242.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice243.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice244.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice245.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            int Find3 = 0, Find2 = 0;

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 3)
                {
                    Find3 = ((i + 1) << 1) + (i + 1);
                }
                if (Values[i] == 2)
                {
                    Find2 = (i + 1) << 1;
                }
            }

            if (Find2 != 0 & Find3 != 0)
            {
                Temp = 40 + Find2 + Find3;
                DescendingSubTotal2 += Temp;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescFull.Text = loader.GetString("FullHouseStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 10] = true;
        }

        private void Dice241_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFullHousse();
        }

        private void Dice242_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFullHousse();
        }

        private void Dice243_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFullHousse();
        }

        private void Dice244_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFullHousse();
        }

        private void Dice245_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescFullHousse();
        }
        #endregion

        #region ScoreAscFullHousse
        private void ScoreAscFullHousse()
        {
            if (Played[2, 10]) return;
            if (Ascending != 10) return;
            Ascending--;

            this.Dice371.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice372.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice373.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice374.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice375.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            int Find3 = 0, Find2 = 0;

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 3)
                {
                    Find3 = ((i + 1) << 1) + (i + 1);
                }
                if (Values[i] == 2)
                {
                    Find2 = (i + 1) << 1;
                }
            }

            if (Find2 != 0 & Find3 != 0)
            {
                Temp = 40 + Find2 + Find3;
                AscendingSubTotal2 += Temp;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscFull.Text = loader.GetString("FullHouseStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 10] = true;
        }

        private void Dice371_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFullHousse();
        }

        private void Dice372_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFullHousse();
        }

        private void Dice373_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFullHousse();
        }

        private void Dice374_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFullHousse();
        }

        private void Dice375_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscFullHousse();
        }
        #endregion


        #region ScoreNormalStraight
        private void ScoreNormalStraight()
        {
            if (Played[0, 11]) return;

            this.Dice121.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice122.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice123.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice124.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice125.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
                Temp += Dices[i];
            }

            //Check that there is no double
            int Doubles = 0;
            int Zeros = 0;
            for (int i = 0; i < 6; i++)
            {
                if (Values[i] > 1)
                {
                    Doubles++;
                }
                if (Values[i] == 0)
                {
                    Zeros++;
                }
            }

            if (Doubles == 0 & Zeros == 1 & (Temp == 20 | Temp == 15))
            {
                if (Temp == 20) Temp = 60;
                if (Temp == 15) Temp = 45;
                NormalSubTotal2 += Temp;
            }
            else
            {
                Temp = 0;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalStraight.Text = loader.GetString("StraightStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 11] = true;
        }

        private void Dice121_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalStraight();
        }

        private void Dice122_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalStraight();
        }

        private void Dice123_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalStraight();
        }

        private void Dice124_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalStraight();
        }

        private void Dice125_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalStraight();
        }
        #endregion

        #region ScoreDescStraight
        private void ScoreDescStraight()
        {
            if (Played[1, 11]) return;
            if (Descending != 11) return;
            Descending++;

            this.Dice251.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice252.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice253.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice254.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice255.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
                Temp += Dices[i];
            }

            //Check that there is no double
            int Doubles = 0;
            int Zeros = 0;
            for (int i = 0; i < 6; i++)
            {
                if (Values[i] > 1)
                {
                    Doubles++;
                }
                if (Values[i] == 0)
                {
                    Zeros++;
                }
            }

            if (Doubles == 0 & Zeros == 1 & (Temp == 20 | Temp == 15))
            {
                if (Temp == 20) Temp = 60;
                if (Temp == 15) Temp = 45;
                DescendingSubTotal2 += Temp;
            }
            else
            {
                Temp = 0;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescStraight.Text = loader.GetString("StraightStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 11] = true;
        }

        private void Dice251_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescStraight();
        }

        private void Dice252_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescStraight();
        }

        private void Dice253_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescStraight();
        }

        private void Dice254_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescStraight();
        }

        private void Dice255_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescStraight();
        }
        #endregion

        #region ScoreAscStraight
        private void ScoreAscStraight()
        {
            if (Played[2, 11]) return;
            if (Ascending != 11) return;
            Ascending--;

            this.Dice381.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice382.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice383.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice384.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice385.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
                Temp += Dices[i];
            }

            //Check that there is no double
            int Doubles = 0;
            int Zeros = 0;
            for (int i = 0; i < 6; i++)
            {
                if (Values[i] > 1)
                {
                    Doubles++;
                }
                if (Values[i] == 0)
                {
                    Zeros++;
                }
            }

            if (Doubles == 0 & Zeros == 1 & (Temp == 20 | Temp == 15))
            {
                if (Temp == 20) Temp = 60;
                if (Temp == 15) Temp = 45;
                AscendingSubTotal2 += Temp;
            }
            else
            {
                Temp = 0;
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscStraight.Text = loader.GetString("StraightStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 11] = true;
        }

        private void Dice381_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscStraight();
        }

        private void Dice382_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscStraight();
        }

        private void Dice383_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscStraight();
        }

        private void Dice384_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscStraight();
        }

        private void Dice385_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscStraight();
        }
        #endregion


        #region ScoreNormalYatzee
        private void ScoreNormalYatzee()
        {
            if (Played[0, 12]) return;

            this.Dice131.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice132.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice133.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice134.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice135.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 5)
                {
                    Temp = 50 + i + 1 + ((i + 1) << 2);
                    NormalSubTotal2 += Temp;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.NormalYatzee.Text = loader.GetString("YatzeeStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[0, 12] = true;
        }

        private void Dice131_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalYatzee();
        }

        private void Dice132_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalYatzee();
        }

        private void Dice133_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalYatzee();
        }

        private void Dice134_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalYatzee();
        }

        private void Dice135_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreNormalYatzee();
        }
        #endregion

        #region ScoreDescYatzee
        private void ScoreDescYatzee()
        {
            if (Played[1, 12]) return;
            if (Descending != 12) return;
            Descending++;

            this.Dice261.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice262.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice263.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice264.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice265.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 5)
                {
                    Temp = 50 + i + 1 + ((i + 1) << 2);
                    DescendingSubTotal2 += Temp;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.DescYatzee.Text = loader.GetString("YatzeeStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[1, 12] = true;
        }

        private void Dice261_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescYatzee();
        }

        private void Dice262_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescYatzee();
        }

        private void Dice263_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescYatzee();
        }

        private void Dice264_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescYatzee();
        }

        private void Dice265_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreDescYatzee();
        }
        #endregion

        #region ScoreAscYatzee
        private void ScoreAscYatzee()
        {
            if (Played[2, 12]) return;
            if (Ascending != 12) return;
            Ascending--;

            this.Dice391.Source = SelectSmallDicesImage(Dices[0]);
            this.Dice392.Source = SelectSmallDicesImage(Dices[1]);
            this.Dice393.Source = SelectSmallDicesImage(Dices[2]);
            this.Dice394.Source = SelectSmallDicesImage(Dices[3]);
            this.Dice395.Source = SelectSmallDicesImage(Dices[4]);

            //Calc score
            int Temp = 0;
            int[] Values = new int[6];

            for (int i = 0; i < 5; i++)
            {
                Values[Dices[i] - 1]++;
            }

            for (int i = 0; i < 6; i++)
            {
                if (Values[i] == 5)
                {
                    Temp = 50 + i + 1 + ((i + 1) << 2);
                    AscendingSubTotal2 += Temp;
                }
            }

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            this.AscYatzee.Text = loader.GetString("YatzeeStr") + " : " + Temp.ToString() + " " + loader.GetString("Points");

            FinalizeATurn();
            Played[2, 12] = true;
        }

        private void Dice391_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscYatzee();
        }

        private void Dice392_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscYatzee();
        }

        private void Dice393_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscYatzee();
        }

        private void Dice394_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscYatzee();
        }

        private void Dice395_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ScoreAscYatzee();
        }
        #endregion
    }
}
