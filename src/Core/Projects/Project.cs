﻿using NDifference.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace NDifference.Projects
{
	/// <summary>
	/// Project object used to store options for configuration of one product.
	/// Can be persisted to file or created on the fly and discarded.
	/// </summary>
	public sealed class Project
	{
		private Identifier identifier = new Identifier();

		public Project()
		{
			this.Version = "1.0";

			this.Product = new Product();

			this.Settings = new ProjectSettings();
		}

		/// <summary>
		/// The name of the file where the project was read from. May be empty
		/// if project created on the fly.
		/// </summary>
		public string FileName { get; set; }

		/// <summary>
		/// The version of the project on file.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// A unique ID for this project.
		/// </summary>
		public string Id
		{
			get
			{
				return this.identifier;
			}

			set
			{
				this.identifier = value;
			}
		}

		public Product Product { get; set; }

		public ProjectSettings Settings { get; set; }

		public PersistableProject ToPersistableFormat()
		{
			var persistableFormat = new PersistableProject
			{
				Identifier = this.Id,
				Version = this.Version
			};

			persistableFormat.ProductName = this.Product.Name;

			string baseFolder = string.IsNullOrEmpty(this.FileName) ? string.Empty : Path.GetDirectoryName(this.FileName);

			bool writeAbsolutePaths = string.IsNullOrEmpty(baseFolder);

			if (!baseFolder.EndsWith("\\"))
			{
				baseFolder += "\\";
			}

			if (this.Product.Increments.Count > 1)
			{
				persistableFormat.SourceName = this.Product.ComparedIncrements.First.Name;
				persistableFormat.TargetName = this.Product.ComparedIncrements.Second.Name;

                if (this.Product.ComparedIncrements.First.Assemblies.AllAssembliesInSameFolder())
                {
                    persistableFormat.SourceFolder = System.IO.Path.GetDirectoryName(this.Product.ComparedIncrements.First.Assemblies[0].Path);

                    foreach (var assembly in this.Product.ComparedIncrements.First.Assemblies)
                    {
                        string include = assembly.Path.MakeRelativeToFolder(persistableFormat.SourceFolder);
                        persistableFormat.SourceAssemblies.Add(include);
                    }
                }
                else
                {
                    foreach (var assembly in this.Product.ComparedIncrements.First.Assemblies)
                    {
                        string include = assembly.Path;

                        if (!writeAbsolutePaths && include.StartsWith(baseFolder))
                            include = include.MakeRelativeToFolder(baseFolder);

                        persistableFormat.SourceAssemblies.Add(include);
                    }
                }

                if (this.Product.ComparedIncrements.Second.Assemblies.AllAssembliesInSameFolder())
                {
                    persistableFormat.TargetFolder = System.IO.Path.GetDirectoryName(this.Product.ComparedIncrements.Second.Assemblies[0].Path);

                    foreach (var assembly in this.Product.ComparedIncrements.Second.Assemblies)
                    {
                        string include = assembly.Path.MakeRelativeToFolder(persistableFormat.TargetFolder);
                        persistableFormat.TargetAssemblies.Add(include);
                    }
                }
                else
                {
                    foreach (var assembly in this.Product.ComparedIncrements.Second.Assemblies)
                    {
                        string include = assembly.Path;

                        if (!writeAbsolutePaths && include.StartsWith(baseFolder))
                            include = include.MakeRelativeToFolder(baseFolder);

                        persistableFormat.TargetAssemblies.Add(include);
                    }
                }
            }

			persistableFormat.Settings = this.Settings.ToPersistableFormat();

			if (!String.IsNullOrEmpty(this.Settings.OutputFolder) && Path.IsPathRooted(this.Settings.OutputFolder))
			{
				persistableFormat.Settings.OutputFolder = writeAbsolutePaths 
                    ? this.Settings.OutputFolder 
                    : this.Settings.OutputFolder.MakeFoldersRelative(baseFolder);
			}

			return persistableFormat;
		}

		public static Project FromPersistableFormat(PersistableProject persistableFormat, string baseFolder)
		{
			Debug.Assert(persistableFormat != null, "Project file object is null");

			var project = new Project
			{
				Id = persistableFormat.Identifier,
				Version = persistableFormat.Version
			};

			if (!string.IsNullOrEmpty(baseFolder) && !baseFolder.EndsWith("\\"))
			{
				baseFolder += "\\";
			}

			project.Product.Name = persistableFormat.ProductName;

			var firstVersion = new ProductIncrement();

			if (!String.IsNullOrEmpty(persistableFormat.SourceName))
			{
				firstVersion.Name = persistableFormat.SourceName;
			}

            if (persistableFormat.SourceAssemblies.Any())
            {
                foreach (var file in persistableFormat.SourceAssemblies)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        string fullPath = file;

                        if (string.IsNullOrEmpty(persistableFormat.SourceFolder))
                        {
                            if (!string.IsNullOrEmpty(baseFolder))
                            {
                                fullPath = baseFolder.MakeAbsolutePath(file);
                            }
                        }
                        else
                        {
                            fullPath = persistableFormat.SourceFolder.MakeAbsolutePath(file);
                        }

                        firstVersion.Add(new AssemblyDiskInfo(fullPath));
                    }
                }
            }
            else
            {
                InferFileListFrom(persistableFormat.SourceFolder, persistableFormat.SourceFilter, firstVersion);
            }

            project.Product.Add(firstVersion);

            var secondVersion = new ProductIncrement();

            if (!String.IsNullOrEmpty(persistableFormat.TargetName))
            {
                secondVersion.Name = persistableFormat.TargetName;
            }

            if (persistableFormat.TargetAssemblies.Any())
            {
                foreach (var file in persistableFormat.TargetAssemblies)
                {
                    if (!string.IsNullOrEmpty(file))
                    {
                        string fullPath = file;

                        if (string.IsNullOrEmpty(persistableFormat.TargetFolder))
                        {
                            if (!string.IsNullOrEmpty(baseFolder))
                            {
                                fullPath = baseFolder.MakeAbsolutePath(file);
                            }
                        }
                        else
                        {
                            fullPath = persistableFormat.TargetFolder.MakeAbsolutePath(file);
                        }

                        secondVersion.Add(new AssemblyDiskInfo(fullPath));
                    }
                }
            }
            else
            {
                InferFileListFrom(persistableFormat.TargetFolder, persistableFormat.TargetFilter, secondVersion);
            }

            project.Product.Add(secondVersion);

            project.Settings = ProjectSettings.FromPersistableFormat(persistableFormat.Settings);

			if (project.Settings.FromIndex >= 0)
			{
				project.Product.FromIncrement = project.Settings.FromIndex;
			}

			if (project.Settings.ToIndex < project.Product.Increments.Count)
			{
				project.Product.ToIncrement = project.Settings.ToIndex;
			}

            if (String.IsNullOrEmpty(project.Settings.OutputFolder))
            {
                project.Settings.OutputFolder = baseFolder;
            }
            else
            {
                if (!Path.IsPathRooted(project.Settings.OutputFolder))
			    {
                    project.Settings.OutputFolder = baseFolder.MakeAbsolutePath(project.Settings.OutputFolder);
                }
            }

			return project;
		}

        private static void InferFileListFrom(string folder, string filter, ProductIncrement increment)
        {
            if (!string.IsNullOrEmpty(folder) && Directory.Exists(folder))
            {
                foreach (string fullPath in Directory.EnumerateFiles(folder, filter))
                {
                    increment.Add(new AssemblyDiskInfo(fullPath));
                }
            }
        }

        public void CopyMetaFrom(Project other)
		{
			if (other == null)
				return;

			this.FileName = other.FileName;
			this.Settings.CopyMetaFrom(other.Settings);
		}
	}

}
