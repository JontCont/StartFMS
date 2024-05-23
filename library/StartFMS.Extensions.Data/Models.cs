namespace StartFMS.Extensions.Data;
public static class Models
{
    public static T? InitValue<T>(this T obj)
    {
        if (obj == null) return default;
        foreach (var property in obj.GetType().GetProperties())
        {
            var propertyInfo = obj.GetType().GetProperty(property.Name);
            if (propertyInfo == null || propertyInfo.GetValue(obj) != null) continue;

            if (property.PropertyType == typeof(string))
            {
                propertyInfo.SetValue(obj, string.Empty);
            }
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?) || property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Int32) || property.PropertyType == typeof(Int64) )
            {
                propertyInfo.SetValue(obj, 0);
            }
            else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(Double))
            {
                propertyInfo.SetValue(obj, 0.0);
            }
            else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(Decimal))
            {
                propertyInfo.SetValue(obj, 0.0m);
            }
            else if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
            {
                propertyInfo.SetValue(obj, DateTime.Now);
            }
        }
        return obj;
    }//ToDefaultValue


    public static T SetValue<T>(this T obj, object req)
    {
        if (obj == null || req == null) return obj;
        foreach (var propertyReq in req.GetType().GetProperties())
        {
            var objPropertyInfo = obj.GetType().GetProperty(propertyReq.Name);
            if (objPropertyInfo == null || objPropertyInfo.GetValue(req) == null) continue;
            var reqPropertyInfo = req.GetType().GetProperty(propertyReq.Name);
            var pName = propertyReq.Name;
            var pValue = req.GetType().GetProperty(propertyReq.Name)?.GetValue(req);
            foreach (var property in obj.GetType().GetProperties().Where(x => x.Name == pName))
            {
                if (obj.GetType().GetProperty(property.Name)?.GetValue(obj) != null) continue;
                obj.GetType().GetProperty(property.Name)?.SetValue(obj, pValue);
            }
        }
        return obj;
    }//ToValue

}//class
