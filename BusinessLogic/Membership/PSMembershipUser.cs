using System.Web.Security;
//using System.Configuration.Provider;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace BusinessLogic.Membership
{
    
    public class PSMembershipUser : MembershipUser
    {

        private string _MRNo;
        public string MRNo
        {
            get
            {
                return _MRNo;
            }
            set
            {
                _MRNo = value;
            }
        }


        private string _firstName;
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }


        private string _middleName;
        public string MiddleName
        {
            get
            {
                return _middleName;
            }
            set
            {
                _middleName = value;
            }
        }

        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }


        private string _DOB;
        public string DOB
        {
            get
            {
                return _DOB;
            }
            set
            {
                _DOB = value;
            }
        }

        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
            }
        }

        private int _secretQuestion_ID;
        public int SecretQuestion_ID
        {
            get
            {
                return _secretQuestion_ID;
            }
            set
            {
                _secretQuestion_ID = value;
            }
        }


        private int _user_ID;
        public int User_ID
        {
            get
            {
                return _user_ID;
            }
            set
            {
                _user_ID = value;
            }
        }





        public PSMembershipUser()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public PSMembershipUser(String Name,
                                    String username,
                                    Object providerUserKey,
                                    String email,
                                    int passwordQuestion,
                                    String comment,
                                    Boolean isApproved,
                                    Boolean isLockedOut,
                                    DateTime creationDate,
                                    DateTime lastLoginDate,
                                    DateTime lastActivityDate,
                                    DateTime lastPasswordChangedDate,
                                    DateTime lastLockedOutDate,
                                    string firstName,
                                    string middleName,
                                    string lastName,
                                    string DOB,
                                    string Gender,
                                    int secretQuestion_ID,
                                    int user_ID
                                    ) :

            base(Name,
                                                username,
                                                providerUserKey,
                                                email,
                                                passwordQuestion.ToString(),
                                                comment,
                                                isApproved,
                                                isLockedOut,
                                                creationDate,
                                                lastLoginDate,
                                                lastActivityDate,
                                                lastPasswordChangedDate,
                                                lastLockedOutDate)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _DOB = DOB;
            _gender = Gender;
            _secretQuestion_ID = secretQuestion_ID;
            _user_ID = user_ID;


        }


    }
}
