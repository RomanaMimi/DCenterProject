using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DCenterProject.Gateway;
using Type = DCenterProject.Model.Type;

namespace DCenterProject.BLL
{
    public class TypeManager
    {
        private TypeGateway typeGateway = new TypeGateway();

        public string Save(Type type)
        {

            if (type.TypeName.Length < 3)
            {
                return "Name must be 3 charcters long";
            }
            if (typeGateway.IsTypeExist(type.TypeName))
            {
                return "Already exist";
            }
            int rowAffected = typeGateway.SaveType(type);
            if (rowAffected > 0)
            {
                return "Success";
            }
            return "Not save.";
        }


        public List<Type> GetAllTypes()
        {
            return typeGateway.GetAllTypes();
        }
    }
}