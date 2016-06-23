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

using cc.bren.infman.spec;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace cc.bren.infman.impl
{
    public class XmlFileInfManRepository : InfManRepository
    {
        private static readonly string NS_HOST = "http://bren.cc/infman/hostspec";

        private DirectoryInfo _storageRoot;

        public XmlFileInfManRepository(DirectoryInfo storageRoot)
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

            throw new NotImplementedException();
        }

        public HostSpecEntity HostSpecInsert(HostSpecInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            Guid hostSpecId = Guid.NewGuid();

            XElement hostXe = new XElement(
                "hostspec",
                new XAttribute("host_spec_id", hostSpecId.ToString()),
                new XAttribute("name", request.Name));

            DirectoryInfo hostSpecDir = this.HostSpecDirectory(
                hostSpecId,
                request.Name,
                true);

            FileInfo hostSpecFile = hostSpecDir.File("host_spec.xml");

            hostXe.Save(hostSpecFile.FullName);

            return HostSpecFactory.Entity(
                hostSpecId,
                request.Name);
        }

        //
        // Directory
        //

        private DirectoryInfo HostSpecsDirectory()
        {
            return new DirectoryInfo(Path.Combine(
                _storageRoot.FullName,
                "host_specs"));
        }

        private DirectoryInfo HostSpecDirectory(
            Guid hostSpecId,
            string name,
            bool autoCreate)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            name = this.FilenameSafe(name);

            DirectoryInfo result = new DirectoryInfo(Path.Combine(
                this.HostSpecsDirectory().FullName,
                name + "_" + hostSpecId.ToString().Substring(0, 8)));

            if (autoCreate && !result.Exists)
            {
                result.Create();
                result.Refresh();
            }

            return result;
        }

        private string FilenameSafe(string value)
        {
            if (value == null) { throw new ArgumentNullException("value"); }

            value = value.Replace(" ", "_");

            return value;
        }

    }
}
