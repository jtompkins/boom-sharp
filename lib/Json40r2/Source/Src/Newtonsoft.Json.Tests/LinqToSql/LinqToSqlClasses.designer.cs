﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Newtonsoft.Json.Tests.LinqToSql
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	public partial class LinqToSqlClassesDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertPerson(Person instance);
    partial void UpdatePerson(Person instance);
    partial void DeletePerson(Person instance);
    partial void InsertRole(Role instance);
    partial void UpdateRole(Role instance);
    partial void DeleteRole(Role instance);
    partial void InsertPersonRole(PersonRole instance);
    partial void UpdatePersonRole(PersonRole instance);
    partial void DeletePersonRole(PersonRole instance);
    partial void InsertDepartment(Department instance);
    partial void UpdateDepartment(Department instance);
    partial void DeleteDepartment(Department instance);
    #endregion
		
		public LinqToSqlClassesDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToSqlClassesDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToSqlClassesDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public LinqToSqlClassesDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Person> Persons
		{
			get
			{
				return this.GetTable<Person>();
			}
		}
		
		public System.Data.Linq.Table<Role> Roles
		{
			get
			{
				return this.GetTable<Role>();
			}
		}
		
		public System.Data.Linq.Table<PersonRole> PersonRoles
		{
			get
			{
				return this.GetTable<PersonRole>();
			}
		}
		
		public System.Data.Linq.Table<Department> Departments
		{
			get
			{
				return this.GetTable<Department>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class Person : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _FirstName;
		
		private string _LastName;
		
		private System.Guid _PersonId;
		
		private System.Guid _DepartmentId;
		
		private EntitySet<PersonRole> _PersonRoles;
		
		private EntityRef<Department> _Department;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnPersonIdChanging(System.Guid value);
    partial void OnPersonIdChanged();
    partial void OnDepartmentIdChanging(System.Guid value);
    partial void OnDepartmentIdChanged();
    #endregion
		
		public Person()
		{
			this._PersonRoles = new EntitySet<PersonRole>(new Action<PersonRole>(this.attach_PersonRoles), new Action<PersonRole>(this.detach_PersonRoles));
			this._Department = default(EntityRef<Department>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", CanBeNull=false)]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", CanBeNull=false)]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonId", IsPrimaryKey=true)]
		public System.Guid PersonId
		{
			get
			{
				return this._PersonId;
			}
			set
			{
				if ((this._PersonId != value))
				{
					this.OnPersonIdChanging(value);
					this.SendPropertyChanging();
					this._PersonId = value;
					this.SendPropertyChanged("PersonId");
					this.OnPersonIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentId")]
		public System.Guid DepartmentId
		{
			get
			{
				return this._DepartmentId;
			}
			set
			{
				if ((this._DepartmentId != value))
				{
					if (this._Department.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnDepartmentIdChanging(value);
					this.SendPropertyChanging();
					this._DepartmentId = value;
					this.SendPropertyChanged("DepartmentId");
					this.OnDepartmentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Person_PersonRole", Storage="_PersonRoles", ThisKey="PersonId", OtherKey="PersonId")]
		public EntitySet<PersonRole> PersonRoles
		{
			get
			{
				return this._PersonRoles;
			}
			set
			{
				this._PersonRoles.Assign(value);
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Department_Person", Storage="_Department", ThisKey="DepartmentId", OtherKey="DepartmentId", IsForeignKey=true)]
		public Department Department
		{
			get
			{
				return this._Department.Entity;
			}
			set
			{
				Department previousValue = this._Department.Entity;
				if (((previousValue != value) 
							|| (this._Department.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Department.Entity = null;
						previousValue.Persons.Remove(this);
					}
					this._Department.Entity = value;
					if ((value != null))
					{
						value.Persons.Add(this);
						this._DepartmentId = value.DepartmentId;
					}
					else
					{
						this._DepartmentId = default(System.Guid);
					}
					this.SendPropertyChanged("Department");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PersonRoles(PersonRole entity)
		{
			this.SendPropertyChanging();
			entity.Person = this;
		}
		
		private void detach_PersonRoles(PersonRole entity)
		{
			this.SendPropertyChanging();
			entity.Person = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class Role : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _Name;
		
		private System.Guid _RoleId;
		
		private EntitySet<PersonRole> _PersonRoles;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnRoleIdChanging(System.Guid value);
    partial void OnRoleIdChanged();
    #endregion
		
		public Role()
		{
			this._PersonRoles = new EntitySet<PersonRole>(new Action<PersonRole>(this.attach_PersonRoles), new Action<PersonRole>(this.detach_PersonRoles));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId", IsPrimaryKey=true)]
		public System.Guid RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_PersonRole", Storage="_PersonRoles", ThisKey="RoleId", OtherKey="RoleId")]
		public EntitySet<PersonRole> PersonRoles
		{
			get
			{
				return this._PersonRoles;
			}
			set
			{
				this._PersonRoles.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_PersonRoles(PersonRole entity)
		{
			this.SendPropertyChanging();
			entity.Role = this;
		}
		
		private void detach_PersonRoles(PersonRole entity)
		{
			this.SendPropertyChanging();
			entity.Role = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class PersonRole : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _PersonId;
		
		private System.Guid _RoleId;
		
		private System.Guid _PersonRoleId;
		
		private EntityRef<Person> _Person;
		
		private EntityRef<Role> _Role;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPersonIdChanging(System.Guid value);
    partial void OnPersonIdChanged();
    partial void OnRoleIdChanging(System.Guid value);
    partial void OnRoleIdChanged();
    partial void OnPersonRoleIdChanging(System.Guid value);
    partial void OnPersonRoleIdChanged();
    #endregion
		
		public PersonRole()
		{
			this._Person = default(EntityRef<Person>);
			this._Role = default(EntityRef<Role>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonId")]
		public System.Guid PersonId
		{
			get
			{
				return this._PersonId;
			}
			set
			{
				if ((this._PersonId != value))
				{
					if (this._Person.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnPersonIdChanging(value);
					this.SendPropertyChanging();
					this._PersonId = value;
					this.SendPropertyChanged("PersonId");
					this.OnPersonIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RoleId")]
		public System.Guid RoleId
		{
			get
			{
				return this._RoleId;
			}
			set
			{
				if ((this._RoleId != value))
				{
					if (this._Role.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRoleIdChanging(value);
					this.SendPropertyChanging();
					this._RoleId = value;
					this.SendPropertyChanged("RoleId");
					this.OnRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PersonRoleId", IsPrimaryKey=true)]
		public System.Guid PersonRoleId
		{
			get
			{
				return this._PersonRoleId;
			}
			set
			{
				if ((this._PersonRoleId != value))
				{
					this.OnPersonRoleIdChanging(value);
					this.SendPropertyChanging();
					this._PersonRoleId = value;
					this.SendPropertyChanged("PersonRoleId");
					this.OnPersonRoleIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Person_PersonRole", Storage="_Person", ThisKey="PersonId", OtherKey="PersonId", IsForeignKey=true)]
		public Person Person
		{
			get
			{
				return this._Person.Entity;
			}
			set
			{
				Person previousValue = this._Person.Entity;
				if (((previousValue != value) 
							|| (this._Person.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Person.Entity = null;
						previousValue.PersonRoles.Remove(this);
					}
					this._Person.Entity = value;
					if ((value != null))
					{
						value.PersonRoles.Add(this);
						this._PersonId = value.PersonId;
					}
					else
					{
						this._PersonId = default(System.Guid);
					}
					this.SendPropertyChanged("Person");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Role_PersonRole", Storage="_Role", ThisKey="RoleId", OtherKey="RoleId", IsForeignKey=true)]
		public Role Role
		{
			get
			{
				return this._Role.Entity;
			}
			set
			{
				Role previousValue = this._Role.Entity;
				if (((previousValue != value) 
							|| (this._Role.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Role.Entity = null;
						previousValue.PersonRoles.Remove(this);
					}
					this._Role.Entity = value;
					if ((value != null))
					{
						value.PersonRoles.Add(this);
						this._RoleId = value.RoleId;
					}
					else
					{
						this._RoleId = default(System.Guid);
					}
					this.SendPropertyChanged("Role");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="")]
	public partial class Department : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private System.Guid _DepartmentId;
		
		private string _Name;
		
		private EntitySet<Person> _Persons;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnDepartmentIdChanging(System.Guid value);
    partial void OnDepartmentIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    #endregion
		
		public Department()
		{
			this._Persons = new EntitySet<Person>(new Action<Person>(this.attach_Persons), new Action<Person>(this.detach_Persons));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DepartmentId", IsPrimaryKey=true)]
		public System.Guid DepartmentId
		{
			get
			{
				return this._DepartmentId;
			}
			set
			{
				if ((this._DepartmentId != value))
				{
					this.OnDepartmentIdChanging(value);
					this.SendPropertyChanging();
					this._DepartmentId = value;
					this.SendPropertyChanged("DepartmentId");
					this.OnDepartmentIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Department_Person", Storage="_Persons", ThisKey="DepartmentId", OtherKey="DepartmentId")]
		public EntitySet<Person> Persons
		{
			get
			{
				return this._Persons;
			}
			set
			{
				this._Persons.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Persons(Person entity)
		{
			this.SendPropertyChanging();
			entity.Department = this;
		}
		
		private void detach_Persons(Person entity)
		{
			this.SendPropertyChanging();
			entity.Department = null;
		}
	}
}
#pragma warning restore 1591
