using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyProj.Model
{
    public class Patient  //notify to UI
    {
        private string fname, mobile, id;

        public String Fname
        {
            get
            { return fname; }
            set
            {
                fname = value;
                //OnPropertyChanged(Fname);
            }
        }

        public String Mobile
        {
            get
            { return mobile; }
            set
            {
                mobile = value;
                //OnPropertyChanged(Lname);
            }
        }

        public String Id
        {
            get
            { return id; }
            set
            {
                id = value;
            }
        }
    }
    public class Patient_Record  //notify to UI
    {
        private string fname, lname, id, email, mobile, dob;

        public String Fname
        {
            get
            { return fname; }
            set
            {
                fname = value;
                //OnPropertyChanged(Fname);
            }
        }

        public String Lname
        {
            get
            { return lname; }
            set
            {
                lname = value;
                //OnPropertyChanged(Lname);
            }
        }

        public String Id
        {
            get
            { return id; }
            set
            {
                id = value;
            }
        }
        public String Email
        {
            get
            { return email; }
            set
            {
                email = value;
            }
        }
        public String Mobile
        {
            get
            { return mobile; }
            set
            {
                mobile = value;
            }
        }
        public String DOB
        {
            get
            { return dob; }
            set
            {
                dob = value;
            }
        }
        
    }
    
}
