using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Testing
{
    class RE
    {
        public bool regexPhone(String phone)
        {
            bool IsValid = false;
            Regex r = new Regex(@"(?:\(?[2-9](?(?=1)1[02-9]|(?(?=0)0[1-9]|\d{2}))\)?\D{0,3})(?:\(?[2-9](?(?=1)1[02-9]|\d{2})\)?\D{0,3})\d{4}");
            if (r.IsMatch(phone))
                IsValid = true;

            return IsValid;
        }
    }
}
