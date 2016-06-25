/*
    Infrastructure Manager
	http://bren.cc/infman

	Copyright (c) 2016, Brendon Matheson

    This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public
    License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any
    later version.

    This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
    warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
    details.

    You should have received a copy of the GNU General Public License along with this program; if not, write to the
    Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace cc.bren.infman.impl
{
    using cc.bren.infman.spec;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Linq;

    public class XrSpecRepository : SpecRepository
    {
        private DirectoryInfo _storageRoot;

        public XrSpecRepository(DirectoryInfo storageRoot)
        {
            if (storageRoot == null) { throw new ArgumentNullException("storageRoot"); }

            _storageRoot = storageRoot;
        }

        //
        // HostSpec
        //

        public HostSpecEntity HostSpecSingle(HostSpecFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            throw new NotImplementedException();
        }

        public IList<HostSpecEntity> HostSpecList(HostSpecFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            IList<HostSpecEntity> result = new List<HostSpecEntity>();

            DirectoryInfo hostSpecsDir = this.HostSpecsDir();
            DirectoryInfo[] hostSpecDirs = hostSpecsDir.GetDirectories();

            foreach(DirectoryInfo hostSpecDir in hostSpecDirs)
            {
                result.Add(this.HostSpecEntityLoad(hostSpecDir));
            }

            return result;
        }

        private HostSpecEntity HostSpecEntityLoad(DirectoryInfo hostSpecDir)
        {
            if (hostSpecDir == null) { throw new ArgumentNullException("hostSpecDir"); }

            FileInfo hostSpecFile = this.HostSpecFile(hostSpecDir);

            XElement hostXe = XElement.Load(hostSpecFile.FullName);

            Guid hostSpecId = Guid.Empty;
            string name = null;
            long ramBytes = 0;

            foreach (XAttribute attr in hostXe.Attributes())
            {
                if (attr.Name == "host_spec_id") hostSpecId = Guid.Parse(attr.Value);
                if (attr.Name == "name") name = attr.Value;
                if (attr.Name == "ram_bytes") ramBytes = long.Parse(attr.Value);
            }

            return HostSpecFactory.Entity(
                hostSpecId,
                name,
                ramBytes);
        }

        public HostSpecEntity HostSpecInsert(HostSpecInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            Guid hostSpecId = Guid.NewGuid();

            XElement hostXe = new XElement(
                "host_spec",
                new XAttribute("host_spec_id", hostSpecId.ToString()),
                new XAttribute("name", request.Name),
                new XAttribute("ram_bytes", request.RamBytes.ToString()));

            DirectoryInfo hostSpecDir = this.HostSpecDir(
                hostSpecId,
                request.Name,
                true);

            FileInfo hostSpecFile = this.HostSpecFile(hostSpecDir);

            hostXe.Save(hostSpecFile.FullName);

            return HostSpecFactory.Entity(
                hostSpecId,
                request.Name,
                request.RamBytes);
        }

        //
        // Helper
        //

        private DirectoryInfo HostSpecsDir()
        {
            return new DirectoryInfo(Path.Combine(
                _storageRoot.FullName,
                "host_spec"));
        }

        private DirectoryInfo HostSpecDir(
            Guid hostSpecId,
            string name,
            bool autoCreate)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            name = this.FilenameSafe(name);

            DirectoryInfo result = new DirectoryInfo(Path.Combine(
                this.HostSpecsDir().FullName,
                name + "_" + hostSpecId.ToString().Substring(0, 8)));

            if (autoCreate && !result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private FileInfo HostSpecFile(
            DirectoryInfo hostSpecDir)
        {
            if (hostSpecDir == null) { throw new ArgumentNullException("hostSpecDir"); }

            return hostSpecDir.File("host_spec.xml");
        }

        private string FilenameSafe(string value)
        {
            if (value == null) { throw new ArgumentNullException("value"); }

            value = value.Replace(" ", "_");

            return value;
        }
    }
}
