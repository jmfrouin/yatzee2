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
        }
    }
}
