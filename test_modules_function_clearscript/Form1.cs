using Microsoft.ClearScript.V8;
using Microsoft.ClearScript;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.ClearScript.V8.V8ScriptEngine;
using Microsoft.ClearScript.JavaScript;

namespace test_modules_function_clearscript
{
    public partial class Form1 : Form
    {
        private V8ScriptEngine _engine;
        private string _nodePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "node_modules");

        public Form1()
        {
            InitializeComponent();
            string searchPath = string.Join(";", GetNodeModule());

            _engine = new V8ScriptEngine(V8ScriptEngineFlags.EnableDynamicModuleImports)
            {

                DefaultAccess = ScriptAccess.Full,
                SuppressExtensionMethodEnumeration = true,
                AllowReflection = true,
                DocumentSettings=new DocumentSettings()
                {
                    FileNameExtensions="js",
                    SearchPath =searchPath,
                    AccessFlags = DocumentAccessFlags.EnableFileLoading
                },
            };

            _engine.AddHostObject("Console", richTextBox1);
            _engine.Execute(
               new DocumentInfo
               {
                   Category = ModuleCategory.Standard
               },
               File.ReadAllText("Scripts/index.js")
               );
        }
        private List<string> GetNodeModule()
        {
            List<string> lstDirPackage = new List<string>();
            string[] directories = Directory.GetDirectories(_nodePath);
            foreach (var directory in directories)
            {
                //string packagePath = Path.Combine(directory, "package.json");
                lstDirPackage.Add(directory);
            }
            return lstDirPackage;
        }
    }
}
