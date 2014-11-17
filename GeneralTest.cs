// does the unit testing to check if there is amesage that needs to be attach today

namespace BenjaminsListings
{
    public class GeneralTest
    {
        [Fact]
        public void appendMessage()
        {
            //should not contain test as the date is in the past
            Assert.DoesNotContain("Test", General.checkToAppendMessage(4, 8, 16, 2013, "Test"));
        }//end of appendMessage()

        [Fact]
        public void appendMessageTodays()
        {
            //should always contain test as the date will always be todays date
            Assert.Contains("Test", General.checkToAppendMessage(DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Day, DateTime.Today.Year, "Test"));
        }//end of appendMessageTodays()

        [Fact]
        public void appendMessageTodaysChangingTemplate()
        {
            //should always contain full string <td>Testing</td> as the template has been change to use a new tempalte
            Assert.Contains("<td>Testing</td>", General.checkToAppendMessage(DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Day, DateTime.Today.Year, "Testing", "<td>{0}</td>"));
        }//end of appendMessageTodaysChangingTemplate()

        [Fact]
        public void appendMessageTodaysChangingNoTemplate()
        {
            //should not have anything as the template was remove
            Assert.DoesNotContain("<td>Testing</td>", General.checkToAppendMessage(DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Day, DateTime.Today.Year, "Testing", ""));
        }//end of appendMessageTodaysChangingTemplate()

        [Fact]
        public void appendMessageTodaysToManyNumberingTemplate()
        {
            //should always throw an error if there is more then 1 format in the template
            Assert.Throws<System.FormatException>(
                delegate 
                {
                    General.checkToAppendMessage(DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Day, DateTime.Today.Year, "Testing", "{1}");
                }//end of delegate
            );
        }//end of appendMessageTodaysChangingTemplate()
    }//end of class GeneralTest
}//end of namespace BenjaminsListings
