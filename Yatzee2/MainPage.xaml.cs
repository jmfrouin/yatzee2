using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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

        private ImageSource SelectDicesImage(int diceValue)
        {
            switch (diceValue)
            {
                case 1:
                    //return Properties.Resources.Dice1N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice1N.png"));
                case 2:
                    //return Properties.Resources.Dice2N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice2N.png"));
                case 3:
                    //return Properties.Resources.Dice3N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice3N.png"));
                case 4:
                    //return Properties.Resources.Dice4N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice4N.png"));
                case 5:
                    //return Properties.Resources.Dice5N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice5N.png"));
                default:
                    //return Properties.Resources.Dice6N;
                    return new BitmapImage(new Uri("ms-appx:///Assets/Dice6N.png"));
            }
        }

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
            this.picDice1.Source = SelectDicesImage(Dices[0]);
            this.picDice2.Source = SelectDicesImage(Dices[1]);
            this.picDice3.Source = SelectDicesImage(Dices[2]);
            this.picDice4.Source = SelectDicesImage(Dices[3]);
            this.picDice5.Source = SelectDicesImage(Dices[4]);

            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            //Update labels
            this.labelRemainingLaunch.Text = loader.GetString("Remaining") + Nb.ToString();
            //this.labelDicesTotal.Text = "Total : " + dicesTotal;
        }

    }
}
