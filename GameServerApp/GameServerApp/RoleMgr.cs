using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServerApp
{
    public class RoleMgr
    {
        private RoleMgr()
        {
            m_AllRole = new List<Role>();
        }

        #region 单利
        private static object lock_object = new object();
        private static RoleMgr instance;

        public static RoleMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(lock_object)
                    {
                        if (instance == null)
                        {
                            instance = new RoleMgr();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        private List<Role> m_AllRole;

        public List<Role> AllRole
        {
            get
            {
                return m_AllRole;
            }
        }

        public Role GetRole(int roleId)
        {
            for (int i = 0; i < m_AllRole.Count; i++)
            {
                if (m_AllRole[i].RoleId == roleId) return m_AllRole[i];
            }
            return null;
        }
    }
}
