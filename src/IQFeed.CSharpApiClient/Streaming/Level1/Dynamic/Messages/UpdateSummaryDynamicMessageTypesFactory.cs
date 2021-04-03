using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages
{
    public static class UpdateSummaryDynamicMessageTypesFactory
    {
        private static readonly AssemblyBuilder dynamicTypesAssembly;
        private static readonly ModuleBuilder dynamicTypesModule;

        // List of supported field types
        private static readonly HashSet<Type> SupportedFieldTypes = new HashSet<Type>(
            new Type[]
            {
                typeof(string),
                typeof(int),
                typeof(double),
                typeof(TimeSpan),
                typeof(DateTime),
            }
        );

        #region Referenced Methods

        // NotImplementedException constructor
        private static readonly ConstructorInfo NotImplementedExceptionConstructor;

        // parser methods
        private static readonly MethodInfo ParseIntMethod;
        private static readonly MethodInfo ParseDoubleMethod;
        private static readonly MethodInfo ParseDateMethod;
        private static readonly MethodInfo ParseTimeMethod;

        // GetHashCode methods
        private static readonly MethodInfo IntGetHashCodeMethod;
        private static readonly MethodInfo DoubleGetHashCodeMethod;
        private static readonly MethodInfo DateTimeGetHashCodeMethod;
        private static readonly MethodInfo TimeSpanGetHashCodeMethod;

        // StringBuilder methods
        private static readonly ConstructorInfo StringBuilderConstructor;
        private static readonly MethodInfo AppendStringMethod;
        private static readonly MethodInfo AppendIntMethod;
        private static readonly MethodInfo AppendDoubleMethod;
        private static readonly MethodInfo AppendObjectMethod;

        // String Extension Methods
        private static readonly MethodInfo GetHashCodeOrDefaultMethod;
        private static readonly MethodInfo OrNullStringMethod;        

        #endregion Referenced Methods
        
        private static readonly Dictionary<string, Type> GeneratedTypes = new Dictionary<string, Type>();

        static UpdateSummaryDynamicMessageTypesFactory()
        {
#if NETFRAMEWORK
            dynamicTypesAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("IQFeedDynamicUpdateSummaryMessageTypesAssembly"), AssemblyBuilderAccess.RunAndSave);
            dynamicTypesModule = dynamicTypesAssembly.DefineDynamicModule("IQFeedDynamicUpdateSummaryMessageTypesModule", "IQFeedDynamicUpdateSummaryMessageTypesModule.dll");
#else
            // .net standard doesn't support saving the generated assembly
            dynamicTypesAssembly = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("IQFeedDynamicUpdateSummaryMessageTypesAssembly"), AssemblyBuilderAccess.Run);
            dynamicTypesModule = dynamicTypesAssembly.DefineDynamicModule("IQFeedDynamicUpdateSummaryMessageTypesModule");
#endif
            // find the referenced methods once and reuse later
            // please note that we are deliberately not throwing any exceptions from here, but will do so on the caller thread

            NotImplementedExceptionConstructor = typeof(NotImplementedException).GetConstructor(new Type[] { typeof(string) });
            
            ParseIntMethod = typeof(FieldParser).GetMethod(nameof(FieldParser.ParseInt), new Type[] { typeof(string) });
            ParseDoubleMethod = typeof(FieldParser).GetMethod(nameof(FieldParser.ParseDouble), new Type[] { typeof(string) });
            ParseDateMethod = typeof(FieldParser).GetMethod(nameof(FieldParser.ParseDate), new Type[] { typeof(string), typeof(string) });
            ParseTimeMethod = typeof(FieldParser).GetMethod(nameof(FieldParser.ParseTime), new Type[] { typeof(string), typeof(string) });

            IntGetHashCodeMethod = typeof(int).GetMethod(nameof(int.GetHashCode), Type.EmptyTypes);
            DoubleGetHashCodeMethod = typeof(double).GetMethod(nameof(double.GetHashCode), Type.EmptyTypes);
            DateTimeGetHashCodeMethod = typeof(DateTime).GetMethod(nameof(DateTime.GetHashCode), Type.EmptyTypes);
            TimeSpanGetHashCodeMethod = typeof(TimeSpan).GetMethod(nameof(TimeSpan.GetHashCode), Type.EmptyTypes);

            StringBuilderConstructor = typeof(StringBuilder).GetConstructor(Type.EmptyTypes);
            AppendStringMethod = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new Type[] { typeof(string) });
            AppendIntMethod = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new Type[] { typeof(int) });
            AppendDoubleMethod = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new Type[] { typeof(double) });
            AppendObjectMethod = typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new Type[] { typeof(object) });

            GetHashCodeOrDefaultMethod = typeof(StringExtensions).GetMethod(nameof(StringExtensions.GetHashCodeOrDefault), new Type[] { typeof(string) });
            OrNullStringMethod = typeof(StringExtensions).GetMethod(nameof(StringExtensions.OrNullString), new Type[] { typeof(string) });
        }

        public static Type GenerateDynamicObjectType(DynamicFieldset[] fieldNames)
        {
            // ensure all needed methods were located, otherwise throw an exception to the caller
            EnsureMethodReferences();

            // parameter validation
            if (fieldNames == null)
            {
                throw new ArgumentNullException(nameof(fieldNames));
            }

            // ensure there are 2 or more field names specified
            if (fieldNames.Length < 2)
            {
                throw new ArgumentException("2 or more fields should be specified", nameof(fieldNames));
            }

            // ensure the Symbol field is specified first
            if (fieldNames[0] != DynamicFieldset.Symbol)
            {
                throw new ArgumentException("Symbol should be specified as the first field!", nameof(fieldNames));
            }

            // get the field names that needs to be implemented and have a backing field in the generated object
            var enabledFields = new HashSet<string>(fieldNames.Select(f => f.ToString()));
            if (enabledFields.Count < fieldNames.Length)
            {
                var duplicates = fieldNames.GroupBy(
                    f => f, // key
                    f => f, // value
                    (key, values) => new { Field = key, Count = values.Count() }
                    ).Where(g => g.Count > 1).Select(g => g.Field);
                throw new ArgumentException($"Duplicate fields are specified: {string.Join(",", duplicates)}", nameof(fieldNames));
            }

            // generate the type id based on the specified fields
            var typeUniqueIdentifier = "DynamicUpdateSummaryMessage_" + string.Join("_", fieldNames);

            // perform all class generation under an exclusive lock, it is expected to be a infrequent operation, so there is no risk of slowness
            lock (GeneratedTypes)
            {
                // check if we have already a class with the same set of fields
                if (GeneratedTypes.ContainsKey(typeUniqueIdentifier))
                {
                    // we have it! just return
                    return GeneratedTypes[typeUniqueIdentifier];
                }

                // create a new type builder
                var typeBuilder = dynamicTypesModule.DefineType("UpdateSummaryDynamicMessage_AutoGen_" + Guid.NewGuid().ToString("N"), TypeAttributes.Public | TypeAttributes.Class);
                typeBuilder.AddInterfaceImplementation(typeof(IUpdateSummaryDynamicMessage));

                // generate the fields and properties
                Dictionary<string, FieldBuilder> fields = GenerateFieldsAndProperties(typeBuilder, enabledFields);

                // generate a default constructor
                ConstructorBuilder defaultConstructorBuilder = GenerateDefaultConstructor(typeBuilder);

                // generate the Parse method
                GenerateParseMethod(fieldNames, typeBuilder, fields, defaultConstructorBuilder);

                // generate the Equals method
                GenerateEqualsMethod(fieldNames, typeBuilder, fields);

                // generate the GetHashCode method
                GenerateGetHashCodeMethod(fieldNames, typeBuilder, fields);

                // generate the ToString method
                GenerateToStringMethod(fieldNames, typeBuilder, fields);

                // generate the type
                var generatedType = typeBuilder.CreateTypeInfo();

                // store in the generated types to reuse if needed
                GeneratedTypes.Add(typeUniqueIdentifier, generatedType);

#if NETFRAMEWORK
                // DEBUGGING capability to save the generated assembly and take a look at the auto generated IL code
                // Uncomment when needed, but always comment out for the checked in version!
                // dynamicTypesAssembly.Save("IQFeedClientDynamicTypesAssembly.dll");
#endif

                // return the generated type
                return generatedType;
            }
        }

        #region IL Generation Methods

        private static Dictionary<string, FieldBuilder> GenerateFieldsAndProperties(TypeBuilder typeBuilder, HashSet<string> enabledFields)
        {
            // store the created fields to access later
            var fields = new Dictionary<string, FieldBuilder>();

            // iterate over all of the get all the public instance properties find out which of the fields need an implementation
            var properties = typeof(IUpdateSummaryDynamicMessage).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToArray();
            foreach (var property in properties)
            {
                bool isFieldEnabled = enabledFields.Contains(property.Name);

                FieldBuilder fieldBuilder = null;
                if (isFieldEnabled)
                {
                    // ensure the type is supported
                    if (!SupportedFieldTypes.Contains(property.PropertyType))
                    {
                        throw new NotSupportedException($"{property.PropertyType.Name} is not supported as dynamic field type! Please add support!");
                    }

                    // the field IS enabled
                    // create a private field
                    fieldBuilder = typeBuilder.DefineField(GetFieldName(property.Name), property.PropertyType, FieldAttributes.Private);
                    fields[property.Name] = fieldBuilder;
                }

                // create a public property
                var propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.PropertyType, null);

                // define the "get" accessor method
                var getterMethodBuilder = typeBuilder.DefineMethod(
                    "get_" + property.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.SpecialName | MethodAttributes.HideBySig,
                    property.PropertyType,
                    null);
                
                var getterILGenerator = getterMethodBuilder.GetILGenerator();

                if (isFieldEnabled)
                {
                    // generate the getter code that returns the backing field
                    getterILGenerator.Emit(OpCodes.Ldarg_0);
                    getterILGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
                    getterILGenerator.Emit(OpCodes.Ret);
                }
                else
                {
                    // generate the getter code that throws an exception
                    getterILGenerator.Emit(OpCodes.Ldstr, $"{property.Name} hasn't been enabled via Level1Client.SelectUpdateFieldName method!");
                    getterILGenerator.Emit(OpCodes.Newobj, NotImplementedExceptionConstructor);
                    getterILGenerator.Emit(OpCodes.Throw);
                }

                // map the two getter method created to our the property 
                propertyBuilder.SetGetMethod(getterMethodBuilder);
            }

            return fields;
        }

        private static ConstructorBuilder GenerateDefaultConstructor(TypeBuilder typeBuilder)
        {
            var defaultConstructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.Standard,
                Type.EmptyTypes);

            var constructorILGenerator = defaultConstructorBuilder.GetILGenerator();

            // The argument 0 is the newly created instance
            constructorILGenerator.Emit(OpCodes.Ldarg_0);
            // call the base class constructor
            constructorILGenerator.Emit(OpCodes.Call, typeof(object).GetConstructor(Type.EmptyTypes));
            // return
            constructorILGenerator.Emit(OpCodes.Ret);
            return defaultConstructorBuilder;
        }

        private static void GenerateParseMethod(DynamicFieldset[] fieldNames, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fields, ConstructorBuilder defaultConstructorBuilder)
        {
            var parseMethodBuilder = typeBuilder.DefineMethod(
                "Parse",
                MethodAttributes.Public | MethodAttributes.Static,
                typeBuilder,
                new Type[] { typeof(string) });

            var parseMethodILGenerator = parseMethodBuilder.GetILGenerator();

            // create a local variable for the split values - index 0
            parseMethodILGenerator.DeclareLocal(typeof(string[]));
            // create a new local variable to store the created instance - index 1
            parseMethodILGenerator.DeclareLocal(typeBuilder);

            // split the string argument into parts

            // load the first argument of the Parse method to the execution stack
            parseMethodILGenerator.Emit(OpCodes.Ldarg_0);
            // call the SplitFeedMessage method with one argument
            parseMethodILGenerator.Emit(OpCodes.Call, typeof(StringExtensions).GetMethod(nameof(StringExtensions.SplitFeedMessage), new Type[] { typeof(string) }));
            // store the result in the 'values' local variable (index 0)
            parseMethodILGenerator.Emit(OpCodes.Stloc_0);

            // create a new instance of the object by calling the default constructor
            parseMethodILGenerator.Emit(OpCodes.Newobj, defaultConstructorBuilder);
            // store the created object reference in 'instance' local variable (index 1)
            parseMethodILGenerator.Emit(OpCodes.Stloc_1);

            // add code to parse the split values and set them to the corresponding fields based on their types
            // NOTE: at this point we will assume all field types are supported
            for (int fieldIndex = 0; fieldIndex < fieldNames.Length; ++fieldIndex)
            {
                var field = fields[fieldNames[fieldIndex].ToString()];

                // load the 'instance' to the execution stack (index 1)
                parseMethodILGenerator.Emit(OpCodes.Ldloc_1);

                // load the 'values' to the execution stack (index 0)
                parseMethodILGenerator.Emit(OpCodes.Ldloc_0);
                // load the field index to the execution stack (+1 to ignore the message type)
                EmitLdcI4(parseMethodILGenerator, fieldIndex + 1);
                // get and load the values[fieldIndex] to the execution stack
                parseMethodILGenerator.Emit(OpCodes.Ldelem_Ref);

                // we will handle only the needed types for now
                if (field.FieldType == typeof(string))
                {
                    // perform direct field assignment => instance.field = values[fieldIndex]
                    // the statement after if blocks will take care of it
                }
                else if (field.FieldType == typeof(int))
                {
                    // call the FieldParser.ParseInt(values[fieldIndex])
                    parseMethodILGenerator.Emit(OpCodes.Call, ParseIntMethod);
                }
                else if (field.FieldType == typeof(double))
                {
                    // call the FieldParser.ParseDouble(values[fieldIndex])
                    parseMethodILGenerator.Emit(OpCodes.Call, ParseDoubleMethod);
                }
                else if (field.FieldType == typeof(TimeSpan))
                {
                    // load the UpdateSummaryMessage.UpdateMessageTimeFormat to the execution stack
                    parseMethodILGenerator.Emit(OpCodes.Ldstr, UpdateSummaryMessage.UpdateMessageTimeFormat);
                    // call the FieldParser.ParseTime(values[fieldIndex], UpdateSummaryMessage.UpdateMessageTimeFormat)
                    parseMethodILGenerator.Emit(OpCodes.Call, ParseTimeMethod);
                }
                else if (field.FieldType == typeof(DateTime))
                {
                    // load the FundamentalMessage.FundamentalDateTimeFormat to the execution stack
                    parseMethodILGenerator.Emit(OpCodes.Ldstr, FundamentalMessage.FundamentalDateTimeFormat);
                    // call the FieldParser.ParseDate(values[fieldIndex], FundamentalMessage.FundamentalDateTimeFormat)
                    parseMethodILGenerator.Emit(OpCodes.Call, ParseDateMethod);
                }

                // assign the string value or the ParseXXX result to the current field
                parseMethodILGenerator.Emit(OpCodes.Stfld, field);
            }

            // load the 'instance' to the execution stack to return it to the caller
            parseMethodILGenerator.Emit(OpCodes.Ldloc_1);

            // return
            parseMethodILGenerator.Emit(OpCodes.Ret);
        }

        private static void GenerateEqualsMethod(DynamicFieldset[] fieldNames, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fields)
        {
            var equalsMethodBuilder = typeBuilder.DefineMethod(
                "Equals",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(bool),
                new Type[] { typeof(object) });

            var equalsMethodILGenerator = equalsMethodBuilder.GetILGenerator();

            // Define some labels for branching
            var notEqualsLabel = equalsMethodILGenerator.DefineLabel();
            var endOfMethodLabel = equalsMethodILGenerator.DefineLabel();

            // create a local variables for 'other' value and the comparison result
            equalsMethodILGenerator.DeclareLocal(typeBuilder);  // index 0
            equalsMethodILGenerator.DeclareLocal(typeof(bool)); // index 1

            // cast the passed in object to our type

            // load the second argument of the Parse method to the execution stack (the first one is the 'this')
            equalsMethodILGenerator.Emit(OpCodes.Ldarg_1);
            // try to cast the other instance to our type
            equalsMethodILGenerator.Emit(OpCodes.Isinst, typeBuilder);
            // store the result in the 'other' local variable
            equalsMethodILGenerator.Emit(OpCodes.Stloc_0);

            // check if the cast was successful

            // load the 'other' variable to the execution stack
            equalsMethodILGenerator.Emit(OpCodes.Ldloc_0);

            // if the reference is null jump to the location that will return false
            equalsMethodILGenerator.Emit(OpCodes.Brfalse, notEqualsLabel);

            // compare the fields
            for (int fieldIndex = 0; fieldIndex < fieldNames.Length; ++fieldIndex)
            {
                var field = fields[fieldNames[fieldIndex].ToString()];

                // load 'this' to the execution stack
                equalsMethodILGenerator.Emit(OpCodes.Ldarg_0);
                // load the field from 'this' instance to the execution stack
                equalsMethodILGenerator.Emit(OpCodes.Ldfld, field);

                // load 'other' to the execution stack
                equalsMethodILGenerator.Emit(OpCodes.Ldloc_0);
                // load the field from 'other' instance to the execution stack
                equalsMethodILGenerator.Emit(OpCodes.Ldfld, field);

                // we will handle only the needed types for now
                if (field.FieldType == typeof(string))
                {
                    // call string == operator
                    equalsMethodILGenerator.Emit(OpCodes.Call, typeof(string).GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static));
                    // if the result is 0 (false) jump to the location that will return false
                    equalsMethodILGenerator.Emit(OpCodes.Brfalse, notEqualsLabel);
                }
                else if (field.FieldType == typeof(int))
                {
                    // if the values don't match jump to the location that will return false
                    equalsMethodILGenerator.Emit(OpCodes.Bne_Un, notEqualsLabel);
                }
                else if (field.FieldType == typeof(double))
                {
                    // if the values don't match jump to the location that will return false
                    equalsMethodILGenerator.Emit(OpCodes.Bne_Un, notEqualsLabel);
                }
                else if (field.FieldType == typeof(TimeSpan))
                {
                    // call TimeSpan == operator
                    equalsMethodILGenerator.Emit(OpCodes.Call, typeof(TimeSpan).GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static));
                    // if the result is 0 (false) jump to the location that will return false
                    equalsMethodILGenerator.Emit(OpCodes.Brfalse, notEqualsLabel);
                }
                else if (field.FieldType == typeof(DateTime))
                {
                    // call DateTime == operator
                    equalsMethodILGenerator.Emit(OpCodes.Call, typeof(DateTime).GetMethod("op_Equality", BindingFlags.Public | BindingFlags.Static));
                    // if the result is 0 (false) jump to the location that will return false
                    equalsMethodILGenerator.Emit(OpCodes.Brfalse, notEqualsLabel);
                }
            }

            // at this point all comparisons passed, which means we have equal objects

            // code section that handles the EQUALS case
            // store 1 (aka true) it in the 'result' local variable
            equalsMethodILGenerator.Emit(OpCodes.Ldc_I4_1);
            equalsMethodILGenerator.Emit(OpCodes.Stloc_1);
            // jump to the end of the method
            equalsMethodILGenerator.Emit(OpCodes.Br, endOfMethodLabel);

            // code section that handles the NOT equals case
            equalsMethodILGenerator.MarkLabel(notEqualsLabel);
            // store 0 (aka false) it in the 'result' local variable
            equalsMethodILGenerator.Emit(OpCodes.Ldc_I4_0);
            equalsMethodILGenerator.Emit(OpCodes.Stloc_1);

            // mark the end the method
            equalsMethodILGenerator.MarkLabel(endOfMethodLabel);
            // load the 'result' value to the execution stack to return it
            equalsMethodILGenerator.Emit(OpCodes.Ldloc_1);
            // return the result
            equalsMethodILGenerator.Emit(OpCodes.Ret);
        }

        private static void GenerateGetHashCodeMethod(DynamicFieldset[] fieldNames, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fields)
        {
            var getHashCodeMethodBuilder = typeBuilder.DefineMethod(
                "GetHashCode",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(int),
                Type.EmptyTypes);

            var getHashCodeMethodILGenerator = getHashCodeMethodBuilder.GetILGenerator();

            // create a local variables for 'hash'
            getHashCodeMethodILGenerator.DeclareLocal(typeof(int));  // index 0
            
            // write 17 to 'hash'
            EmitLdcI4(getHashCodeMethodILGenerator, 17);
            getHashCodeMethodILGenerator.Emit(OpCodes.Stloc_0);
            
            // get the hash codes from the fields and 
            for (int fieldIndex = 0; fieldIndex < fieldNames.Length; ++fieldIndex)
            {
                var field = fields[fieldNames[fieldIndex].ToString()];

                // load 'hash' to the execution stack
                getHashCodeMethodILGenerator.Emit(OpCodes.Ldloc_0);
                // load to the execution stack
                EmitLdcI4(getHashCodeMethodILGenerator, 29);
                // multiply 'hash' by 29
                getHashCodeMethodILGenerator.Emit(OpCodes.Mul);

                // load 'this' to the execution stack
                getHashCodeMethodILGenerator.Emit(OpCodes.Ldarg_0);

                // we will handle only the needed types for now
                if (field.FieldType == typeof(string))
                {
                    // load the field from 'this' instance to the execution stack
                    getHashCodeMethodILGenerator.Emit(OpCodes.Ldfld, field);
                    // get the hash code of the field defaulting to 0 if the string is null
                    getHashCodeMethodILGenerator.Emit(OpCodes.Call, GetHashCodeOrDefaultMethod);
                }
                else if (field.FieldType == typeof(int))
                {
                    // load the field address to the execution stack
                    getHashCodeMethodILGenerator.Emit(OpCodes.Ldflda, field);
                    // get the hash code of the field
                    getHashCodeMethodILGenerator.Emit(OpCodes.Call, IntGetHashCodeMethod);
                }
                else if (field.FieldType == typeof(double))
                {
                    // load the field address to the execution stack
                    getHashCodeMethodILGenerator.Emit(OpCodes.Ldflda, field);
                    // get the hash code of the field
                    getHashCodeMethodILGenerator.Emit(OpCodes.Call, DoubleGetHashCodeMethod);
                }
                else if (field.FieldType == typeof(TimeSpan))
                {
                    // load the field address to the execution stack
                    getHashCodeMethodILGenerator.Emit(OpCodes.Ldflda, field);
                    // get the hash code of the field
                    getHashCodeMethodILGenerator.Emit(OpCodes.Call, TimeSpanGetHashCodeMethod);
                }
                else if (field.FieldType == typeof(DateTime))
                {
                    // load the field address to the execution stack
                    getHashCodeMethodILGenerator.Emit(OpCodes.Ldflda, field);
                    // get the hash code of the field
                    getHashCodeMethodILGenerator.Emit(OpCodes.Call, DateTimeGetHashCodeMethod);
                }
                
                // add the returned hash code to the result of hash * 29 that is still on the execution stack
                getHashCodeMethodILGenerator.Emit(OpCodes.Add);

                // store the result into 'hash' local variable
                getHashCodeMethodILGenerator.Emit(OpCodes.Stloc_0);
            }

            // load 'hash' to the execution stack in order to return it
            getHashCodeMethodILGenerator.Emit(OpCodes.Ldloc_0);

            // return the result
            getHashCodeMethodILGenerator.Emit(OpCodes.Ret);
        }

        private static void GenerateToStringMethod(DynamicFieldset[] fieldNames, TypeBuilder typeBuilder, Dictionary<string, FieldBuilder> fields)
        {
            var toStringMethodBuilder = typeBuilder.DefineMethod(
                "ToString",
                MethodAttributes.Public | MethodAttributes.Virtual,
                typeof(string),
                Type.EmptyTypes);

            var toStringMethodILGenerator = toStringMethodBuilder.GetILGenerator();

            // create a local variables for 'stringBuilder'
            toStringMethodILGenerator.DeclareLocal(typeof(StringBuilder));  // index 0

            // create a new string builder instance by calling the default constructor
            toStringMethodILGenerator.Emit(OpCodes.Newobj, StringBuilderConstructor);
            // store in the local variable
            toStringMethodILGenerator.Emit(OpCodes.Stloc_0);

            // append fields to the StringBuilder instance
            for (int fieldIndex = 0; fieldIndex < fieldNames.Length; ++fieldIndex)
            {
                var fieldName = fieldNames[fieldIndex].ToString();
                var field = fields[fieldName];

                // load 'stringBuilder' to the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Ldloc_0);
                // load the string with the field name to execution stack
                toStringMethodILGenerator.Emit(OpCodes.Ldstr, (fieldIndex == 0 ? string.Empty : ", ") + $"{fieldName}: ");
                // call the Append string method
                toStringMethodILGenerator.Emit(OpCodes.Callvirt, AppendStringMethod);
                // drop the result from the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Pop);

                // load 'stringBuilder' to the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Ldloc_0);
                // load 'this' to the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Ldarg_0);
                // load the field from 'this' instance to the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Ldfld, field);

                // we will handle only the needed types for now
                if (field.FieldType == typeof(string))
                {
                    // call the OrNullString string method
                    toStringMethodILGenerator.Emit(OpCodes.Call, OrNullStringMethod);
                    // call the Append string method for the returned result
                    toStringMethodILGenerator.Emit(OpCodes.Callvirt, AppendStringMethod);
                }
                else if (field.FieldType == typeof(int))
                {
                    // call the Append int method
                    toStringMethodILGenerator.Emit(OpCodes.Callvirt, AppendIntMethod);
                }
                else if (field.FieldType == typeof(double))
                {
                    // call the Append double method
                    toStringMethodILGenerator.Emit(OpCodes.Callvirt, AppendDoubleMethod);
                }
                else if (field.FieldType == typeof(TimeSpan) || field.FieldType == typeof(DateTime))
                {
                    // box the value
                    toStringMethodILGenerator.Emit(OpCodes.Box, field.FieldType);
                    // call the Append object method
                    toStringMethodILGenerator.Emit(OpCodes.Callvirt, AppendObjectMethod);
                }                

                // drop the result from the execution stack
                toStringMethodILGenerator.Emit(OpCodes.Pop);
            }

            // load 'stringBuilder' to the execution stack
            toStringMethodILGenerator.Emit(OpCodes.Ldloc_0);

            // call stringBuilder's toString to generate the value
            toStringMethodILGenerator.Emit(OpCodes.Callvirt, typeof(object).GetMethod(nameof(object.ToString), Type.EmptyTypes));

            // return the result
            toStringMethodILGenerator.Emit(OpCodes.Ret);
        }

        #endregion IL Generation Methods

        #region Private Helper Methods

        private static string GetFieldName(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return $"_{char.ToLowerInvariant(name[0])}{name.Substring(1)}";
        }

        private static void EnsureMethodReferences()
        {
            const string MethodNotFoundErrorMessage = " method couldn't be found! Please revisit the generator code!";

            if (NotImplementedExceptionConstructor == null) throw new NotSupportedException(nameof(NotImplementedExceptionConstructor) + MethodNotFoundErrorMessage);

            if (ParseIntMethod == null) throw new NotSupportedException(nameof(ParseIntMethod) + MethodNotFoundErrorMessage);
            if (ParseDoubleMethod == null) throw new NotSupportedException(nameof(ParseDoubleMethod) + MethodNotFoundErrorMessage);
            if (ParseDateMethod == null) throw new NotSupportedException(nameof(ParseDateMethod) + MethodNotFoundErrorMessage);
            if (ParseTimeMethod == null) throw new NotSupportedException(nameof(ParseTimeMethod) + MethodNotFoundErrorMessage);

            if (IntGetHashCodeMethod == null) throw new NotSupportedException(nameof(IntGetHashCodeMethod) + MethodNotFoundErrorMessage);
            if (DoubleGetHashCodeMethod == null) throw new NotSupportedException(nameof(DoubleGetHashCodeMethod) + MethodNotFoundErrorMessage);
            if (DateTimeGetHashCodeMethod == null) throw new NotSupportedException(nameof(DateTimeGetHashCodeMethod) + MethodNotFoundErrorMessage);
            if (TimeSpanGetHashCodeMethod == null) throw new NotSupportedException(nameof(TimeSpanGetHashCodeMethod) + MethodNotFoundErrorMessage);

            if (StringBuilderConstructor == null) throw new NotSupportedException(nameof(StringBuilderConstructor) + MethodNotFoundErrorMessage);
            if (AppendStringMethod == null) throw new NotSupportedException(nameof(AppendStringMethod) + MethodNotFoundErrorMessage);
            if (AppendIntMethod == null) throw new NotSupportedException(nameof(AppendIntMethod) + MethodNotFoundErrorMessage);
            if (AppendDoubleMethod == null) throw new NotSupportedException(nameof(AppendDoubleMethod) + MethodNotFoundErrorMessage);
            if (AppendObjectMethod == null) throw new NotSupportedException(nameof(AppendObjectMethod) + MethodNotFoundErrorMessage);

            if (GetHashCodeOrDefaultMethod == null) throw new NotSupportedException(nameof(GetHashCodeOrDefaultMethod) + MethodNotFoundErrorMessage);
            if (OrNullStringMethod == null) throw new NotSupportedException(nameof(OrNullStringMethod) + MethodNotFoundErrorMessage);
        }

        private static void EmitLdcI4(ILGenerator parseMethodILGenerator, int val)
        {   
            switch(val)
            {
                case -1:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_M1);
                    break;
                case 0:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_0);
                    break;
                case 1:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_1);
                    break;
                case 2:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_2);
                    break;
                case 3:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_3);
                    break;
                case 4:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_4);
                    break;
                case 5:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_5);
                    break;
                case 6:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_6);
                    break;
                case 7:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_7);
                    break;
                case 8:
                    parseMethodILGenerator.Emit(OpCodes.Ldc_I4_8);
                    break;
                default:
                    if (sbyte.MinValue <= val && val <= sbyte.MaxValue)
                    {
                        parseMethodILGenerator.Emit(OpCodes.Ldc_I4_S, (sbyte)val);
                    }
                    else
                    {
                        parseMethodILGenerator.Emit(OpCodes.Ldc_I4, val);
                    }
                    break;
            }
        }

        #endregion Private Helper Methods
    }
}