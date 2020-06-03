using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HQ4A.Models;

namespace HQ4A.Roles
{
    public class RolesConfig : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            using (HQ4AEntities db = new HQ4AEntities())
            {
                //List<Usuarios> usuarios = db.Usuarios.Where(x => x.Nombre == username).ToList();
                //string[] roles = new string[usuarios.Count];
                //int i = 0;
                //foreach (Usuarios item in usuarios)
                //{
                //    roles[i] = item.Roles.Rol;
                //    i++;
                //}
                //return roles;
                // OOOOOO
                List<Usuarios> usuarios = db.Usuarios.Where(x => x.Nombre == username).ToList();
                List<string> roles = new List<string>();
                foreach (var item in usuarios)
                {
                    roles.Add(item.Roles.Rol);
                }
                return roles.ToArray();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}