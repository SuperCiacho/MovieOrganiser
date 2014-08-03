using System;
using Microsoft.Win32;
using System.Security.Permissions;

namespace FilmWeb_Movie_Checker
{
    class MenuKontekstowe
    {
        #region AddToContext
        public static void AddToContex(string ext, string TypeName, string Description, string ID, string ExecutablePath)
        {
            RegistryKey regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName + @"\shell\" + ID, true);

            if (regKey == null) regKey = Registry.ClassesRoot.CreateSubKey(TypeName + @"\shell\" + ID);

            regKey.SetValue("", Description);
            regKey.Close();

            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName + @"\shell\" + ID + @"\command", true);

            if (regKey == null) regKey = Registry.ClassesRoot.CreateSubKey(TypeName + @"\shell\" + ID + @"\command");

            regKey.SetValue("", "\"" + ExecutablePath + "\" \"%1\"");
            regKey.Close();

            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey("." + ext, true);

            if (regKey != null)
            {
                TypeName = regKey.GetValue("").ToString();
            }
            else
            {
                regKey = Registry.ClassesRoot.CreateSubKey("." + ext);
                regKey.SetValue("", TypeName);
            }

            regKey.Close();

            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName + @"\shell\" + ID, true);

            if (regKey == null) regKey = Registry.ClassesRoot.CreateSubKey(TypeName + @"\shell\" + ID);

            regKey.SetValue("", Description);
            regKey.Close();

            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName + @"\shell\" + ID + @"\command", true);

            if (regKey == null) regKey = Registry.ClassesRoot.CreateSubKey(TypeName + @"\shell\" + ID + @"\command");
            

            regKey.SetValue("", "\"" + ExecutablePath + "\" \"%1\"");
            regKey.Close();

            Properties.Settings.Default.Context_Menu = true;
            Properties.Settings.Default.Save();
        }
        #endregion
        #region RemoveFromContext
        public static void RemoveFromContex(string ext, string TypeName, string ID)
        {
            RegistryKey regKey = null;

            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName, true);
            regKey = regKey.OpenSubKey("shell", true);
            regKey.DeleteSubKeyTree(ID, false);


            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey("." + ext, true);
            if (regKey != null)
                TypeName = regKey.GetValue("").ToString();
            regKey.Close();
            regKey = Registry.ClassesRoot;
            regKey = regKey.OpenSubKey(TypeName, true);
            regKey = regKey.OpenSubKey("shell", true);
            regKey.DeleteSubKeyTree(ID, false);

            Properties.Settings.Default.Context_Menu = false;
            Properties.Settings.Default.Save();

        }
        #endregion
    }
}
