// Main File to run the send out email program 

namespace BenjaminsListings
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            DateTime start = DateTime.Now;

            try
            {
                StringBuilder sbEmailBody = new StringBuilder();
                string strEmailBodyFile = "EmailBody.txt";//holds the file name of body for this email if there is one
                string strSubject = "Benjamin's Park Memorial Chapel: ";//holds the file name of body for this email if there is one

                //checks if there is a file if so then use that file instead
                if (File.Exists(strEmailBodyFile))
                {
                    #region Send Custom Email Body

                    Console.WriteLine("Reading email body from file");

                    //uses the reader to open the email body and read it
                    using (StreamReader rwOpenTemplate = new StreamReader(strEmailBodyFile))
                    {
                        int intCurrentLine = 0;//holds the current line being read

                        //goes around for each line and adds it to the email body
                        while (!rwOpenTemplate.EndOfStream)
                        {
                            //checks if it is the first line if not then read the line 
                            //as the first live is the subject
                            if (intCurrentLine > 0)
                                sbEmailBody.Append(rwOpenTemplate.ReadLine());
                            else
                                strSubject = rwOpenTemplate.ReadLine();

                            intCurrentLine++;
                        }//end of while loop
                    }//end of using

                    //deletes the file from the server so that normal email can be sent next time
                    File.Delete(strEmailBodyFile);

                    #endregion
                }//end of if
                else
                {
                    Console.WriteLine("Compose Email Html Head..");

                    #region Html head

                    //Adds the Message for a Hoiday

                    sbEmailBody.Append(General.checkToAppendMessage(4, 13, 16, 2014, "<strong>Benjamin's Park Memorial Chapel</strong> will be closed for Passover on Tuesday and Wednesday. We will re-open after sundown on Wednesday evening, April 16. For immediate assistance, please call (416) 663-9060.", "<div style=\"width:532px;\"><label><strong>{0}<br/><br/></strong></label></div>"));
                    sbEmailBody.Append(General.checkToAppendMessage(4, 18, 22, 2014, "<strong>Benjamin's Park Memorial Chapel</strong> will be closed for Passover on Tuesday and Wednesday. We will re-open after sundown on Wednesday evening, April 16. For immediate assistance, please call (416) 663-9060.", "<div style=\"width:532px;\"><label><strong>{0}<br/><br/></strong></label></div>"));
                    sbEmailBody.Append(General.checkToAppendMessage(6, 2, 5, 2014, "<strong>Benjamin's</strong>  will be closed for the Festival of Shavuot on Wednesday and Thursday and will re-open after sundown on Thursday  evening.  If a death occurs, please call (416) 663-9060.", "<div style=\"width:532px;\"><label><strong>{0}<br/><br/></strong></label></div>"));

                    Console.WriteLine("Compose Shiva Html");

                    //Adds the Shavas for this years hoilday

                    sbEmailBody.Append(General.checkToAppendMessage(4, 11, 14, 2014, "All <strong>shivas</strong> conclude with the onset of Passover on  Monday evening."));
                    sbEmailBody.Append(General.checkToAppendMessage(4, 17, 22, 2014, "All <strong>shivas</strong> begin after the conclusion of Passover on Tuesday evening April 22."));
                    sbEmailBody.Append(General.checkToAppendMessage(6, 2, 3, 2014, "All <strong>shivas</strong> conclude on Tuesday with the onset of Shavuot."));

                    #endregion
                }//end of if

                #region send Emails

                Console.WriteLine("Sending emails..");

                MailAddress from = new MailAddress("");

                // goes around for each mail address found in the database and sends it out to that person
                foreach (MailAddress ma in mailAddresses)
                {
                    MailMessage emailUsers = new MailMessage(from, ma);
                    emailUsers.Subject = strSubject;
                    emailUsers.Body = sbEmailBody.ToString();
                    emailUsers.Priority = MailPriority.Normal;
                    emailUsers.IsBodyHtml = true;

                    try
                    {
                        SmtpClient client = new SmtpClient();
                        client.Host = "";
                        client.Send(emailUsers);
                        client.Credentials = CredentialCache.DefaultNetworkCredentials;
                    }//end of try
                    catch (Exception ex)
                    {
                        emailUsers.Dispose();
                    }//end of catch
                }//end of foreach

                #endregion
            }//end of try
            catch (Exception ex)
            {
                #region General Support

                String strSorrySubject = "Cannot create e-Listings";
                String strSorryBody = "Hi,<br/><br/>It looks like e-Listings did not go out";

                emailGeneral.Subject = strSorrySubject;
                emailGeneral.Body = strSorryBody + "<br/>" + ex.Message + ex.StackTrace;
                emailGeneral.Priority = MailPriority.High;
                emailGeneral.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Host = "";
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Send(emailGeneral);

                emailGeneral.Dispose();
                Console.WriteLine("General Error Email sent to: Support");
                General.SendErrorToAdmin(ex);

                #endregion
            }//end of catch
        }//end of Main
    }//end of class
}//end of namespace