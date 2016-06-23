/*
    Infrastructure Manager

	Copyright (c) 2016, Brendon Matheson

	http://bren.cc/infman

	This work is copyright.  You may not reproduce or transmit it any any
	form or by any means without permission in writing from the owner of this
	work, Brendon Matheson.  If you infringe our copyright, you render yourself
    liable for prosecution.
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
