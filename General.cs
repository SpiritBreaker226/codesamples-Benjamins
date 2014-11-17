// Fonds the General Functions for this program

namespace BenjaminsListings
{
    public static class General
    {
        /**
         * Checks if a message should be appended to the newsletter going out
         * @type {String}
         */
        public static string checkToAppendMessage(int intMonth, int intFromDay, int intToDay, int intYear, string strMessageAppend, string strTemplate = "<tr><td colpan=\"4\"><div align=\"left\"><br/><label>{0}</label></td></tr>")
        {
            try 
            {
                string strMessageToSent = "";//holds what will be send to the 

                //checks if the date that the user wants is todays date
                if (DateTime.Today.Month == intMonth && DateTime.Today.Day >= intFromDay && DateTime.Today.Day <= intToDay && DateTime.Today.Year == intYear)
                    //appends the message that the user wants to be sent out to the template to the message
                    strMessageToSent = String.Format(strTemplate, strMessageAppend);

                return strMessageToSent;    
            }//end of try 
            catch (System.Exception e) 
            {
                throw e;
            }//end of catch
        }//end of appendMessage()
    }// end of class General 
}// end of namespaceBenjaminsListings
