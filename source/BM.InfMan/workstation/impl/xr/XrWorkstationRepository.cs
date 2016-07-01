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

namespace cc.bren.infman.workstation.impl.xr
{
    using cc.bren.infman.framework.xr;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class XrWorkstationRepository : WorkstationRepository
    {
        private XrConnection _workstationConn;
        private WorkstationXrMapping _workstationMapping;

        public XrWorkstationRepository(
            DirectoryInfo storageRoot)
        {
            _workstationConn = new XrConnection(
                storageRoot,
                "workstation");
            _workstationMapping = new WorkstationXrMapping(this);
        }

        //
        // Workstation
        //

        public WorkstationEntity WorkstationSingle(WorkstationFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            return XR.Single(_workstationConn, _workstationMapping, filter);
        }

        public IList<WorkstationEntity> WorkstationList(WorkstationFilter filter)
        {
            if (filter == null) { throw new ArgumentNullException("filter"); }

            return XR.List(_workstationConn, _workstationMapping, filter);
        }

        public WorkstationEntity WorkstationInsert(WorkstationInsert insert)
        {
            if (insert == null) { throw new ArgumentNullException("insert"); }

            return XR.Insert(_workstationConn, _workstationMapping, insert);
        }

        public WorkstationEntity WorkstationUpdate(WorkstationUpdate update)
        {
            if (update == null) { throw new ArgumentNullException("update"); }

            return XR.Update(_workstationConn, _workstationMapping, update);
        }
    }
}
