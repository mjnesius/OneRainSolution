using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OneRainGraph
{
    class NoDataException:Exception
    {
        public NoDataException()
        {

        }

        public NoDataException(string _message)
            : base(_message)
        {
            // Initializes the variables to pass to the MessageBox.Show method.
            DialogResult dResult;
            string caption = "Error getting OneRain data";
            // Displays the MessageBox.
            dResult = MessageBox.Show(_message, caption);
        }
        public NoDataException(string _message, Exception _inner)
            : base(_message, _inner)
        {
           
        }
    }
}
