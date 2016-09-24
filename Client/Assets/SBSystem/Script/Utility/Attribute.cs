using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SB
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GroupAttribute : Attribute
    {

        public string group;

        public GroupAttribute(string group)
        {
            this.group = group;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class NameAttribute : Attribute
    {

        public string name;

        public NameAttribute(string name)
        {
            this.name = name;
        }
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    [ComVisible(true)]
    public class VariableDecsAttribute : Attribute
    {

        public string name;

        public VariableDecsAttribute(string name)
        {
            this.name = name;
        }
    }


    [AttributeUsage(AttributeTargets.Field, Inherited = false)]
    [ComVisible(true)]
    public class FileSelectAttribute : Attribute
    {

        public string startPath;
        public string suffix;

        public FileSelectAttribute(string startPath, string suffix)
        {
            this.startPath = startPath;
            this.suffix = suffix;
        }
    }
}
