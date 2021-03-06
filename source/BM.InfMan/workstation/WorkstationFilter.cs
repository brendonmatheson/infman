﻿/*
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

namespace cc.bren.infman.workstation
{
    using cc.bren.infman.framework;
    using System;

    public class WorkstationFilter : Filter<WorkstationEntity>
    {
        public static WorkstationFilter All()
        {
            return new WorkstationFilter(null);
        }

        public static WorkstationFilter ById(Guid? workstationId)
        {
            return new WorkstationFilter(workstationId);
        }

        private WorkstationFilter(
            Guid? workstationId)
        {
            this.WorkstationId = workstationId;
        }

        public Guid? WorkstationId { get; private set; }

        public bool Matches(WorkstationEntity entity)
        {
            bool result = true;

            if (this.WorkstationId.HasValue)
            {
                result &= this.WorkstationId.Value == entity.WorkstationId;
            }

            return result;
        }
    }
}
