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
    using cc.bren.infman.framework.xmlrepository;
    using cc.bren.infman.infrastructure;
    using cc.bren.infman.infrastructure.impl;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class XrInfrastructureRepository : InfrastructureRepository
    {
        private DirectoryInfo _storageRoot;
        private XrConnection _infrastructureConn;
        private XrConnection _hostInstanceConn;

        public XrInfrastructureRepository(DirectoryInfo storageRoot)
        {
            if (storageRoot == null) { throw new ArgumentNullException("storageRoot"); }

            _storageRoot = storageRoot;
            _infrastructureConn = new XrConnection(
                storageRoot,
                "infrastructure");
            _hostInstanceConn = new XrConnection(
                storageRoot,
                "host_instance");
        }

        //
        // Infrastructure
        //

        public IList<InfrastructureEntity> InfrastructureList(InfrastructureFilter filter)
        {
            return XR.List(
                _infrastructureConn,
                new InfrastructureXrMapping(),
                filter);
        }

        public InfrastructureEntity InfrastructureInsert(InfrastructureInsert request)
        {
            return XR.Insert(
                _infrastructureConn,
                new InfrastructureXrMapping(),
                request);
        }

        //
        // HostInstance
        //

        public HostInstanceEntity HostInstanceSingle(HostInstanceFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            IList<HostInstanceEntity> list = this.HostInstanceList(filter);

            return list.Single();
        }

        public IList<HostInstanceEntity> HostInstanceList(HostInstanceFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            return XR.List(
                _hostInstanceConn,
                new HostInstanceXrMapping(),
                filter);
        }

        public HostInstanceEntity HostInstanceInsert(HostInstanceInsert request)
        {
            if (request == null) { throw new ArgumentNullException("request"); }

            return XR.Insert(
                _hostInstanceConn,
                new HostInstanceXrMapping(),
                request);
        }
    }
}
