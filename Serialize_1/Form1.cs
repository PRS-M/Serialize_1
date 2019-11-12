using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Serialize_1
{
    public partial class Form1 : Form
    {
        Guy joe;
        Guy bob;
        int bank = 100;

        public void UpdateForm()
        {
            joesCashLabel.Text = joe.Name + " has $" + joe.Cash;
            bobsCashLabel.Text = bob.Name + " has $" + bob.Cash;
            bankCashLabel.Text = "The bank has $" + bank;
        }

        public Form1()
        {
            InitializeComponent();

            joe = new Guy() { Cash = 50, Name = "Joe" };
            bob = new Guy() { Cash = 100, Name = "Bob" };

            UpdateForm();
        }

        private void giveButton_Click(object sender, EventArgs e)
        {
            if (bank >= 10)
            {
                bank -= joe.ReceiveCash(10);
                UpdateForm();
            }
            else
            {
                MessageBox.Show("The bank is out money.", "BANK ALERT!");
            }
        }

        private void receiveButton_Click(object sender, EventArgs e)
        {
            bank += bob.GiveCash(5);
            UpdateForm();
        }

        private void joeGivesToBob_Click(object sender, EventArgs e)
        {
            bob.ReceiveCash(joe.GiveCash(10));
            UpdateForm();
        }

        private void bobGivesToJoe_Click(object sender, EventArgs e)
        {
            joe.ReceiveCash(bob.GiveCash(5));
            UpdateForm();
        }

        private void saveJoeButton_Click(object sender, EventArgs e)
        {
            using (Stream output = File.Create("Guy_File.dat"))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(output, joe);
            }
        }

        private void loadJoeButton_Click(object sender, EventArgs e)
        {
            try
            {
                using (Stream input = File.OpenRead("Guy_File.dat"))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    joe = (Guy)formatter.Deserialize(input);
                }
                UpdateForm();
            }
            catch
            {
                MessageBox.Show("No file to load!");
            }
        }
    }
}
