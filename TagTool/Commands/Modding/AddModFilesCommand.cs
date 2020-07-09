﻿using TagTool.Cache;
using System.Collections.Generic;
using TagTool.Commands.Common;
using System.IO;
using System;
using TagTool.Extensions;

namespace TagTool.Commands.Modding
{
    class AddModFilesCommand : Command
    {
        public GameCacheModPackage Cache { get; }

        public AddModFilesCommand(GameCacheModPackage cache) :
            base(true,
                "AddModFiles",
                "Adds the files in the directory to the mod package",
                "AddExtraFiles <folder path>",
                "Adds the files in the directory to the mod package")
        {
            Cache = cache;
        }

        public override object Execute(List<string> args)
        {
            if (args.Count != 1)
                return new TagToolError(CommandError.ArgCount);

            var path = args[0];

            var directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                Console.WriteLine($"ERROR: Directory does not exist.");
                return new TagToolError(CommandError.DirectoryNotFound);
            }

            AddFiles(directory);

            return true;
        }

        void AddFiles(DirectoryInfo directory)
        {
            foreach (var file in directory.GetFiles("*.*", SearchOption.AllDirectories))
            {
                string virtualPath = directory.GetRelativePath(file.FullName);
                Cache.BaseModPackage.Files.Add(virtualPath, file.OpenRead());
            }
        }
    }
}