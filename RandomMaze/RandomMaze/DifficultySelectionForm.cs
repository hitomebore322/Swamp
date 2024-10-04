using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandomMaze
{
    public partial class DifficultySelectionForm : Form
    {
        public DifficultySelectionForm()
        {
            InitializeComponent();
        }

        private void DifficultySelectionForm_Load(object sender, EventArgs e)
        {

        }
        private void btnEasy_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(10, 10); // Легкий уровень
            gameForm.Show(); // Показываем форму с лабиринтом
            this.Hide(); // Скрываем форму выбора сложности
        }

        private void btnMedium_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(20, 20); // Средний уровень
            gameForm.Show(); // Показываем форму с лабиринтом
            this.Hide(); // Скрываем форму выбора сложности
        }

        private void btnHard_Click(object sender, EventArgs e)
        {
            Form1 gameForm = new Form1(30, 30); // Сложный уровень
            gameForm.Show(); // Показываем форму с лабиринтом
            this.Hide(); // Скрываем форму выбора сложности
        }
    }
}
