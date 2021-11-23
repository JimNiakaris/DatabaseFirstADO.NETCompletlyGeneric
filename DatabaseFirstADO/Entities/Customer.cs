using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstADO.Entities
{
    class Customer
    {
        // Customers
        /* Id
         * FirstName
         * LastName
         * Email
         * DateOfBirth
         * LandLineTel
         * MobileTel
         */
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LandLineTel { get; set; }
        public string MobileTel { get; set; }

        public Customer()
        {

        }

        public Customer(int iD, string firstName, string lastName, string email, DateTime dateOfBirth, string landLineTel, string mobileTel)
        {
            Id = iD;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            DateOfBirth = dateOfBirth;
            LandLineTel = landLineTel;
            MobileTel = mobileTel;
        }

        public  override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Id: {Id}").Append($" First Name: {FirstName}").Append($" Last Name: {LastName}")
                .Append($" Email: {Email}")
                .Append($" Date Of Birth: {DateOfBirth}")
                .Append($" Land Line Telephone: {LandLineTel}")
                .Append($" Mobile Telephone: {MobileTel}");
            return (sb.ToString());
        }


    }
}
