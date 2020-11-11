# JsonApiDynamicObject
Standardizes dynamic objects so that they can be serialized according to the [json:api](https://jsonapi.org/ "JSON API Homepage") specification. These dynamic objects must contain simple attributes with values for basic types.

## Example
For a dynamic object based on a Dictionary <string, object> or an ExpandoObject
```C#
// ExpandoObject
dynamic exp = new ExpandoObject();
exp.id = 2;
//exp.type = "";
exp.name = "Jhon";
exp.age = 33;
exp.active = true;
exp.price = 75.26;
exp.sex = 'M';
exp.birthDate = DateTime.Now.ToShortDateString();
exp.jobs = new List<string> { "Teacher", "Ingenier", "Developer" };

// Dictionary <string, object>
dynamic dic = new Dictionary<string, object>();
dic["id"] = "1";
//dic["type"] = "";
dic["name"] = "Jhon";
dic["age"] = 33;
dic["active"] = true;
dic["price"] = 2.3;
dic["sex"] = 'M';
dic["birthDate"] = DateTime.Now.ToShortDateString();
dic["jobs"] = new List<string> { "Teacher", "Ingenier", "Developer" };
```
Using [Json.NET](https://jsonapi.org/ "Json.NET Homepage") for serialization
```C#
var obj = JsonApiDynamicObjects.getRootObject(exp);

string json = JsonConvert.SerializeObject(obj);
```
It is obtained as a result
```C#
// ExpandoObject
{
  "data": {
    "id": "2",
    "type": "ExpandoObject",
    "attributes": {
      "name": "Jhon",
      "age": 33,
      "activo": true,
      "price": 75.26,
      "sex": "M",
      "birthdate": "10/11/2020",
      "jobs": [
        "Teacher",
        "Ingenier",
        "Developer"
      ]
    }
  }
}

// Dictionary <string, object>
{
  "data": {
    "id": "1",
    "type": "Dictionary`2",
    "attributes": {
      "name": "Jhon",
      "age": 33,
      "es_activo": true,
      "price": 2.3,
      "sex": "M",
      "birthdate": "10/11/2020",
      "jobs": [
        "Teacher",
        "Ingenier",
        "Developer"
      ]
    }
  }
}
```