using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Code
{
    public enum FileType
    {
        dir,
        file,
    }
    public class FileSystem
    {
        private FileType type;

        private int size;

        public int Size
        {
            get
            {
                if (subs != null)
                    return size + subs.Sum(x => x.Size);
                else
                    return size;
            }
        }
        
        private string name;

        private List<FileSystem> subs;

        private FileSystem parent;

        public FileSystem()
        {
            type = FileType.dir;
            name = "";
            size = 0;
            parent = null;
            subs = new List<FileSystem>();
            subs.Add(new FileSystem("/",FileType.dir,0,this));
        }

        public FileSystem ComputeCommand(string input)
        {
            FileSystem fs = this;
            if (string.IsNullOrEmpty(input))
                return fs;
            var parts = input.Split(" ");
            if (parts[0] == "$")
            {
                if (parts[1] == "cd")
                {
                    if (parts[2] == "..")
                    {
                        return parent;
                    }
                    else
                    {
                        return subs.First(x => x.name == parts[2]);
                    }
                }
                if (parts[1] == "ls")
                    subs = new();
            }
            else
            {
                if (parts[0] == "dir")
                {
                    subs.Add(new FileSystem(parts[1], FileType.dir, 0,this));
                }
                else
                {
                    subs.Add(new FileSystem(parts[1], FileType.file, int.Parse(parts[0]),this));
                }
            }

            return fs;
        }

        public int GetSize(int max)
        {
            List<int> flat = new List<int>();
            flat = Flatsize(flat);
            flat.Sort();
            for (int i = 0; i < flat.Count(); i++)
            {
                if (flat[i] > max)
                    flat.RemoveRange(i, flat.Count() - i);
            }
            return flat.Sum();
        }

        public int FindSmallestToDelete(int available,int needed)
        {
            int ret=0;

            int over = needed - (available -this.Size);
            List<int> flat = new List<int>();
            flat = Flatsize(flat);
            ret = flat.Where(x => x > over).Min();


            return ret;
        }

        private List<int> Flatsize(List<int> sofar)
        {
            if (FileType.file == this.type)
                return sofar;
            else
            {
                if(this.Size!=0)
                    sofar.Add(this.Size);

                foreach(var s in subs)
                    sofar = s.Flatsize(sofar);
            }

            return sofar;
        }

        public FileSystem ReturnToRoot()
        {
            if (parent == null)
                return this;
            else
                return parent.ReturnToRoot();
        }

        public FileSystem(string name, FileType type, int size,FileSystem fs)
        {
            this.name = name;
            this.type = type;
            this.size = size;
            parent = fs;
        }

    }
}
