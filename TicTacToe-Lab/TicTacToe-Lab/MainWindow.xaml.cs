using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace TicTacToeLab
{
    public partial class MainWindow : Window
    {
       
        private string[,] board = new string[3, 3];
   
        private string currentPlayer = "X";
     
        private bool gameEnded = false;

        public MainWindow()
        {
            InitializeComponent();
            StartNewGame();
        }

  
        private void StartNewGame()
        {

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    board[i, j] = "";


            Button00.Content = "";
            Button01.Content = "";
            Button02.Content = "";
            Button10.Content = "";
            Button11.Content = "";
            Button12.Content = "";
            Button20.Content = "";
            Button21.Content = "";
            Button22.Content = "";

            currentPlayer = "X";
            gameEnded = false;
            StatusText.Text = "Ходит X";
        }

   
        private void CellButton_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded) return;

            Button clickedButton = sender as Button;
            if (clickedButton.Content != null && clickedButton.Content.ToString() != "")
                return; 

           
            string tag = clickedButton.Tag.ToString();
            string[] coords = tag.Split(',');
            int row = int.Parse(coords[0]);
            int col = int.Parse(coords[1]);

            board[row, col] = currentPlayer;
            clickedButton.Content = currentPlayer;

            if (CheckWin(currentPlayer))
            {
                StatusText.Text = $"Игрок {currentPlayer} победил!";
                gameEnded = true;
                return;
            }


            if (IsBoardFull())
            {
                StatusText.Text = "Ничья!";
                gameEnded = true;
                return;
            }


            currentPlayer = (currentPlayer == "X") ? "O" : "X";
            StatusText.Text = $"Ходит {currentPlayer}";
        }


        private bool CheckWin(string player)
        {

            for (int i = 0; i < 3; i++)
                if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
                    return true;

            for (int j = 0; j < 3; j++)
                if (board[0, j] == player && board[1, j] == player && board[2, j] == player)
                    return true;

            if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
                return true;
            if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
                return true;

            return false;
        }


        private bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (string.IsNullOrEmpty(board[i, j]))
                        return false;
            return true;
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            StartNewGame();
        }
    }
}