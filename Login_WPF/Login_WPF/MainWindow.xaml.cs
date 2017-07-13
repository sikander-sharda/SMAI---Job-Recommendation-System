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
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;

namespace Login_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public class Patient
    {
        public string ID {get; set;}
        public string FName {get; set;}
        public string Mobile { get; set; }
        

    }
    public class Patient_Record
    {
        public string ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }

    }
    //public class 
    public partial class MainWindow : Window
    {
        String str;
        List<string> data = new List<string>();
        public MainWindow()
        {

            InitializeComponent();
            readFile();
            dataGrid1.ItemsSource = LoadData();
        }
        private void readFile()
        {
            string text = System.IO.File.ReadAllText(@"C:\Users\sikander.s1\Documents\Visual Studio 2013\Projects\Samsung_practice\file.txt");
            string ln = "";
            foreach(var ch in text)
            {
                //System.Console.Write(ch);
                if(ch=='\n')
                {
                    data.Add(ln);
                    System.Console.Write(ln);
                    ln = "";
                }
                else
                {
                    ln += ch;
                }
            }
        }
        private int checkDuplicate(string str)
        {
            string temp ="";
            foreach(string s in data)
            {
                System.Console.WriteLine(s);
                var spdata = s.Split('#');
                temp = "";
                for(int i = 1; i<spdata.Length-1; i++)
                {
                    if(i!=1)
                    {
                        temp += "#";
                    }
                    temp += spdata[i];
                }
                System.Console.WriteLine(temp);
                if (temp == str)
                    return 0; 
            }
            return 1;
        }
        private List<Patient> LoadData()
        {
            List<Patient> patients = new List<Patient>();
            
                foreach (string s in data)
                {

                    var spdata = s.Split('#');
                    patients.Add(new Patient()
                        {
                            ID = spdata[0],
                            FName = spdata[1],

                            Mobile = spdata[4],

                        });

                }
           
            
            return patients;
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            textBoxMobile.Text = "";
            dateP.Text = "";
            
        }
        private void dataGrid1_SelectionChanged(object sender, EventArgs e)
        {
            Patient sel = (Patient)dataGrid1.SelectedItem;
            string text = System.IO.File.ReadAllText(@"C:\Users\sikander.s1\Documents\Visual Studio 2013\Projects\Samsung_practice\file.txt");
            //sel.ID
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxAddress.Text = "";
            textBoxMobile.Text = "";
            dateP.Text = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Patient sel = (Patient)dataGrid1.SelectedItem;
            System.Console.WriteLine(sel.FName);
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                //errormessage.Text = DateTime.Now.To;
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string mobile = textBoxMobile.Text;
                if (textBoxMobile.Text.Length == 0)
                {
                    errormessage.Text = "Enter Mobile No.";
                    textBoxMobile.Focus();
                }
                else if (!Regex.IsMatch(textBoxMobile.Text, @"^\d{10}$"))
                {
                    errormessage.Text = "Enter a valid Mobile No.";
                    textBoxMobile.Focus();
                }
                else
                {
                    if(firstname.Length == 0||lastname.Length == 0 || !Regex.IsMatch(firstname, @"^[A-Za-z]+$")||!Regex.IsMatch(lastname, @"^[A-Za-z]+$"))
                    {
                        errormessage.Text = "Enter a valid Name";
                        textBoxFirstName.Focus();
                        textBoxLastName.Focus();
                    }
                    else
                    {
                        errormessage.Text = "";
                        string address = textBoxAddress.Text;
                        str = firstname + "#" + lastname + "#" + email + "#" + mobile;
                        //string id = DateTime.Now.ToString();

                        if (checkDuplicate(str) == 0)
                        {
                            errormessage.Text = "User already registered";
                        }
                        else
                        {
                            string id = DateTime.Now.ToString();
                            
                            str = id + "#" + firstname + "#" + lastname + "#" + email + "#" + mobile + "#" + dateP;
                            data.Add(str);
                            dataGrid1.ItemsSource = LoadData();
                            File.AppendAllText(@"C:\Users\sikander.s1\Documents\Visual Studio 2013\Projects\Samsung_practice\file.txt", str + Environment.NewLine);
                            
                            errormessage.Text = "You have Registered successfully.";

                            
                            Reset();
                        }
                    }
                }
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
