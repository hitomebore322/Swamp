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
    public partial class Form1 : Form
    {
        private int[,] maze; // Двумерный массив для представления лабиринта
        private int width = 21; // Ширина лабиринта (нечетное число)
        private int height = 21; // Высота лабиринта (нечетное число)
        private Random rand = new Random(); // Генератор случайных чисел
        private int finishX; // Координата X финиша
        private int finishY; // Координата Y финиша

        private int playerX; // Координата X игрока
        private int playerY; // Координата Y игрока
        private const int playerSize = 20; // Размер игрока увеличен в 2 раза

        private bool gameFinished = false; // Переменная для отслеживания завершения игры

        private List<(int, int)> items = new List<(int, int)>(); // Позиции предметов
        private int itemsCollected = 0; // Сколько предметов собрано
        private int totalItems = 2; // Общее количество предметов в лабиринте

        private List<Image> characterSprites; // Список спрайтов для анимации
        private int currentFrame = 0; // Текущий кадр анимации
        private int frameCount = 4; // Количество кадров в анимации
        private int frameDelay = 100; // Задержка между кадрами в миллисекундах
        private DateTime lastFrameChange; // Время последнего изменения кадра
        private Image wallTexture; // Объявление переменной для текстуры стены
        private Image floorTexture;
        private Image backgroundTexture; // Переменная для текстуры фона
        private Image playerSprite; // Изображение игрока
        private Image pickupTexture;
        private Image finishtexture;

        private void LoadFinish()
        {
            finishtexture = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\finish.png");

        }

        private void LoadPickup()
        { 
        pickupTexture = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\orb4x.png");

        }
        private void LoadTextures()
        {
            // Загрузка текстуры для пола
            floorTexture = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\floor.png");
            // Загрузка текстуры для стены
            wallTexture = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\1woll.png");
            backgroundTexture = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\back.png");


        }

        // Метод для загрузки спрайтов
        private void LoadCharacterSprites()
        {
            playerSprite = Image.FromFile("D:\\ForGit\\RandomMaze\\RandomMaze\\images\\orc4x.png");
        }


        public Form1(int width, int height)
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Включаем двойную буферизацию
            this.width = width; // Устанавливаем ширину лабиринта
            this.height = height; // Устанавливаем высоту лабиринта
            LoadCharacterSprites(); // Загрузка спрайтов персонажа
            LoadTextures();
            LoadPickup();
            LoadFinish();
            GenerateMazeDFS(); // Генерируем лабиринт при запуске формы

            // Устанавливаем размеры окна в зависимости от размера лабиринта
            int cellSize = 30; // Размер одной клетки
            int formWidth = cellSize * this.width; // Общая ширина
            int formHeight = cellSize * this.height; // Общая высота


            // Устанавливаем клиентскую часть окна под лабиринт
            this.ClientSize = new Size(formWidth, formHeight);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Окно фиксированного размера
            this.MaximizeBox = false; // Отключаем кнопку максимизации
            this.StartPosition = FormStartPosition.CenterScreen; // Центрируем форму на экране


            // Добавляем события для отрисовки и управления
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            this.Shown += new EventHandler(Form1_Shown);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            MessageBox.Show("Добро пожаловать в лабиринт, для начала собери все сферы.");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate(); // Перерисовываем форму при изменении ее размера
        }

        private void GenerateItems(int itemCount)
        {
            for (int i = 0; i < itemCount; i++)
            {
                while (true)
                {
                    int randomX = rand.Next(1, width - 2);
                    int randomY = rand.Next(1, height - 2);

                    // Проверяем, что это проходимая клетка и что здесь еще нет предмета
                    if (maze[randomX, randomY] == 1 && !items.Contains((randomX, randomY)))
                    {
                        items.Add((randomX, randomY)); // Добавляем предмет
                        break;
                    }
                }
            }
        }
        public Form1()
        {
            InitializeComponent(); // Инициализация компонентов формы
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawMaze(e.Graphics); // Отрисовываем лабиринт при запуске
        }

        private void PlaceItems()
        {
            Random rand = new Random();
            for (int i = 0; i < totalItems; i++)
            {
                int x, y;
                do
                {
                    x = rand.Next(1, width - 2); // Генерируем случайные координаты
                    y = rand.Next(1, height - 2);
                } while (maze[x, y] != 1 || (x == playerX && y == playerY)); // Убеждаемся, что клетка проходима и там не находится игрок

                items.Add((x, y)); // Добавляем предмет на карту
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void GenerateMazeDFS()
        {
            maze = new int[width, height];

            // Инициализация: все клетки — стены
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y] = 0; // 0 — стена
                }
            }

            Stack<(int, int)> stack = new Stack<(int, int)>();

            // Начальная клетка
            int startX = 1;
            int startY = 1;
            maze[startX, startY] = 1; // Проходимая клетка
            stack.Push((startX, startY));

            Random rand = new Random();

            // Пока есть клетки для обработки
            while (stack.Count > 0)
            {
                var current = stack.Pop();

                // Получаем непосещенные соседние клетки
                var neighbors = GetUnvisitedNeighbors(current.Item1, current.Item2);

                if (neighbors.Count > 0)
                {
                    stack.Push(current); // Добавляем текущую клетку обратно в стек

                    // Увеличиваем шанс выбора нескольких соседей для создания развилок
                    foreach (var neighbor in neighbors)
                    {
                        if (rand.Next(100) < 80) // 30% шанс на создание тупика
                        {
                            // Не соединяем клетку с соседями, оставляем тупик
                            continue;
                        }
                        if (rand.Next(100) < 80) // 50% шанс на создание развилки
                        {
                            // Соединяем текущую клетку с выбранным соседом
                            maze[(current.Item1 + neighbor.Item1) / 2, (current.Item2 + neighbor.Item2) / 2] = 1; // Пробиваем стену
                            maze[neighbor.Item1, neighbor.Item2] = 1; // Делаем клетку проходимой

                            stack.Push(neighbor); // Добавляем соседа в стек для дальнейшей обработки
                        }
                    }
                }
            }

            GenerateFinish(); // Генерация финиша после создания лабиринта
            PlaceItems();

            // Устанавливаем начальные координаты игрока
            if (maze[2, 2] == 1) // Проверяем, что клетка проходима
            {
                playerX = 2;
                playerY = 2;
            }
            else
            {
                playerX = startX; // Если клетка (2, 2) непроходима, используем стартовую позицию
                playerY = startY;
            }

        }


        // Метод для получения непосещенных соседей для DFS
        private List<(int, int)> GetUnvisitedNeighbors(int x, int y)
        {
            List<(int, int)> neighbors = new List<(int, int)>();

            // Проверяем 4 направления (вверх, вниз, влево, вправо)
            if (x > 1 && maze[x - 2, y] == 0) // Вверх
            {
                neighbors.Add((x - 2, y));
            }
            if (x < width - 2 && maze[x + 2, y] == 0) // Вниз
            {
                neighbors.Add((x + 2, y));
            }
            if (y > 1 && maze[x, y - 2] == 0) // Влево
            {
                neighbors.Add((x, y - 2));
            }
            if (y < height - 2 && maze[x, y + 2] == 0) // Вправо
            {
                neighbors.Add((x, y + 2));
            }

            return neighbors;
        }


        private void AddFrontier(int x, int y, List<(int, int)> frontier)
        {
            if (x > 1 && maze[x - 2, y] == 0) frontier.Add((x - 2, y)); // Слева
            if (x < width - 2 && maze[x + 2, y] == 0) frontier.Add((x + 2, y)); // Справа
            if (y > 1 && maze[x, y - 2] == 0) frontier.Add((x, y - 2)); // Сверху
            if (y < height - 2 && maze[x, y + 2] == 0) frontier.Add((x, y + 2)); // Снизу
        }

        private void GenerateFinish()
        {
            // Начальная позиция игрока
            int startX = 1;
            int startY = 1;

            int maxDistance = 0;
            int farthestX = startX;
            int farthestY = startY;

            // Проходим по всем клеткам лабиринта
            for (int x = 1; x < width; x += 2)
            {
                for (int y = 1; y < height; y += 2)
                {
                    if (maze[x, y] == 1) // Только по проходимым клеткам
                    {
                        int distance = Math.Abs(x - startX) + Math.Abs(y - startY); // Манхэттенское расстояние

                        // Если найдено большее расстояние, обновляем координаты финиша
                        if (distance > maxDistance)
                        {
                            maxDistance = distance;
                            farthestX = x;
                            farthestY = y;
                        }
                    }
                }
            }

            // Устанавливаем финиш в самой дальней точке
            finishX = farthestX;
            finishY = farthestY;

            // Делаем клетку финиша проходимой
            maze[finishX, finishY] = 1;
        }

        private List<(int, int)> GetNeighbors(int x, int y)
        {
            List<(int, int)> neighbors = new List<(int, int)>();

            if (x > 1 && maze[x - 2, y] == 1) neighbors.Add((x - 2, y)); // Слева
            if (x < width - 2 && maze[x + 2, y] == 1) neighbors.Add((x + 2, y)); // Справа
            if (y > 1 && maze[x, y - 2] == 1) neighbors.Add((x, y - 2)); // Сверху
            if (y < height - 2 && maze[x, y + 2] == 1) neighbors.Add((x, y + 2)); // Снизу

            return neighbors;
        }

        private void GeneratePath(int x, int y)
        {
            // Направления движения: вверх, вниз, влево, вправо
            int[] directions = { 0, 1, 2, 3 };
            Shuffle(directions); // Перемешивание для случайности

            foreach (int direction in directions)
            {
                int dx = 0, dy = 0;

                switch (direction)
                {
                    case 0: dx = 0; dy = -2; break; // Вверх
                    case 1: dx = 0; dy = 2; break;  // Вниз
                    case 2: dx = -2; dy = 0; break; // Влево
                    case 3: dx = 2; dy = 0; break;  // Вправо
                }

                int nx = x + dx;
                int ny = y + dy;

                // Проверка, что клетка в пределах и еще не посещена
                if (nx > 0 && nx < width - 1 && ny > 0 && ny < height - 1 && maze[nx, ny] == 0)
                {
                    maze[nx - dx / 2, ny - dy / 2] = 1; // Убираем стену между клетками
                    maze[nx, ny] = 1; // Делаем клетку проходимой
                    GeneratePath(nx, ny); // Рекурсия
                }
            }
        }

        // Перемешивание массива для случайного выбора направлений
        private void Shuffle(int[] array)
        {
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        private void DrawMaze(Graphics g)
        {
            // Устанавливаем коэффициент масштабирования
            float scaleFactor = 1.5f; // Измените этот коэффициент для увеличения/уменьшения масштаба

            // Сохраняем текущее состояние графики
            g.ScaleTransform(scaleFactor, scaleFactor); // Масштабируем графику

            int cellSize = 20; // Размер одной клетки

            g.DrawImage(backgroundTexture, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze[x, y] == 0)
                    {
                        // Отрисовываем текстуру стены
                        g.DrawImage(wallTexture, x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                    else
                    {
                        // Отрисовываем текстуру пола
                        g.DrawImage(floorTexture, x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }

            // Рисуем предметы 
            foreach (var item in items)
            {
                g.DrawImage(pickupTexture, item.Item1 * cellSize, item.Item2 * cellSize, cellSize, cellSize);
            }

            // Рисуем финиш (зеленый квадрат)
            if (itemsCollected == totalItems) // Финиш доступен, если все предметы собраны
            {
                g.DrawImage(finishtexture, finishX * cellSize, finishY * cellSize, cellSize, cellSize);
            }

            // Рисуем игрока
            DrawCharacter(g);
        }

        private void DrawCharacter(Graphics g)
        {
            // Рисуем игрока на фиксированных координатах
            g.DrawImage(playerSprite, playerX * 20, playerY * 20, playerSize, playerSize);
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = playerX;
            int newY = playerY;

            switch (e.KeyCode)
            {
                case Keys.W: newY -= 1; break;
                case Keys.S: newY += 1; break;
                case Keys.A: newX -= 1; break;
                case Keys.D: newX += 1; break;
            }

            if (newX >= 0 && newX < width && newY >= 0 && newY < height && maze[newX, newY] == 1)
            {
                playerX = newX;
                playerY = newY;
                // Перерисовываем только необходимую область
                this.Invalidate(); // Вызов перерисовки формы
            }

            // Проверка на сбор предметов
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Item1 == playerX && items[i].Item2 == playerY)
                {
                    items.RemoveAt(i); // Убираем предмет с карты
                    itemsCollected++; // Увеличиваем счетчик собранных предметов
                    break;
                }
            }

            if(itemsCollected == totalItems && !gameFinished) // Добавляем флаг, чтобы сообщение показывалось один раз
    {
                MessageBox.Show("Все сферы собраны! Отнеси их гоблину.");
                gameFinished = true; // Устанавливаем флаг завершения игры
            }

            // Проверка на достижение финиша
            if (playerX == finishX && playerY == finishY && itemsCollected == totalItems)
            {
                MessageBox.Show("Поздравляем! Вы прошли лабиринт!");
                Menu menu = new Menu(); // Возвращаемся в главное меню
                menu.Show();
                this.Hide();
            }

            this.Invalidate(); // Перерисовать форму
        }
    }
}
