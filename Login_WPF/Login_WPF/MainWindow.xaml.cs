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
        List<Patient> display_data = new List<Patient>();
        List<Patient_Record> main_data = new List<Patient_Record>();
        public MainWindow()
        {

            InitializeComponent();
            readFile();
            dataGrid1.ItemsSource = LoadData();
        }
        private void readFile()
        {
            string text2 = System.IO.File.ReadAllText(@"C:\Users\Corleone\Documents\SMAI---Job-Recommendation-System-master\Login_WPF\file.txt");
           // string ln = "";
            var temp_da = text2.Split('\n');
            for (int i = 0; i < temp_da.Length - 1; i++)
            {
                var spdata = temp_da[i].Split('#');
                Patient temp_p = new Patient();
                temp_p.FName = spdata[1]; temp_p.ID = spdata[0]; temp_p.Mobile = spdata[4];
                System.Console.WriteLine(temp_p.FName + " " + temp_p.ID + " " + temp_p.Mobile);
                display_data.Add(temp_p);
                Patient_Record temp_pt = new Patient_Record();
                temp_pt.FName = spdata[1]; temp_pt.ID = spdata[0]; temp_pt.Mobile = spdata[4];
                temp_pt.LName = spdata[2]; temp_pt.DOB = spdata[5]; temp_pt.Email = spdata[3];
                main_data.Add(temp_pt);
            }
            
        }
        private int checkDuplicate(string firstname, string lastname, string email, string mobile)
        {
            foreach(var p in main_data)
            {
                if((p.Email==email)&&(p.FName==firstname)&&(p.LName==lastname)&&(p.Mobile==mobile))
                {
                    return 0;
                }
            }
            //Patient temp_p = new Patient
            return 1;
        }
        private List<Patient> LoadData()
        {
            /*List<Patient> patients = new List<Patient>();
            
                foreach (string s in data)
                {

                    var spdata = s.Split('#');
                    patients.Add(new Patient()
                        {
                            ID = spdata[0],
                            FName = spdata[1],

                            Mobile = spdata[4],

                        });

                }*/
           
            
            return display_data;
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
            string text = System.IO.File.ReadAllText(@"C:\Users\Corleone\Documents\SMAI---Job-Recommendation-System-master\Login_WPF\file.txt");
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

                        if (checkDuplicate(firstname, lastname, email, mobile) == 0)
                        {
                            errormessage.Text = "User already registered";
                        }
                        else
                        {
                            string id = DateTime.Now.ToString("ddMMyyyyHHmmss");
                            Patient temp_p = new Patient();
                            temp_p.ID = id; temp_p.Mobile = mobile; temp_p.FName = firstname;
                            display_data.Add(temp_p);
                            Patient_Record temp_pt = new Patient_Record();
                            temp_pt.Mobile = mobile; temp_pt.LName = lastname; temp_pt.ID = id;
                            temp_pt.FName = firstname; temp_pt.Email = email; temp_pt.DOB = dateP.ToString();
                            main_data.Add(temp_pt);
                            str = id + "#" + firstname + "#" + lastname + "#" + email + "#" + mobile + "#" + dateP;
                            //data.Add(str);
                            dataGrid1.ItemsSource = LoadData(); 
                            File.AppendAllText(@"C:\Users\Corleone\Documents\SMAI---Job-Recommendation-System-master\Login_WPF\file.txt", str + Environment.NewLine);
                            
                            errormessage.Text = "You have Registered successfully.";
                           // dataGrid1.ItemsSource = display_data;
                            
                            Reset();
                        }
                    }
                }
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dataGrid1.SelectedItem; //probably you find this object
            string ij = (dataGrid1.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
            System.Console.WriteLine(ij);
            foreach(var item2 in main_data)
            {
                if(item2.ID==ij)
                {
                    textBoxFirstName.Text = item2.FName;
                    textBoxLastName.Text = item2.LName;
                    textBoxEmail.Text = item2.Email;
                    //textBoxAddress.Text = item.;
                    textBoxMobile.Text = item2.Mobile;
                    dateP.Text = item2.DOB;
                }
            }
        }
    }
}
