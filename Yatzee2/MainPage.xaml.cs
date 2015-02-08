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
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice1N.png"));
                case 2:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice2N.png"));
                case 3:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice3N.png"));
                case 4:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice4N.png"));
                case 5:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice5N.png"));
                default:
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice6N.png"));
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
            Nb = 3;
            Total--;
            if (Total == 0)
            {
                //MessageBox.Show("!!\nPlease click on New game.", "Attention");
                /*var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                
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

            /*
            //Update scores
            labelNormalSubTotal1.Text = "SubTotal : " + NormalSubTotal1.ToString();
            labelNormalSubTotal2.Text = "SubTotal : " + NormalSubTotal2.ToString();
            labelNormalBonus.Text = "Bonus : " + NormalBonus.ToString();
            labelNormalTotal.Text = "Total : " + NormalTotal.ToString();

            labelAscendingSubTotal1.Text = "SubTotal : " + AscendingSubTotal1.ToString();
            labelAscendingSubTotal2.Text = "SubTotal : " + AscendingSubTotal2.ToString();
            labelAscendingBonus.Text = "Bonus : " + AscendingBonus.ToString();
            labelAscendingTotal.Text = "Total : " + AscendingTotal.ToString();

            labelDescendingSubTotal1.Text = "SubTotal : " + DescendingSubTotal1.ToString();
            labelDescendingSubTotal2.Text = "SubTotal : " + DescendingSubTotal2.ToString();
            labelDescendingBonus.Text = "Bonus : " + DescendingBonus.ToString();
            labelDescendingTotal.Text = "Total : " + DescendingTotal.ToString();

            int BigTotal = NormalTotal + AscendingTotal + DescendingTotal;
            labelGrandTotal.Text = "Score = " + BigTotal.ToString();

            LaunchDicesButton.Show();
             * /

            //Reset dices
            /*picDice1.Image = Properties.Resources.BBlank;
            picDice2.Image = Properties.Resources.BBlank;
            picDice3.Image = Properties.Resources.BBlank;
            picDice4.Image = Properties.Resources.BBlank;
            picDice5.Image = Properties.Resources.BBlank;*/

            //Reset infos
            this.labelRemainingLaunch.Text = "";
            this.labelDicesTotal.Text = "";

            //Refresh
            //Refresh();
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
    }
}
