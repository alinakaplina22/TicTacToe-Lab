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
        private string currentPlayer;
        private bool gameEnded = false;

        private string player1Symbol = "X";
        private string player2Symbol = "O";


        private int score1 = 0;
        private int score2 = 0;

        public MainWindow()
        {
            InitializeComponent();

            Symbol1Box.Text = player1Symbol;
            Symbol2Box.Text = player2Symbol;
            UpdateScoreDisplay();
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

            currentPlayer = player1Symbol;
            gameEnded = false;
            this.Title = $"Ходит {currentPlayer}";
        }


        private void UpdateScoreDisplay()
        {
            Score1Text.Text = $"{player1Symbol}: {score1}";
            Score2Text.Text = $"{player2Symbol}: {score2}";
        }


        private void ApplySymbolsButton_Click(object sender, RoutedEventArgs e)
        {
            string newSym1 = Symbol1Box.Text.Trim();
            string newSym2 = Symbol2Box.Text.Trim();


            if (string.IsNullOrEmpty(newSym1) || string.IsNullOrEmpty(newSym2))
            {
                MessageBox.Show("Символы не могут быть пустыми!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (newSym1 == newSym2)
            {
                MessageBox.Show("Символы должны отличаться!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            player1Symbol = newSym1;
            player2Symbol = newSym2;


            UpdateScoreDisplay();

            StartNewGame();
        }
        
        private void ResetScoreButton_Click(object sender, RoutedEventArgs e)
        {
            score1 = 0;
            score2 = 0;
            UpdateScoreDisplay();
            StartNewGame();
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

                if (currentPlayer == player1Symbol)
                    score1++;
                else
                    score2++;

                UpdateScoreDisplay();
                MessageBox.Show($"Игрок {currentPlayer} победил!", "Игра окончена");
                gameEnded = true;
                this.Title = $"Победил {currentPlayer}";
                return;
            }


            if (IsBoardFull())
            {
                MessageBox.Show("Ничья!", "Игра окончена");
                gameEnded = true;
                this.Title = "Ничья";
                return;
            }


            currentPlayer = (currentPlayer == player1Symbol) ? player2Symbol : player1Symbol;
            this.Title = $"Ходит {currentPlayer}";
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
    }
}