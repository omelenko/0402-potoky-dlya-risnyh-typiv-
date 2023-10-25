using System.Collections.Concurrent;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        Animal[] animalsounds = new Animal[2];
        static List<Task> tasks = new List<Task>();
        class Animal
        {
            public static ConcurrentDictionary<string, int> ClassCounts { get; private set; } = new ConcurrentDictionary<string, int>();
            public static int TotalCount;

            public Animal()
            {
                string className = this.GetType().Name;
                ClassCounts.AddOrUpdate(className, 1, (key, oldValue) => oldValue + 1);
                Interlocked.Increment(ref TotalCount);
            }

            public virtual void MakeSound()
            {

            }

            public static void PrintCounts()
            {
                String a;
                MessageBox.Show("Кількість тварин: " + TotalCount);
                foreach (var kvp in ClassCounts)
                {
                    a = kvp.Key == "Dog" ? "собак" : "котів";
                    MessageBox.Show($"Кількість {a}: {kvp.Value}");
                }
            }
        }

        class Dog : Animal
        {
            public Dog() : base() { }

            public override void MakeSound()
            {
                MessageBox.Show("Гав!", this.GetType().Name);
            }
            public static void PrintCounts()
            {
                MessageBox.Show($"Кількість собак: {ClassCounts["Dog"]}");
            }
        }

        class Cat : Animal
        {
            public Cat() : base() { }

            public override void MakeSound()
            {
                MessageBox.Show("Мяу!", this.GetType().Name);
            }
            public static void PrintCounts()
            {
                MessageBox.Show($"Кількість котів: {ClassCounts["Cat"]}");
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tasks.Add(Task.Run(() => { var cat = new Cat(); }));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            animalsounds[0] = new Dog();
            animalsounds[1] = new Cat();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tasks.Add(Task.Run(() => { var dog = new Dog(); }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Cat.PrintCounts();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Animal.PrintCounts();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dog.PrintCounts();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            animalsounds[1].MakeSound();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            animalsounds[0].MakeSound();
        }
    }
}