using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using ContactManager.Shared;

namespace ContactManager.App;

public partial class Form1 : Form
{
    private List<Contact> _contacts = new List<Contact>();
    private readonly List<IPluginable> _plugins = new List<IPluginable>();

    public Form1()
    {
        InitializeComponent();
        contactBindingSource.DataSource = _contacts;
        LoadPlugins();
    }

    private void LoadPlugins()
    {
        string pluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
        if (!Directory.Exists(pluginsPath))
        {
            Directory.CreateDirectory(pluginsPath);
            return;
        }

        foreach (string file in Directory.GetFiles(pluginsPath, "*.dll"))
        {
            Assembly assembly = Assembly.LoadFrom(file);
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(IPluginable).IsAssignableFrom(type) && !type.IsInterface)
                {
                    IPluginable? plugin = Activator.CreateInstance(type) as IPluginable;
                    if (plugin != null)
                    {
                        _plugins.Add(plugin);
                        AddPluginMenuItems(plugin);
                    }
                }
            }
        }
    }

    private void AddPluginMenuItems(IPluginable plugin)
    {
        // Add Save menu item
        ToolStripMenuItem saveMenuItem = new ToolStripMenuItem(plugin.Format);
        saveMenuItem.Click += (s, e) =>
        {
            using SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = $"{plugin.Format} files|*.{plugin.Format.ToLower()}";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                plugin.Save(_contacts, dialog.FileName);
            }
        };
        saveToToolStripMenuItem.DropDownItems.Add(saveMenuItem);

        // Add Load menu item
        ToolStripMenuItem loadMenuItem = new ToolStripMenuItem(plugin.Format);
        loadMenuItem.Click += (s, e) =>
        {
            using OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = $"{plugin.Format} files|*.{plugin.Format.ToLower()}";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _contacts.Clear();
                _contacts.AddRange(plugin.Load(dialog.FileName));
                contactBindingSource.ResetBindings(false);
            }
        };
        loadFromToolStripMenuItem.DropDownItems.Add(loadMenuItem);

        // Add Help menu item
        ToolStripMenuItem helpMenuItem = new ToolStripMenuItem(plugin.Format);
        helpMenuItem.Click += (s, e) =>
        {
            var attribute = plugin.GetType().GetCustomAttribute<InfoAttribute>();
            if (attribute != null)
            {
                MessageBox.Show($"Author: {attribute.Author}", $"About {plugin.Format} Plugin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        };
        helpToolStripMenuItem.DropDownItems.Add(helpMenuItem);
    }

    private void SaveXmlToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "XML files|*.xml";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            using FileStream stream = File.Create(dialog.FileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Contact>));
            serializer.Serialize(stream, _contacts);
        }
    }

    private void LoadXmlToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "XML files|*.xml";
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            using FileStream stream = File.OpenRead(dialog.FileName);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Contact>));
            _contacts.Clear();
            _contacts.AddRange((List<Contact>)serializer.Deserialize(stream)!);
            contactBindingSource.ResetBindings(false);
        }
    }

    private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}
