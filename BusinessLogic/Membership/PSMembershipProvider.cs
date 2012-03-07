using System.Web.Security;
using System.Web.Configuration.Common;
using System.Collections.Specialized;
using System;
using System.Data;
using System.Data.SqlClient;
//using System.Configuration;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Web;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace BusinessLogic.Membership
{
    
    public class PSMembershipProvider : MembershipProvider
    {
        // Global connection string, generated password length, generic exception message, event log info.
        //

        private int newPasswordLength = 8;
        private string eventSource = "OdbcMembershipProvider";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private string connectionString;



        //
        // Used when determining encryption key values.
        //

        private MachineKeySection machineKey;

        //
        // If false, exceptions are thrown to the caller. If true,
        // exceptions are written to the event log.
        //

        private bool pWriteExceptionsToEventLog;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }


        //
        // System.Configuration.Provider.ProviderBase.Initialize Method
        //

        public override void Initialize(string name, NameValueCollection config)
        {
            //
            // Initialize values from web.config.
            //

            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "OdbcMembershipProvider";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Sample ODBC Membership provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(config["maxInvalidPasswordAttempts"].ToString());
            pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }
            
            //
            // Initialize SqlConnection.
            //

            //WebConfigurationManager.ConnectionStrings[config["connectionStringName"]];
            //WebConfigurationManager.OpenWebConfiguration
            string ConnectionStringSettings =
              WebConfigurationManager.ConnectionStrings[1].ToString();

            ////if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
            ////{
            ////    throw new ProviderException("Connection string cannot be blank.");
            ////}

            connectionString = ConnectionStringSettings;
            //machineKey = (MachineKeySection)
              //  WebConfigurationManager.GetSection("system.web/machineKey");
            //System.Web.Configuration.we
            // Get encryption and decryption key information from the configuration.
            //Configuration cfg =
            //  WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //machineKey = (MachineKeySection)WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).GetSection("system.web/machineKey");
             // <machineKey  validationKey="" decryptionKey="" validation="SHA1" />
            machineKey = new MachineKeySection();

            machineKey.ValidationKey = "C50B3C89CB21F4F1422FF158A5B42D0E8DB8CB5CDA1742572A487D9401E3400267682B202B746511891C1BAF47F8D25C07F6C39A104696DB51F17C529AD3CABE";
            machineKey.DecryptionKey = "8A9BE8FD67AF6979E7D20198CFEA50DD3D3799C77AF2B72F";
            machineKey.Validation = MachineKeyValidation.SHA1;
            
            if (machineKey.ValidationKey.Contains("AutoGenerate"))
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("Hashed or Encrypted passwords " +
                                                "are not supported with auto-generated keys.");
        }


        //
        // A helper function to retrieve config values from the configuration file.
        //

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }


        //
        // System.Web.Security.MembershipProvider properties.
        //


        private string pApplicationName;
        private bool pEnablePasswordReset;
        private bool pEnablePasswordRetrieval;
        private bool pRequiresQuestionAndAnswer;
        private bool pRequiresUniqueEmail;
        private int pMaxInvalidPasswordAttempts;
        private int pPasswordAttemptWindow;
        private MembershipPasswordFormat pPasswordFormat;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }


        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters;

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength;

        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression;

        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }

        //
        // System.Web.Security.MembershipProvider methods.
        //

        //
        // MembershipProvider.ChangePassword
        //

        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            if (!ValidateUser(username, oldPwd))
                return false;


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPwd, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Change password canceled due to new password validation failure.");


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users " +
                    " SET Password = @Password, Last_PasswordChange_Date = @LastPasswordChangedDate " +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(newPwd);
            cmd.Parameters.Add("@LastPasswordChangedDate", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }


        //
        // MembershipProvider.ChangePassword
        //

        public bool ChangePassword(string username, string newPwd)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users " +
                    " SET Password = @Password, Last_Password_Change_Date = @LastPasswordChangedDate " +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(newPwd);
            cmd.Parameters.Add("@LastPasswordChangedDate", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }




        //
        // MembershipProvider.ChangePasswordQuestionAndAnswer
        //

        public override bool ChangePasswordQuestionAndAnswer(string username,
                      string password,
                      string newPwdQuestion,
                      string newPwdAnswer)
        {
            if (!ValidateUser(username, password))
                return false;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users " +
                    " SET Password_Question = @Question, Password_Answer = @Answer" +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Question", SqlDbType.VarChar, 255).Value = newPwdQuestion;
            cmd.Parameters.Add("@Answer", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(newPwdAnswer);
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;


            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ChangePasswordQuestionAndAnswer");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return true;
            }

            return false;
        }



        //
        // MembershipProvider.CreateUser
        //

        public override MembershipUser CreateUser(string username,
                string password,
                string email,
                string passwordQuestion,
                string passwordAnswer,
                bool isApproved,
                object providerUserKey,
                out MembershipCreateStatus status
              )
        {
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }



            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO Users " +
                      " (PKID, Username, Password, Email, PasswordQuestion, " +
                      " PasswordAnswer, IsApproved," +
                      " Comment, CreationDate, LastPasswordChangedDate, LastActivityDate," +
                      " ApplicationName, IsLockedOut, LastLockedOutDate," +
                      " FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart, " +
                      " FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart)" +
                      " Values(@PKID, @Username, @Password, @Email, @PasswordQuestion,@PasswordAnswer , @IsApproved, " +
                      " @Comment, @CreationDate, @LastPasswordChangedDate, @LastActivityDate, " +
                      " @ApplicationName, @IsLockedOut, @LastLockedOutDate, @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart, " +
                      " @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart)", conn);

                cmd.Parameters.Add("@PKID", SqlDbType.UniqueIdentifier).Value = providerUserKey;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(password);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 128).Value = email;
                cmd.Parameters.Add("@PasswordQuestion", SqlDbType.VarChar, 255).Value = passwordQuestion;
                cmd.Parameters.Add("@PasswordAnswer", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(passwordAnswer);
                cmd.Parameters.Add("@IsApproved", SqlDbType.Bit).Value = isApproved;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255).Value = "";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastPasswordChangedDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;
                cmd.Parameters.Add("@IsLockedOut", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@LastLockedOutDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptCount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptWindowStart", SqlDbType.DateTime).Value = createDate;


                try
                {
                    conn.Open();

                    int recAdded = cmd.ExecuteNonQuery();

                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (SqlException e)
                {
                    if (WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "CreateUser");
                    }

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    conn.Close();
                }


                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }


            return null;
        }



        public MembershipUser CreateUser(string username,
                 string password,
                 string email,
                 string passwordQuestion,
                 string passwordAnswer,
                 bool isApproved,
                 object providerUserKey,
                 out MembershipCreateStatus status,
                 string firstName,
                 string middleName,
                 string lastName,
                 string DOB,
                 string gender,
                 int secretQuestionId,
                 string User_ID,
                 string authenticationCode)
        {
            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }



            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(username, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                if (providerUserKey == null)
                {
                    providerUserKey = Guid.NewGuid();
                }
                else
                {
                    if (!(providerUserKey is Guid))
                    {
                        status = MembershipCreateStatus.InvalidProviderUserKey;
                        return null;
                    }
                }

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("INSERT INTO Users " +
                      " (PKID, Username, Password, Email, PasswordQuestion, " +
                      " PasswordAnswer, IsApproved," +
                      " Comment, CreationDate, LastPasswordChangedDate, LastActivityDate," +
                      " ApplicationName, IsLockedOut, LastLockedOutDate," +
                      " FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart, " +
                      " FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart,firstName,middleName,lastName,DOB,gender,secretQuestionId,User_ID,AuthorizationCode)" +
                      " Values(@PKID, @Username, @Password, @Email, @PasswordQuestion,@PasswordAnswer , @IsApproved, " +
                      " @Comment, @CreationDate, @LastPasswordChangedDate, @LastActivityDate, " +
                      " @ApplicationName, @IsLockedOut, @LastLockedOutDate, @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart, " +
                      " @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart,@FirstName,@MiddleName,@LastName,@DOB,@Gender,@SecretQuestionId,@User_ID,@AuthorizationCode)", conn);

                cmd.Parameters.Add("@PKID", SqlDbType.UniqueIdentifier).Value = providerUserKey;
                cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(password);
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 128).Value = email;
                cmd.Parameters.Add("@PasswordQuestion", SqlDbType.VarChar, 255).Value = passwordQuestion;
                cmd.Parameters.Add("@PasswordAnswer", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(passwordAnswer);
                cmd.Parameters.Add("@IsApproved", SqlDbType.Bit).Value = isApproved;
                cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255).Value = "";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastPasswordChangedDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;
                cmd.Parameters.Add("@IsLockedOut", SqlDbType.Bit).Value = false;
                cmd.Parameters.Add("@LastLockedOutDate", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAttemptCount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAttemptWindowStart", SqlDbType.DateTime).Value = createDate;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptCount", SqlDbType.Int).Value = 0;
                cmd.Parameters.Add("@FailedPasswordAnswerAttemptWindowStart", SqlDbType.DateTime).Value = createDate;

                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = firstName;
                cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 50).Value = middleName;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = lastName;
                cmd.Parameters.Add("@DOB", SqlDbType.VarChar, 50).Value = DOB;
                cmd.Parameters.Add("@Gender", SqlDbType.VarChar, 50).Value = gender;
                cmd.Parameters.Add("@SecretQuestionId", SqlDbType.Int).Value = secretQuestionId;
                cmd.Parameters.Add("@User_ID", SqlDbType.VarChar, 50).Value = User_ID;
                cmd.Parameters.Add("@AuthorizationCode", SqlDbType.VarChar, 50).Value = authenticationCode;



                try
                {
                    conn.Open();

                    int recAdded = cmd.ExecuteNonQuery();

                    if (recAdded > 0)
                    {
                        status = MembershipCreateStatus.Success;
                    }
                    else
                    {
                        status = MembershipCreateStatus.UserRejected;
                    }
                }
                catch (SqlException e)
                {
                    if (WriteExceptionsToEventLog)
                    {
                        WriteToEventLog(e, "CreateUser");
                    }

                    status = MembershipCreateStatus.ProviderError;
                }
                finally
                {
                    conn.Close();
                }


                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }


            return null;
        }



        //
        // MembershipProvider.DeleteUser
        //

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Users " +
                   " WHERE Username = @Username AND Applicationname = @ApplicationName", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (deleteAllRelatedData)
                {
                    // Process commands to delete all data for the user in the database.
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }



        public bool DeleteSession(string sessionId)
        {
            return true;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM ASPStateTempSessions " +
                   " WHERE SessionId like @SessionId ", conn);

            cmd.Parameters.Add("@SessionId", SqlDbType.VarChar, 255).Value = sessionId + "%";

            int rowsAffected = 0;

            try
            {
                conn.Open();
                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "DeleteSession");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }



        //
        // MembershipProvider.GetAllUsers
        //

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users " +
                                              "WHERE ApplicationName = ?", conn);
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, Username, Email, PasswordQuestion," +
                         " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                         " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate,,FirstName,MiddleName, LastName, DOB, Gender, SecretQuestionId,User_ID,AuthorizationCode " +
                         " FROM Users " +
                         " WHERE ApplicationName = ? " +
                         " ORDER BY Username Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetAllUsers ");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return users;
        }


        //
        // MembershipProvider.GetNumberOfUsersOnline
        //

        public override int GetNumberOfUsersOnline()
        {

            TimeSpan onlineSpan = new TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0);
            DateTime compareTime = DateTime.Now.Subtract(onlineSpan);

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users " +
                    " WHERE LastActivityDate > ? AND ApplicationName = ?", conn);

            cmd.Parameters.Add("@CompareDate", SqlDbType.DateTime).Value = compareTime;
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            int numOnline = 0;

            try
            {
                conn.Open();

                numOnline = (int)cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetNumberOfUsersOnline");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            return numOnline;
        }



        //
        // MembershipProvider.GetPassword
        //

        public override string GetPassword(string username, string answer)
        {
            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            //if (PasswordFormat == MembershipPasswordFormat.Hashed)
            //{
            //    throw new ProviderException("Cannot retrieve Hashed passwords.");
            //}

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password, Password_Answer, Is_Locked_Out FROM Users " +
                  " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            string password = "";
            string passwordAnswer = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader.GetBoolean(2))
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    password = reader.GetString(0);
                    passwordAnswer = reader.GetString(1);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                UpdateFailureCount(username, "passwordAnswer");

                throw new MembershipPasswordException("Incorrect password answer.");
            }


            //if (PasswordFormat == MembershipPasswordFormat.Encrypted)
            //{
            password = UnEncodePassword(password);
            //}

            return password;
        }


        public string GetPassword(string username, string answer, out string message, string sessionID)
        {
            message = "";

            if (!EnablePasswordRetrieval)
            {
                throw new ProviderException("Password Retrieval Not Enabled.");
            }

            //if (PasswordFormat == MembershipPasswordFormat.Hashed)
            //{
            //    throw new ProviderException("Cannot retrieve Hashed passwords.");
            //}

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password, Password_Answer, Is_Locked_Out FROM Users " +
                  " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            string password = "";
            string passwordAnswer = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader.GetBoolean(2))
                    {
                        string strMessage = "";
                        ValidateUser(username, EndDec.Decrypt(reader.GetString(0)), out strMessage, sessionID);

                        if (strMessage.Trim().Length != 0)
                        {
                            message = strMessage;
                        }


                    }

                    password = reader.GetString(0);
                    passwordAnswer = reader.GetString(1);
                }
                else
                {
                    message = "The supplied user name is not found.";
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetPassword");


                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                UpdateFailureCount(username, "passwordAnswer");

                message = "Incorrect password answer.";
            }



            password = UnEncodePassword(password);

            return password;
        }



        //
        // MembershipProvider.GetUser(string, bool)
        //

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT User_ID, UserCode, Email, Secret_Question_ID," +
                 " '', Is_Approved, Is_Locked_Out,'1/1/2011', Last_Login_Date," +
                 " Last_Activity_Date, Last_Password_Change_Date, Last_Locked_Out_Date,Fname,Mname, Lname,'1/1/2011', 'M', Secret_Question_ID,User_ID" +
                 " FROM Users WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            MembershipUser u = null;
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);
                    reader.Close();
                    if (userIsOnline)
                    {
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users " +
                                  "SET Last_Activity_Date = @LastActivityDate " +
                                  "WHERE UserCode = @Username ", conn);

                        updateCmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                        //updateCmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                        updateCmd.ExecuteNonQuery();

                    }
                }

            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(String, Boolean)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return u;
        }









        //
        // MembershipProvider.GetUser(object, bool)
        //

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT User_ID, UserCode, Email, Password_Question," +
                  " Is_Approved, Is_Locked_Out, Created_Date, Last_Login_Date," +
                  " Last_Activity_Date, Last_Password_Changed_Date, Last_Locked_Out_Date,Fname, Lname,  DOB, Gender, Secret_Question_ID" +
                  " FROM Users WHERE User_ID = @PKID", conn);

            cmd.Parameters.Add("@PKID", SqlDbType.UniqueIdentifier).Value = providerUserKey;

            MembershipUser u = null;
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    u = GetUserFromReader(reader);

                    if (userIsOnline)
                    {
                        SqlCommand updateCmd = new SqlCommand("UPDATE Users " +
                                  "SET Last_Activity_Date = @LastActivityDate " +
                                  "WHERE User_ID = @PKID", conn);

                        updateCmd.Parameters.Add("@LastActivityDate", SqlDbType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("@PKID", SqlDbType.UniqueIdentifier).Value = providerUserKey;

                        updateCmd.ExecuteNonQuery();
                    }
                }

            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUser(Object, Boolean)");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return u;
        }


        //
        // GetUserFromReader
        //    A helper function that takes the current row from the SqlDataReader
        // and hydrates a MembershiUser from the values. Called by the 
        // MembershipUser.GetUser implementation.
        //

        private BusinessLogic.Membership.PSMembershipUser GetUserFromReader(SqlDataReader reader)
        {
            object providerUserKey = reader.GetValue(0);
            string username = reader.GetString(1);
            string email = reader.GetString(2);

            int passwordQuestion = 0;
            if (reader.GetValue(3) != DBNull.Value)
                passwordQuestion = reader.GetInt32(3);

            string comment = "";
            if (reader.GetValue(4) != DBNull.Value)
                comment = "";

            bool isApproved = reader.IsDBNull(5) ? false : reader.GetString(5) == "A" ? true : false;// reader.IsDBNull(5):.GetBoolean(5);
            bool isLockedOut = reader.IsDBNull(6) ? false : reader.GetBoolean(6);
            DateTime creationDate = DateTime.Now;// reader.GetDateTime(7);

            DateTime lastLoginDate = new DateTime();
            if (reader.GetValue(8) != DBNull.Value)
                lastLoginDate = reader.GetDateTime(8);

            DateTime lastActivityDate = reader.IsDBNull(9) ? DateTime.Now : reader.GetDateTime(9);
            DateTime lastPasswordChangedDate = reader.IsDBNull(10) ? DateTime.Now : reader.GetDateTime(10);// reader.GetDateTime(10);

            DateTime lastLockedOutDate = new DateTime();
            if (reader.GetValue(11) != DBNull.Value)
                lastLockedOutDate = reader.GetDateTime(11);

            string firstName = "";
            if (reader.GetValue(12) != DBNull.Value)
                firstName = reader.GetString(12);

            string middleName = "";
            if (reader.GetValue(13) != DBNull.Value)
                middleName = reader.GetString(13);

            string lastName = "";
            if (reader.GetValue(14) != DBNull.Value)
                lastName = reader.GetString(14);

            string DOB = "";
            //if (reader.GetValue(15) != DBNull.Value)
            //    DOB = reader.GetDateTime(15).ToString();

            string gender = "";
            if (reader.GetValue(16) != DBNull.Value)
                gender = reader.GetString(16);

            int secretQuestionId = 0;
            if (reader.GetValue(17) != DBNull.Value)
                secretQuestionId = reader.GetInt32(17);

            //string User_ID = "";
            //if (reader.GetValue(18) != DBNull.Value)
            //    User_ID = reader.GetString(18);


            int user_Id = 0;
            if (reader.GetValue(17) != DBNull.Value)
                user_Id = reader.GetInt32(18);




            PSMembershipUser u = new PSMembershipUser(this.Name,
                                                  username,
                                                  providerUserKey,
                                                  email,
                                                  passwordQuestion,
                                                  comment,
                                                  isApproved,
                                                  isLockedOut,
                                                  creationDate,
                                                  lastLoginDate,
                                                  lastActivityDate,
                                                  lastPasswordChangedDate,
                                                  lastLockedOutDate,
                                                  firstName, middleName, lastName, DOB, gender, secretQuestionId, user_Id);

            return u;
        }


        //
        // MembershipProvider.UnlockUser
        //

        public override bool UnlockUser(string username)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users " +
                                              " SET Is_Locked_Out = 0, Last_Locked_Out_Date = @LastLockedOutDate " +
                                              " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@LastLockedOutDate", SqlDbType.DateTime).Value = DateTime.Now;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            int rowsAffected = 0;

            try
            {
                conn.Open();

                rowsAffected = cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UnlockUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (rowsAffected > 0)
                return true;

            return false;
        }


        //
        // MembershipProvider.GetUserNameByEmail
        //

        public override string GetUserNameByEmail(string email)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT UserCode" +
                  " FROM Users WHERE Email_Primary = @Email ", conn);

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 128).Value = email;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            string username = "";

            try
            {
                conn.Open();

                username = (string)cmd.ExecuteScalar();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetUserNameByEmail");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }

            if (username == null)
                username = "";

            return username;
        }




        //
        // MembershipProvider.ResetPassword
        //

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("Password reset is not enabled.");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(username, "passwordAnswer");

                throw new ProviderException("Password answer required for password reset.");
            }

            string newPassword =
              System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters);


            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("Reset password canceled due to password validation failure.");


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password_Answer, Is_Locked_Out FROM Users " +
                  " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            int rowsAffected = 0;
            string passwordAnswer = "";
            SqlDataReader reader = null;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (reader.GetBoolean(1))
                        throw new MembershipPasswordException("The supplied user is locked out.");

                    passwordAnswer = reader.GetString(0);
                }
                else
                {
                    throw new MembershipPasswordException("The supplied user name is not found.");
                }

                if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
                {
                    UpdateFailureCount(username, "passwordAnswer");

                    throw new MembershipPasswordException("Incorrect password answer.");
                }

                SqlConnection con2 = new SqlConnection(connectionString);
                con2.Open();

                SqlCommand updateCmd = new SqlCommand("UPDATE Users " +
                    " SET Password = @Password, Last_Password_Changed_Date = @LastPasswordChangedDate" +
                    " WHERE UserCode = @Username AND Is_Locked_Out = 0", con2);

                updateCmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = EndDec.Encrypt(newPassword);
                updateCmd.Parameters.Add("@LastPasswordChangedDate", SqlDbType.DateTime).Value = DateTime.Now;
                updateCmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                //updateCmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                rowsAffected = updateCmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetPassword");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            if (rowsAffected > 0)
            {
                return newPassword;
            }
            else
            {
                throw new MembershipPasswordException("User not found, or user is locked out. Password not Reset.");
            }
        }


        //
        // MembershipProvider.UpdateUser
        //

        public override void UpdateUser(MembershipUser user)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Users " +
                    " SET Email_Primary = @Email, " +
                    " Is_Approved = @IsApproved" +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 128).Value = user.Email;
            //cmd.Parameters.Add("@Comment", SqlDbType.VarChar, 255).Value = user.Comment;
            cmd.Parameters.Add("@IsApproved", SqlDbType.Bit).Value = user.IsApproved;
            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = user.UserName;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;


            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                conn.Close();
            }
        }


        //
        // MembershipProvider.ValidateUser
        //

        public override bool ValidateUser(string username, string password)
        {
            bool isValid = false;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password, Is_Approved,Fname,Mname,Lname,User_ID,Is_Locked_Out,Status FROM Users " +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            SqlDataReader reader = null;
            bool isApproved = false;
            string pwd = "";



            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();
                    pwd = reader.GetString(0);
                    isApproved = reader.GetString(7) == "A" ? true : false;//reader.GetBoolean(1);

                    StringBuilder strB = new StringBuilder();

                    strB.Append(reader.IsDBNull(2) ? "" : reader.GetString(2));
                    strB.Append(reader.IsDBNull(3) ? "" : reader.GetString(3));
                    strB.Append(reader.IsDBNull(4) ? "" : reader.GetString(4));

                    System.Web.HttpContext.Current.Session["Employee_Name"] = strB.ToString();
                    //reader.GetString(2) + " " +
                    //reader.GetString(3) + " " +
                    //reader.GetString(4);

                    System.Web.HttpContext.Current.Session["User_ID"] = reader.GetInt32(5);

                    if (reader.IsDBNull(6) ? false : reader.GetBoolean(6))
                    {
                        //this.message = "Your user ID has been blocked, try after 10 minutes.";
                        return false;
                        //throw new Exception(this.message);
                    }

                }
                else
                {
                    return false;
                }

                reader.Close();

                if (CheckPassword(password, pwd))
                {
                    if (isApproved)
                    {
                        isValid = true;

                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET Last_Login_Date = @LastLoginDate" +
                                                                " WHERE UserCode = @Username ", conn);

                        updateCmd.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                        //updateCmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    conn.Close();

                    UpdateFailureCount(username, "password");
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ValidateUser");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return isValid;
        }




        //
        // MembershipProvider.ValidateUser
        //

        public bool ValidateUser(string username, string password, out string message, string newSessionId)
        {
            bool isValid = false;
            message = "";
            string oldSessionID = "";


            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password, Is_Approved,Fname,Mname,Lname,User_ID,Is_Locked_Out,Last_Locked_Out_Date,Session_ID,Status FROM Users " +
                    " WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            SqlDataReader reader = null;
            bool isApproved = false;
            string pwd = "";



            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();
                    pwd = reader.GetString(0);
                    isApproved = reader.GetString(9) == "A" ? true : false;//reader.GetBoolean(1);

                    string name = "";

                    StringBuilder strB = new StringBuilder();

                    strB.Append(reader.IsDBNull(2) ? "" : reader.GetString(2));
                    strB.Append(reader.IsDBNull(3) ? "" : reader.GetString(3));
                    strB.Append(reader.IsDBNull(4) ? "" : reader.GetString(4));

                    System.Web.HttpContext.Current.Session["Employee_Name"] = strB.ToString();
                    //( reader.IsDBNull(2) ? "" : reader.GetString(2) ) + 
                    //reader.IsDBNull(3) ? "" : reader.GetString(3); +" " +
                    //reader.IsDBNull(4)? "" : reader.GetString(4);

                    System.Web.HttpContext.Current.Session["User_ID"] = reader.GetInt32(5);

                    //bool is_Locked_Out = reader.IsDBNull(6)?false:reader.GetBoolean(6);

                    if (reader.IsDBNull(6) ? false : reader.GetBoolean(6))
                    {
                        //UnlockUser(string username);


                        if (!reader.IsDBNull(8))
                        {
                            DateTime dt = reader.GetDateTime(8);
                            TimeSpan ts = DateTime.Now.Subtract(dt);

                            if (ts.TotalMinutes > 10)
                            {
                                UnlockUser(username);
                            }
                            else
                            {
                                message = "Your user ID has been blocked, try after 10 minutes.";
                                return false;
                            }


                        }

                    }

                    if (!reader.IsDBNull(8))
                        oldSessionID = reader.GetString(8);


                }
                else
                {
                    message = "";
                    return false;
                }

                reader.Close();

                if (oldSessionID != newSessionId)
                {
                    DeleteSession(oldSessionID);
                }




                #region checkpassword
                if (CheckPassword(password, pwd))
                {
                    if (isApproved)
                    {
                        isValid = true;

                        SqlCommand updateCmd = new SqlCommand("UPDATE Users SET Last_Login_Date = @LastLoginDate,Session_ID = @SessionId" +
                                                                " WHERE UserCode = @Username ", conn);

                        updateCmd.Parameters.Add("@LastLoginDate", SqlDbType.DateTime).Value = DateTime.Now;
                        updateCmd.Parameters.Add("@SessionId", SqlDbType.VarChar, 50).Value = newSessionId;
                        updateCmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                        //updateCmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    conn.Close();

                    UpdateFailureCount(username, "password");
                }
                #endregion
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "User");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            return isValid;

        }





        //
        // UpdateFailureCount
        //   A helper method that performs the checks and updates associated with
        // password failure tracking.
        //

        private void UpdateFailureCount(string username, string failureType)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Failed_Password_Attempt_Count, " +
                                              "  Failed_Password_Attempt_WindowStart, " +
                                              "  Failed_Password_Answer_Attempt_Count, " +
                                              "  Failed_Password_Answer_Attempt_WindowStart " +
                                              "  FROM Users " +
                                              "  WHERE UserCode = @Username ", conn);

            cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
            //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            SqlDataReader reader = null;
            DateTime windowStart = new DateTime();
            int failureCount = 0;

            try
            {
                conn.Open();

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.HasRows)
                {
                    reader.Read();

                    if (failureType == "password")
                    {
                        failureCount = reader.GetInt32(0);
                        windowStart = reader.GetDateTime(1);
                    }

                    if (failureType == "passwordAnswer")
                    {
                        failureCount = reader.GetInt32(2);
                        windowStart = reader.GetDateTime(3);
                    }
                }

                reader.Close();

                DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    // First password failure or outside of PasswordAttemptWindow. 
                    // Start a new password failure count from 1 and a new window starting now.

                    if (failureType == "password")
                        cmd.CommandText = "UPDATE Users " +
                                          "  SET Failed_Password_Attempt_Count = @Count, " +
                                          "      Failed_Password_Attempt_WindowStart = @WindowStart " +
                                          "  WHERE UserCode = @Username ";

                    if (failureType == "passwordAnswer")
                        cmd.CommandText = "UPDATE Users " +
                                          "  SET Failed_Password_Answer_Attempt_Count = @Count, " +
                                          "      Failed_Password_Answer_Attempt_WindowStart = @WindowStart " +
                                          "  WHERE UserCode = @Username  ";

                    cmd.Parameters.Clear();

                    cmd.Parameters.Add("@Count", SqlDbType.Int).Value = 1;
                    cmd.Parameters.Add("@WindowStart", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                    //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                    if (cmd.ExecuteNonQuery() < 0)
                        throw new ProviderException("Unable to update failure count and window start.");
                }
                else
                {
                    if (failureCount++ >= MaxInvalidPasswordAttempts)
                    {
                        // Password attempts have exceeded the failure threshold. Lock out
                        // the user.

                        cmd.CommandText = "UPDATE Users " +
                                          "  SET Is_Locked_Out = @IsLockedOut, Last_Locked_Out_Date = @LastLockedOutDate " +
                                          "  WHERE UserCode = @Username ";

                        cmd.Parameters.Clear();

                        cmd.Parameters.Add("@IsLockedOut", SqlDbType.Bit).Value = true;
                        cmd.Parameters.Add("@LastLockedOutDate", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                        //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                        if (cmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to lock out user.");
                    }
                    else
                    {
                        // Password attempts have not exceeded the failure threshold. Update
                        // the failure counts. Leave the window the same.

                        if (failureType == "password")
                            cmd.CommandText = "UPDATE Users " +
                                              "  SET Failed_Password_Attempt_Count = @Count" +
                                              "  WHERE UserCode = @Username ";

                        if (failureType == "passwordAnswer")
                            cmd.CommandText = "UPDATE Users " +
                                              "  SET Failed_Password_Answer_Attempt_Count = @Count" +
                                              "  WHERE UserCode = @Username ";

                        cmd.Parameters.Clear();

                        cmd.Parameters.Add("@Count", SqlDbType.Int).Value = failureCount;
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar, 255).Value = username;
                        //cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

                        if (cmd.ExecuteNonQuery() < 0)
                            throw new ProviderException("Unable to update failure count.");
                    }
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "UpdateFailureCount");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }
        }


        //
        // CheckPassword
        //   Compares password values based on the MembershipPasswordFormat.
        //

        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;


            pass2 = EndDec.Decrypt(dbpassword);
            //switch (PasswordFormat)
            //{
            //    case MembershipPasswordFormat.Encrypted:
            //        pass2 = UnEncodePassword(dbpassword);
            //        break;
            //    case MembershipPasswordFormat.Hashed:
            //        pass1 = EncDec.Encrypt(password);
            //        break;
            //    default:
            //        break;
            //}

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }


        //
        // EncodePassword
        //   Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
        //

        //private string EncodePassword(string password)
        //{
        //    string encodedPassword = password;

        //    switch (PasswordFormat)
        //    {
        //        case MembershipPasswordFormat.Clear:
        //            break;
        //        case MembershipPasswordFormat.Encrypted:
        //            encodedPassword =
        //              Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
        //            break;
        //        case MembershipPasswordFormat.Hashed:
        //            HMACSHA1 hash = new HMACSHA1();
        //            hash.Key = HexToByte(machineKey.ValidationKey);
        //            encodedPassword =
        //              Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
        //            break;
        //        default:
        //            throw new ProviderException("Unsupported password format.");
        //    }

        //    return encodedPassword;
        //}


        //
        // UnEncodePassword
        //   Decrypts or leaves the password clear based on the PasswordFormat.
        //

        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            return EndDec.Decrypt(password);

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        //
        // HexToByte
        //   Converts a hexadecimal string to a byte array. Used to convert encryption
        // key values from the configuration.
        //

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }


        //
        // MembershipProvider.FindUsersByName
        //

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users " +
                      "WHERE Username LIKE @UsernameSearch AND ApplicationName = @ApplicationName", conn);
            cmd.Parameters.Add("@UsernameSearch", SqlDbType.VarChar, 255).Value = usernameToMatch;
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = pApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, Username, Email, PasswordQuestion," +
                  " Comment, Status, IsLockedOut, CreationDate, LastLoginDate," +
                  " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " +
                  " FROM Users " +
                  " WHERE Username LIKE ? AND ApplicationName = ? " +
                  " ORDER BY Username Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByName");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return users;
        }

        //
        // MembershipProvider.FindUsersByEmail
        //

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Count(*) FROM Users " +
                                              "WHERE Email LIKE @EmailSearch AND ApplicationName = @ApplicationName", conn);
            cmd.Parameters.Add("@EmailSearch", SqlDbType.VarChar, 255).Value = emailToMatch;
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;

            MembershipUserCollection users = new MembershipUserCollection();

            SqlDataReader reader = null;
            totalRecords = 0;

            try
            {
                conn.Open();
                totalRecords = (int)cmd.ExecuteScalar();

                if (totalRecords <= 0) { return users; }

                cmd.CommandText = "SELECT PKID, Username, Email, PasswordQuestion," +
                         " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," +
                         " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " +
                         " FROM Users " +
                         " WHERE Email LIKE ? AND ApplicationName = ? " +
                         " ORDER BY Username Asc";

                reader = cmd.ExecuteReader();

                int counter = 0;
                int startIndex = pageSize * pageIndex;
                int endIndex = startIndex + pageSize - 1;

                while (reader.Read())
                {
                    if (counter >= startIndex)
                    {
                        MembershipUser u = GetUserFromReader(reader);
                        users.Add(u);
                    }

                    if (counter >= endIndex) { cmd.Cancel(); }

                    counter++;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "FindUsersByEmail");

                    throw new ProviderException(exceptionMessage);
                }
                else
                {
                    throw e;
                }
            }
            finally
            {
                if (reader != null) { reader.Close(); }

                conn.Close();
            }

            return users;
        }


        //
        // WriteToEventLog
        //   A helper function that writes exception detail to the event log. Exceptions
        // are written to the event log as a security measure to avoid private database
        // details from being returned to the browser. If a method does not return a status
        // or boolean indicating the action succeeded or failed, a generic exception is also 
        // thrown by the caller.
        //

        private void WriteToEventLog(Exception e, string action)
        {
            return;
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }


        //public override bool ValidateUser(string username, string password)
        //{
        //    PortalDataManager.Portal.BLLPortalRegistration preg = new PortalDataManager.Portal.BLLPortalRegistration();
        //    PortalDataSets.DSRegistration dsreg = preg.LoadPatientSignIn(username, password);

        //    if (dsreg == null)
        //        return false;
        //    else
        //    {
        //        System.Web.HttpContext.Current.Session["User_ID"] = dsreg.Tables[PortalDataSets.DSRegistration.TABLE_PORTAL_REGISTRATION].Rows[0][ PortalDataSets.DSRegistration.FIELD_User_ID].ToString();

        //        //System.Web.HttpContext.Current.Session["Demographic"] = "True";
        //        //System.Web.HttpContext.Current.Session["Problems"] = "True";
        //        //System.Web.HttpContext.Current.Session["Allergies"] = "True";
        //        //System.Web.HttpContext.Current.Session["Procedure"] = "True";
        //        //System.Web.HttpContext.Current.Session["Medications"] = "True";
        //        //System.Web.HttpContext.Current.Session["Immunization"] = "True";
        //        //System.Web.HttpContext.Current.Session["VitalSigns"] = "True";
        //        //System.Web.HttpContext.Current.Session["Appointments"] = "True";
        //        //System.Web.HttpContext.Current.Session["Problem"] = "True";
        //        //System.Web.HttpContext.Current.Session["REVIEW"] = "True";
        //        DataRow row = dsreg.Tables[PortalDataSets.DSRegistration.TABLE_PORTAL_REGISTRATION].Rows[0];
        //        System.Web.HttpContext.Current.Session["Employee_Name"] =
        //            row[PortalDataSets.DSRegistration.FIELD_Fname].ToString() + " " +
        //            row[PortalDataSets.DSRegistration.FIELD_Mname].ToString() +  " " +
        //            row[PortalDataSets.DSRegistration.FIELD_Lname].ToString();


        //        return true;
        //    }



        //    //try
        //    //{
        //    //    prxWsWBEmployee.wsWBEmployee ObjPrx = new prxWsWBEmployee.wsWBEmployee();

        //    //    prxWsWBEmployee.SoapRegCodeHeader regcode = new prxWsWBEmployee.SoapRegCodeHeader();
        //    //    regcode.RegCode = System.Configuration.ConfigurationManager.AppSettings["RegistrationCode"].ToString();
        //    //    if (SystemIP != string.Empty)
        //    //        regcode.IP = SystemIP;
        //    //    else
        //    //        regcode.IP = "127.0.0.1";// Request.UserHostAddress;
        //    //    ObjPrx.SoapRegCodeHeaderValue = regcode;

        //    //    ObjPrx.Url = System.Configuration.ConfigurationManager.AppSettings["PCISWebServiceURL"].ToString() + "wsWBEmployee.asmx";

        //    //    prxWsWBEmployee.Employee emp = ObjPrx.ValidateUser(username, password);

        //    //    return emp.IsAuthenticated;

        //    //    //throw new Exception("The method or operation is not implemented.");
        //    //}

        //    //catch (SoapHeaderException soapEx)
        //    //{
        //    //    throw soapEx;
        //    //}

        //    //catch (Exception oEx)
        //    //{
        //    //    throw oEx;
        //    //}
        //}
    }
}
