using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

/*
 TODO:
    1. Hook up GUI elements to code
    2. Handle connection to the SQLite DB

 Flow:
    1. Implement SQLite connection
    2. Parse SQLite database
    3. Populate lists
    4. TBD
 */

namespace RagnaLogger
{
    public partial class reportViewer : Form
    {
        public reportViewer()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
