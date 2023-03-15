namespace StartFMS.Extensions.Data;
public static class Models {
    public static T InitValue<T>(this T obj) {
        foreach (var property in obj.GetType().GetProperties()) {
            if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null) continue;
            switch (property.PropertyType.Name.ToLower()) {
                case "string":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "".ToString());
                    break;
                case "int":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "0".ToInt());
                    break;
                case "int32":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "0".ToInt32());
                    break;
                case "int64":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "0".ToInt64());
                    break;
                case "double":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "0".ToDecimal());
                    break;
                case "decimal":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, "0".ToDecimal());
                    break;
                case "datetime":
                    obj.GetType().GetProperty(property.Name).SetValue(obj, DateTime.Now);
                    break;
                default:
                    break;
            };
        }
        return obj;
    }//ToDefaultValue

    public static T SetValue<T>(this T obj, object req) {
        foreach (var propertyReq in req.GetType().GetProperties()) {
            if (req.GetType().GetProperty(propertyReq.Name).GetValue(req) == null) continue;
            var pName = propertyReq.Name;
            var pValue = req.GetType().GetProperty(propertyReq.Name).GetValue(req);
            foreach (var property in obj.GetType().GetProperties().Where(x => x.Name == pName)) {
                if (obj.GetType().GetProperty(property.Name).GetValue(obj) != null) continue;
                switch (property.PropertyType.Name.ToLower()) {
                    case "string":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "int":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "int32":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "int64":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "double":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "decimal":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    case "datetime":
                        obj.GetType().GetProperty(property.Name).SetValue(obj, pValue);
                        break;
                    default:
                        break;
                };
            }
        }
        return obj;
    }//ToValue

}//class
