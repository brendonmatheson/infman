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
    using cc.bren.infman.workstation.impl;
    using System;

    public static class Extensions
    {
        public static WorkstationEntity Mutate(
            this WorkstationEntity entity,
            string name,
            string keyPath)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }

            return WorkstationFactory.Entity(
                entity.WorkstationId,
                name,
                keyPath);
        }
    }
}
