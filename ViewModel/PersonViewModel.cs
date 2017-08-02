using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyProj.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyProj.Command;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Controls;



namespace MyProj.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {

        public string glob_id;
        public int Flag = 0;
        void ReadFile()
        {
            string text2 = System.IO.File.ReadAllText(@"D:\file.txt");
            // string ln = "";
            var temp_da = text2.Split('\n');
            for (int i = 0; i < temp_da.Length - 1; i++)
            {
                var spdata = temp_da[i].Split('#');
                Patient temp_p = new Patient();
                temp_p.Fname = spdata[1]; temp_p.Id = spdata[0]; temp_p.Mobile = spdata[4];
                //System.Console.WriteLine(temp_p.Fname + " " + temp_p.Id + " " + temp_p.Mobile);
                display_data.Add(temp_p);
                Patient_Record temp_pt = new Patient_Record();
                temp_pt.Fname = spdata[1]; temp_pt.Id = spdata[0]; temp_pt.Mobile = spdata[4];
                temp_pt.Lname = spdata[2]; temp_pt.DOB = spdata[5]; temp_pt.Email = spdata[3];
                main_data.Add(temp_pt);
            }
        }
        private bool checkDuplicate(string firstname, string lastname, string email, string mobile)
        {
            if (firstname.Length == 0 || lastname.Length == 0 )
            {
                Error = "Enter Name";
                ErrorPos = "419,20,-26,460";
                return false;
            }
            if(!Regex.IsMatch(firstname, @"^[A-Za-z]+$") || !Regex.IsMatch(lastname, @"^[A-Za-z]+$"))
            {
                Error = "Enter a valid Name. Should not contain any digits.";
                ErrorPos = "419,20,-26,460";
                return false;
            }
            if (email.Length == 0)
            {
                Error = "Enter an email.";
                Console.WriteLine("fweeee");
                ErrorPos = "419,80,-26,400";
                return false;
                //errormessage.Text = DateTime.Now.To;
                //textBoxEmail.Focus();
            }


            if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                Error = "Enter a valid email.";
                ErrorPos = "419,80,-26,400";
                return false;
                //
            }
            if (mobile.Length == 0)
            {
                Error = "Enter Mobile No.";
                ErrorPos = "419,110,-26,370";
                return false;
            }
            if (!Regex.IsMatch(mobile, @"^\d{10}$"))
            {
                Error = "Enter a valid Mobile No. Should have 10 digits.";
                ErrorPos = "419,110,-26,370";
                return false;
            }
            

            foreach (var p in main_data)
            {
                if ((p.Email == email) && (p.Fname == firstname) && (p.Lname == lastname) && (p.Mobile == mobile))
                {
                    Error = "User already Registered";
                    return false;
                }
            }
            //Patient temp_p = new Patient
            Error = "So Far So Good";
            return true;

        }
        public PersonViewModel()
        {
            ErrorPos = "67,0,0,480";
            Flag = 0;
            //SQLiteConnection conn = new SQLiteConnection("Data Source=Patients.db");
            Selected_Patient = new Patient { Fname = "", Id = "", Mobile = "" };
            Error = "";
            Add_Update = "Add";
            PatientRecord_data = new Patient_Record { Email = "", DOB = "", Fname = "", Id = "", Lname = "", Mobile = "" };
            display_data = new ObservableCollection<Patient>();
            main_data = new List<Patient_Record>();
            ReadFile();
        }
        private string _add_update;
        public string Add_Update
        {
            get { return _add_update; }
            set { _add_update = value; OnPropertyChanged("Add_Update"); }
        }
        private string _errorpos;
        public string ErrorPos
        {
            get { return _errorpos; }
            set { _errorpos = value; OnPropertyChanged("ErrorPos"); }
        }
        private string _error;
        public string Error
        {
            get { return _error; }
            set { _error = value; OnPropertyChanged("Error"); }
        }
        private Patient_Record _patient;
        public Patient_Record PatientRecord_data
        {
            get { return _patient; }
            set
            {
                _patient = value;

                OnPropertyChanged("PatientRecord_data");
            }
        }
        private Patient _selected_patient;
        public Patient Selected_Patient
        {
            get { return _selected_patient; }
            set
            {
                _selected_patient = value;
                if (Flag == 0 && Selected_Patient.Id != "")
                {
                    Console.WriteLine("ass");
                    foreach (var item2 in main_data)
                    {
                        if (item2.Id == Selected_Patient.Id)
                        {
                            Console.WriteLine("here");
                            PatientRecord_data = new Patient_Record
                            {
                                Fname = item2.Fname,
                                Id = Selected_Patient.Id,
                                DOB = item2.DOB,
                                Mobile = item2.Mobile,
                                Lname = item2.Lname,
                                Email = item2.Email
                            };
                            Add_Update = "Update";
                            //
                            break;
                        }
                    }
                    Console.WriteLine(Selected_Patient.Id);
                }
                else
                {
                    Flag = 0;
                    Console.WriteLine("weee");
                }
                OnPropertyChanged("Selected_Patient");
            }
        }


        /************************************************************************************************/

        private ObservableCollection<Patient> _display_data;
        private List<Patient_Record> _main_data;
        public List<Patient_Record> main_data
        {
            get { return _main_data; }
            set
            {
                _main_data = value;
                //OnPropertyChanged("display_data");
            }
        }
        public ObservableCollection<Patient> display_data
        {
            get { return _display_data; }
            set
            {
                _display_data = value;
                OnPropertyChanged("display_data");
            }
        }

        /*************************************************************************************************/

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new RelayCommand(AddExecute, CanAddExecute, true);
                }
                return _AddCommand;
            }
        }

        private void AddExecute(object parameter)
        {
            //Person obj = new Person();
            var passwordBox = parameter as PasswordBox;
            var password = passwordBox.Password;
            Console.WriteLine(password);
            if (checkDuplicate(PatientRecord_data.Fname, PatientRecord_data.Lname, PatientRecord_data.Email, PatientRecord_data.Mobile))
            {
                PatientRecord_data.Id = DateTime.Now.ToString("ddMMyyyyHHmmss");
                main_data.Add(PatientRecord_data);
                Patient temp_p = new Patient();
                temp_p.Fname = PatientRecord_data.Fname;
                temp_p.Mobile = PatientRecord_data.Mobile;
                temp_p.Id = PatientRecord_data.Id;
                display_data.Add(temp_p);
                if (Add_Update == "Add")
                {
                    string str = PatientRecord_data.Id + "#" + PatientRecord_data.Fname + "#" + PatientRecord_data.Lname + "#" + PatientRecord_data.Email + "#" + PatientRecord_data.Mobile + "#" + PatientRecord_data.DOB;
                    File.AppendAllText(@"D:\file.txt", str + Environment.NewLine);
                    ErrorPos = "67,0,0,480";
                    Error = "Registered Successfully!";
                }
                else
                {
                    glob_id = Selected_Patient.Id;
                    Console.WriteLine("hehehe2");
                    //var itemToRemove = display_data.Single(r => r.Id == Selected_Patient.Id);
                    //display_data.Remove(itemToRemove);
                    Flag = 1;
                    display_data.Remove(Selected_Patient);
                    Console.WriteLine("fefef2");
                    var itemToRemove2 = main_data.Single(r => r.Id == glob_id);
                    main_data.Remove(itemToRemove2);
                    overwrite();
                    ErrorPos = "67,0,0,480";
                    Error = "Record Updated.";
                    
                    Add_Update = "Add";
                    Selected_Patient = new Patient { Mobile = "", Id = "", Fname = "" };
                    PatientRecord_data = new Patient_Record { Email = "", Lname = "", Mobile = "", DOB = "", Id = "" };
                }
                PatientRecord_data = new Patient_Record();
            }
            else
                return;
            //Thread.Sleep(1000); 
        }



        private bool CanAddExecute(object parameter)
        {
            //if(PatientRecord_data.Email=="")Console.WriteLine("Asd");


            return true;

        }
        private ICommand _RegCommand;
        public ICommand RegCommand
        {
            get
            {
                if (_RegCommand == null)
                {
                    _RegCommand = new RelayCommand(RegExecute, CanRegExecute, true);
                }
                return _RegCommand;
            }
        }

        private void RegExecute(object parameter)
        {
            Selected_Patient = new Patient { Mobile = "", Id = "", Fname = "" };
            PatientRecord_data = new Patient_Record { Email = "", Lname = "", Mobile = "", DOB = "", Id = "" };
            Error = "";
            ErrorPos = "67,0,0,480";
            Add_Update = "Add";
            //Thread.Sleep(1000); 
        }



        private bool CanRegExecute(object parameter)
        {
            //if(PatientRecord_data.Email=="")Console.WriteLine("Asd");


            return true;

        }

        private ICommand _DelCommand;
        public ICommand DelCommand
        {
            get
            {
                if (_DelCommand == null)
                {
                    _DelCommand = new RelayCommand(DelExecute, CanDelExecute, true);
                }
                return _DelCommand;
            }
        }

        private void DelExecute(object parameter)
        {
            if (Selected_Patient.Id == "")
            {
                Error = "Select Item to be deleted.";
            }
            else
            {
                glob_id = Selected_Patient.Id;
                Console.WriteLine("hehehe");
                //var itemToRemove = display_data.Single(r => r.Id == Selected_Patient.Id);
                //display_data.Remove(itemToRemove);
                Flag = 1;
                display_data.Remove(Selected_Patient);
                Console.WriteLine("fefef");
                var itemToRemove2 = main_data.Single(r => r.Id == glob_id);
                main_data.Remove(itemToRemove2);
                overwrite();
                ErrorPos = "67,0,0,480";
                Error = "Deletion Successful";
                
                Add_Update = "Add";
                Selected_Patient = new Patient { Mobile = "", Id = "", Fname = "" };
                PatientRecord_data = new Patient_Record { Email = "", Lname = "", Mobile = "", DOB = "", Id = "" };
            }

            //Thread.Sleep(1000); 
        }

        private void overwrite()
        {
            using (StreamWriter newTask = new StreamWriter(@"D:\file.txt", false))
            {
                foreach (var item in main_data)
                {
                    string tempstr = item.Id + "#" + item.Fname + "#" + item.Lname + "#" + item.Email + "#" + item.Mobile + "#" + item.DOB;
                    newTask.WriteLine(tempstr);
                }

            }
            //throw new NotImplementedException();
        }



        private bool CanDelExecute(object parameter)
        {
            //if(PatientRecord_data.Email=="")Console.WriteLine("Asd");


            return true;

        }
        /***************************************************************************************************/


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}