
using PCR.Users.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace PCR.Users.Data
{
    public class WorkAddressRepository : BaseRepository
    {
        public WorkAddressRepository(string databseId, string pcrId) : base(databseId, pcrId)
        {
        }
        public WorkAddressRepository(string databseId) : base(databseId)
        {
        }
        public WorkAddressRepository() : base()
        {
        }

        /// <summary>
        /// To get the list of work address details.
        /// </summary>
        /// <returns></returns>
        public IList<WorkAddress> GetWorkAddressDetails()
        {
            try
            {
                var lstWorkAddress = _clientDbContext.WorkAddress.ToList();
                return lstWorkAddress;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To get the work address details by userid.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public WorkAddress GetWorkAddressDetailsByID(int userId)
        {
            try
            {
                var workAdd = _masterDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
                if (workAdd == null && _clientDbContext != null)
                {
                    workAdd = _clientDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
                }

                return workAdd;
            }
            catch
            {
                throw;
            }
        }

        public WorkAddress GetWorkAddressDetailsByMailId(int userId)
        {
            try
            { 
                var workAdd = _masterDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
               

                if (workAdd == null && _clientDbContext != null)
                {
                    workAdd = _clientDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
                }

                return workAdd;
            }
            catch
            {
                throw;
            }
        }

        public WorkAddress GetWorkAddressDetailsByMailIdServices(int userId)
        {
            try
            {
                var workAdd = _masterDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);


                if (workAdd == null && _clientDbContext != null)
                {
                    workAdd = _clientDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
                }

                return workAdd;
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// To add the work address details of the user.
        /// </summary>
        /// <param name="workAddress"></param>
        public void AddWorkAddressDetails(WorkAddress workAddress)
        {
            try
            {
                _clientDbContext.WorkAddress.Add(workAddress);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To upadte the user work address details.
        /// </summary>
        /// <param name="workAddress"></param>
        public void UpdateWorkAddressDetails(WorkAddress workAddress)
        {
            try
            {
                var workAdrs = _masterDbContext.WorkAddress.Where(a => a.UserID == workAddress.UserID && a.WorkAddressID == workAddress.WorkAddressID && a.RoleID == 1).FirstOrDefault();
                if (workAdrs != null)
                {
                    _masterDbContext.Entry(workAddress).State = System.Data.Entity.EntityState.Modified;
                    _masterDbContext.SaveChanges();
                }
                else
                {
                    _clientDbContext.Entry(workAddress).State = System.Data.Entity.EntityState.Modified;
                    _clientDbContext.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To find the work address id based on userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int FindWorkAddresID(int userId)
        {
            try
            {
                var workAdd = _clientDbContext.WorkAddress.FirstOrDefault(p => p.UserID == userId);
                if (workAdd != null)
                    return workAdd.WorkAddressID;
                else
                    return 0;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// To delete the user work address details.
        /// </summary>
        /// <param name="workAddress"></param>
        public void DeleteWorkAddressDetails(WorkAddress workAddress)
        {
            try
            {
                _clientDbContext.WorkAddress.Remove(workAddress);
                _clientDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
        

    }
}
