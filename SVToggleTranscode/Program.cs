using System;
using System.IO;
using System.Windows.Forms;

namespace SVToggleTranscode
{
    static class Program
    {
        static void Main()
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();

                using (var file = File.Open(args.Length > 1 ? args[1] : "WindomSV_LV.exe", FileMode.Open, FileAccess.ReadWrite))
                {
                    file.Position = 0x189E5A;
                    int instruction = file.ReadByte();
                    file.Position = 0x189E5A;

                    if (instruction == 0x75)
                    {
                        file.Write(new byte[] { 0x90, 0x90 }, 0, 2);
                    }
                    else if (instruction == 0x90)
                    {
                        file.Write(new byte[] { 0x75, 0x05 }, 0, 2);
                    }
                }

                MessageBox.Show("Transcode toggled", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
