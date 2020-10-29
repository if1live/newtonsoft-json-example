# Newtonsoft.Json example

## execute

```bash
cd JsonTest
dotnet test
```

## value type

int, long, bool, date, ...

### non-nullable

json | c#
------|------
exist | value
null | exception
undefined | default(T)

### nullable

json | c#
------|------
exist | value
null | nullable
undefined | nullable

## reference type

string, list, ...

json | c#
------|------
exist | value
null | null
undefined | null
