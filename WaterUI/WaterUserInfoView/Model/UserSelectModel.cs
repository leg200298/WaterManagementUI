using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterUserInfoView.Model
{
    public class UserSelectModel
    {
        private string _name;
        private string _phone;
        private string _address;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }     
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }


    }
}
