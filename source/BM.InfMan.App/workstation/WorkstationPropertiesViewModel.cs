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

using System;
namespace cc.bren.infman.workstation
{
    using cc.bren.infman.framework;
    using System;
    using System.ComponentModel;

    public class WorkstationPropertiesViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(sender, e);
            }
        }

        private void OnPropertyChanged(
            string propertyName)
        {
            if (propertyName == null) { throw new ArgumentNullException("propertyName"); }

            this.OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private WorkstationRepository _workstationRepository;

        public static WorkstationPropertiesViewModel ForAdd(
            WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            return new WorkstationPropertiesViewModel(
                PropertiesMode.Add,
                workstationRepository,
                null);
        }

        public static WorkstationPropertiesViewModel ForEdit(
            WorkstationRepository workstationRepository,
            Guid workstationId)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (workstationId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "workstationId"); }

            return new WorkstationPropertiesViewModel(
                PropertiesMode.Edit,
                workstationRepository,
                workstationId);
        }

        private WorkstationPropertiesViewModel(
            PropertiesMode propertiesMode,
            WorkstationRepository workstationRepository,
            Guid? workstationId)
        {
            if (propertiesMode == null) { throw new ArgumentNullException("propertiesMode"); }
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            _workstationRepository = workstationRepository;

            propertiesMode.Apply(
                add: () =>
                {
                    this.Name = string.Empty;
                },
                edit: () =>
                {
                    if (workstationId == null) { throw new ArgumentNullException("workstationId"); }

                    this.WorkstationId = workstationId;

                    WorkstationEntity entity = _workstationRepository.WorkstationSingle(WorkstationFilter.ById(workstationId.Value));
                    this.Name = entity.Name;
                });
        }

        private Guid? WorkstationId { get; set; }

        #region Name

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                this.OnPropertyChanged(nameof(this.Name));
            }
        }

        #endregion
    }
}
