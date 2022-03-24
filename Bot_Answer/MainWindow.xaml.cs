using System;
using System.Collections.Generic;
using System.IO;
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
using System.Text.RegularExpressions;

namespace Bot_Answer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            string path = "DelDataBase.txt";
            string DB = File.ReadAllText(path);
            string quest = TextBox1.Text.Trim();

            if (Regex.IsMatch(DB, quest))
                {
                    string[] lines = File.ReadAllLines(path);
                    foreach (string line in lines)
                        if (Regex.IsMatch(line, quest))
                        {
                            Chat.Items.Add("Вы : " + quest);
                            Say(path, line);
                        }
            }
            else
            {
                Remember(quest);
            }



        }

        private void Remember(string quest)
        {
            Chat.Items.Add("Вы :" + quest);
            Chat.Items.Add("Bot : Sorry, I dont know");

            TextBox1.Visibility = Visibility.Hidden;
            Send.Visibility = Visibility.Hidden;
        }

        private void Say(string path,  string answer)
        {
            using(FileStream fs = new FileStream(path , FileMode.OpenOrCreate))
            using (StreamReader sw = new StreamReader(fs))
            {
                int cout = 0;
                cout = answer.IndexOf(": ");
                Chat.Items.Add( "Bot : " + answer.Substring(cout + 2)); 
            }
        }

        private void Train_Click(object sender, RoutedEventArgs e)
        {
            string quest = TextBox1.Text.Trim();
            string quest1 = TextBox2.Text.Trim();
            string path = "DelDataBase.txt";
            using (FileStream fs = new FileStream(path, FileMode.Append))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(quest + " : "+ quest1);
            }
            TextBox2.Visibility = Visibility.Hidden;
            Train.Visibility = Visibility.Hidden;
            TextBox1.Visibility = Visibility.Visible;
            Send.Visibility = Visibility.Visible;
        }
    }
}
