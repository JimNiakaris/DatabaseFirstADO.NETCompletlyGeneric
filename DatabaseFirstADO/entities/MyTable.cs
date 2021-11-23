using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstADO.Entities
{
    class MyTable
    {
        public int Id { get; set; }
        public int MyNumber { get; set; }
        public string MyString { get; set; }
        public string MyString2 { get; set; }

        public MyTable()
        {

        }

        public MyTable(int Id, int MyNumber, string MyString, string MyString2)
        {
            this.Id = Id;
            this.MyNumber = MyNumber;
            this.MyString = MyString;
            this.MyString2 = MyString2;
        }

        public override string ToString()
        {
            string result = $"ID: {Id}, my_number: {MyNumber}, " +
                                                            $"my_string:{MyString}, " +
                                                            $"my_string2: {MyString2}";
            return(result);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
