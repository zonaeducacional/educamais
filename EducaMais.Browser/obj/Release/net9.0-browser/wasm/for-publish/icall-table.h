#define ICALL_TABLE_corlib 1

static int corlib_icall_indexes [] = {
221,
231,
232,
233,
234,
235,
236,
237,
238,
239,
242,
243,
345,
346,
347,
375,
376,
377,
404,
405,
406,
504,
505,
506,
509,
554,
555,
557,
559,
561,
563,
568,
576,
577,
578,
579,
580,
581,
582,
583,
584,
585,
586,
638,
639,
683,
692,
693,
761,
767,
770,
772,
777,
778,
780,
781,
785,
786,
788,
789,
792,
793,
794,
797,
799,
802,
804,
806,
815,
878,
880,
882,
892,
893,
894,
896,
902,
903,
904,
905,
906,
914,
915,
916,
920,
921,
923,
925,
1123,
1298,
1299,
7399,
7400,
7402,
7403,
7404,
7405,
7406,
7408,
7409,
7410,
7411,
7412,
7430,
7432,
7437,
7439,
7441,
7443,
7494,
7500,
7501,
7503,
7504,
7505,
7506,
7507,
7509,
7511,
8647,
8651,
8653,
8654,
8655,
8656,
9107,
9108,
9109,
9110,
9131,
9132,
9133,
9186,
9269,
9272,
9281,
9282,
9283,
9284,
9285,
9286,
9593,
9598,
9599,
9624,
9639,
9645,
9652,
9663,
9666,
9685,
9759,
9767,
9769,
9770,
9777,
9791,
9810,
9811,
9819,
9821,
9827,
9828,
9831,
9833,
9838,
9844,
9845,
9852,
9854,
9866,
9869,
9870,
9871,
9881,
9890,
9896,
9897,
9898,
9900,
9901,
9918,
9920,
9934,
9951,
9977,
10006,
10007,
10483,
10563,
10564,
10766,
10767,
10774,
10775,
10776,
10781,
10833,
11311,
11312,
11703,
11707,
11717,
12543,
12564,
12566,
12568,
};
void ves_icall_System_Array_InternalCreate (int,int,int,int,int);
int ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal (int);
int ves_icall_System_Array_IsValueOfElementTypeInternal (int,int);
int ves_icall_System_Array_CanChangePrimitive (int,int,int);
int ves_icall_System_Array_FastCopy (int,int,int,int,int);
int ves_icall_System_Array_GetLengthInternal_raw (int,int,int);
int ves_icall_System_Array_GetLowerBoundInternal_raw (int,int,int);
void ves_icall_System_Array_GetGenericValue_icall (int,int,int);
void ves_icall_System_Array_GetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetGenericValue_icall (int,int,int);
void ves_icall_System_Array_SetValueImpl_raw (int,int,int,int);
void ves_icall_System_Array_SetValueRelaxedImpl_raw (int,int,int,int);
void ves_icall_System_Runtime_RuntimeImports_ZeroMemory (int,int);
void ves_icall_System_Runtime_RuntimeImports_Memmove (int,int,int);
void ves_icall_System_Buffer_BulkMoveWithWriteBarrier (int,int,int,int);
int ves_icall_System_Delegate_AllocDelegateLike_internal_raw (int,int);
int ves_icall_System_Delegate_CreateDelegate_internal_raw (int,int,int,int,int);
int ves_icall_System_Delegate_GetVirtualMethod_internal_raw (int,int);
void ves_icall_System_Enum_GetEnumValuesAndNames_raw (int,int,int,int);
int ves_icall_System_Enum_InternalGetCorElementType (int);
void ves_icall_System_Enum_InternalGetUnderlyingType_raw (int,int,int);
int ves_icall_System_Environment_get_ProcessorCount ();
int ves_icall_System_Environment_get_TickCount ();
int64_t ves_icall_System_Environment_get_TickCount64 ();
void ves_icall_System_Environment_FailFast_raw (int,int,int,int);
void ves_icall_System_GC_register_ephemeron_array_raw (int,int);
int ves_icall_System_GC_get_ephemeron_tombstone_raw (int);
void ves_icall_System_GC_SuppressFinalize_raw (int,int);
void ves_icall_System_GC_ReRegisterForFinalize_raw (int,int);
void ves_icall_System_GC_GetGCMemoryInfo (int,int,int,int,int,int);
int ves_icall_System_GC_AllocPinnedArray_raw (int,int,int);
int ves_icall_System_Object_MemberwiseClone_raw (int,int);
double ves_icall_System_Math_Atan2 (double,double);
double ves_icall_System_Math_Ceiling (double);
double ves_icall_System_Math_Cos (double);
double ves_icall_System_Math_Exp (double);
double ves_icall_System_Math_Floor (double);
double ves_icall_System_Math_Log (double);
double ves_icall_System_Math_Pow (double,double);
double ves_icall_System_Math_Sin (double);
double ves_icall_System_Math_Sqrt (double);
double ves_icall_System_Math_Tan (double);
double ves_icall_System_Math_ModF (double,int);
float ves_icall_System_MathF_Cos (float);
float ves_icall_System_MathF_Sin (float);
int ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw (int,int);
void ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw (int,int,int);
void ves_icall_RuntimeMethodHandle_ReboxToNullable_raw (int,int,int,int);
int ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw (int,int,int);
void ves_icall_RuntimeType_make_array_type_raw (int,int,int,int);
void ves_icall_RuntimeType_make_byref_type_raw (int,int,int);
void ves_icall_RuntimeType_make_pointer_type_raw (int,int,int);
void ves_icall_RuntimeType_MakeGenericType_raw (int,int,int,int);
int ves_icall_RuntimeType_GetMethodsByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetPropertiesByName_native_raw (int,int,int,int,int);
int ves_icall_RuntimeType_GetConstructors_native_raw (int,int,int);
int ves_icall_System_RuntimeType_CreateInstanceInternal_raw (int,int);
void ves_icall_RuntimeType_GetDeclaringMethod_raw (int,int,int);
void ves_icall_System_RuntimeType_getFullName_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetGenericArgumentsInternal_raw (int,int,int,int);
int ves_icall_RuntimeType_GetGenericParameterPosition (int);
int ves_icall_RuntimeType_GetEvents_native_raw (int,int,int,int);
int ves_icall_RuntimeType_GetFields_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetInterfaces_raw (int,int,int);
int ves_icall_RuntimeType_GetNestedTypes_native_raw (int,int,int,int,int);
void ves_icall_RuntimeType_GetDeclaringType_raw (int,int,int);
void ves_icall_RuntimeType_GetName_raw (int,int,int);
void ves_icall_RuntimeType_GetNamespace_raw (int,int,int);
int ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetAttributes (int);
int ves_icall_RuntimeTypeHandle_GetMetadataToken_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_GetCorElementType (int);
int ves_icall_RuntimeTypeHandle_HasInstantiation (int);
int ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_HasReferences_raw (int,int);
int ves_icall_RuntimeTypeHandle_GetArrayRank_raw (int,int);
void ves_icall_RuntimeTypeHandle_GetAssembly_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetElementType_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetModule_raw (int,int,int);
void ves_icall_RuntimeTypeHandle_GetBaseType_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition (int);
int ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw (int,int);
int ves_icall_RuntimeTypeHandle_is_subclass_of_raw (int,int,int);
int ves_icall_RuntimeTypeHandle_IsByRefLike_raw (int,int);
void ves_icall_System_RuntimeTypeHandle_internal_from_name_raw (int,int,int,int,int,int);
int ves_icall_System_String_FastAllocateString_raw (int,int);
int ves_icall_System_Type_internal_from_handle_raw (int,int);
int ves_icall_System_ValueType_InternalGetHashCode_raw (int,int,int);
int ves_icall_System_ValueType_Equals_raw (int,int,int,int);
int ves_icall_System_Threading_Interlocked_CompareExchange_Int (int,int,int);
void ves_icall_System_Threading_Interlocked_CompareExchange_Object (int,int,int,int);
int ves_icall_System_Threading_Interlocked_Decrement_Int (int);
int ves_icall_System_Threading_Interlocked_Increment_Int (int);
int64_t ves_icall_System_Threading_Interlocked_Increment_Long (int);
int ves_icall_System_Threading_Interlocked_Exchange_Int (int,int);
void ves_icall_System_Threading_Interlocked_Exchange_Object (int,int,int);
int64_t ves_icall_System_Threading_Interlocked_CompareExchange_Long (int,int64_t,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Exchange_Long (int,int64_t);
int64_t ves_icall_System_Threading_Interlocked_Read_Long (int);
int ves_icall_System_Threading_Interlocked_Add_Int (int,int);
int64_t ves_icall_System_Threading_Interlocked_Add_Long (int,int64_t);
void ves_icall_System_Threading_Monitor_Monitor_Enter_raw (int,int);
void mono_monitor_exit_icall_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_raw (int,int);
void ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw (int,int);
int ves_icall_System_Threading_Monitor_Monitor_wait_raw (int,int,int,int);
void ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw (int,int,int,int,int);
void ves_icall_System_Threading_Thread_StartInternal_raw (int,int,int);
void ves_icall_System_Threading_Thread_InitInternal_raw (int,int);
int ves_icall_System_Threading_Thread_GetCurrentThread ();
void ves_icall_System_Threading_InternalThread_Thread_free_internal_raw (int,int);
int ves_icall_System_Threading_Thread_GetState_raw (int,int);
void ves_icall_System_Threading_Thread_SetState_raw (int,int,int);
void ves_icall_System_Threading_Thread_ClrState_raw (int,int,int);
void ves_icall_System_Threading_Thread_SetName_icall_raw (int,int,int,int);
int ves_icall_System_Threading_Thread_YieldInternal ();
void ves_icall_System_Threading_Thread_SetPriority_raw (int,int,int);
void ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw (int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw (int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw (int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw (int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw (int);
int ves_icall_System_GCHandle_InternalAlloc_raw (int,int,int);
void ves_icall_System_GCHandle_InternalFree_raw (int,int);
int ves_icall_System_GCHandle_InternalGet_raw (int,int);
void ves_icall_System_GCHandle_InternalSet_raw (int,int,int);
int ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError ();
void ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError (int);
void ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw (int,int,int,int);
int ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw (int,int,int,int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw (int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw (int,int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw (int,int,int,int);
void ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw (int,int);
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack ();
int ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw (int,int,int);
int ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw (int);
int ves_icall_System_Reflection_Assembly_InternalLoad_raw (int,int,int,int);
int ves_icall_System_Reflection_Assembly_InternalGetType_raw (int,int,int,int,int,int);
int ves_icall_System_Reflection_AssemblyName_GetNativeName (int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw (int,int,int,int);
int ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw (int,int);
int ves_icall_MonoCustomAttrs_IsDefinedInternal_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw (int,int);
int ves_icall_System_Reflection_LoaderAllocatorScout_Destroy (int);
void ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw (int,int,int,int);
int ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw (int,int,int,int,int);
void ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw (int,int,int);
void ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw (int,int,int,int,int,int,int);
void ves_icall_RuntimeEventInfo_get_event_info_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_ResolveType_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetParentType_raw (int,int,int);
int ves_icall_RuntimeFieldInfo_GetFieldOffset_raw (int,int);
int ves_icall_RuntimeFieldInfo_GetValueInternal_raw (int,int,int);
void ves_icall_RuntimeFieldInfo_SetValueInternal_raw (int,int,int,int);
int ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw (int,int);
int ves_icall_reflection_get_token_raw (int,int);
void ves_icall_get_method_info_raw (int,int,int);
int ves_icall_get_method_attributes (int);
int ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw (int,int,int);
int ves_icall_System_MonoMethodInfo_get_retval_marshal_raw (int,int);
int ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw (int,int,int,int);
int ves_icall_RuntimeMethodInfo_get_name_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_base_method_raw (int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
void ves_icall_RuntimeMethodInfo_GetPInvoke_raw (int,int,int,int,int);
int ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw (int,int,int);
int ves_icall_RuntimeMethodInfo_GetGenericArguments_raw (int,int);
int ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw (int,int);
int ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw (int,int);
void ves_icall_InvokeClassConstructor_raw (int,int);
int ves_icall_InternalInvoke_raw (int,int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw (int,int,int,int,int,int);
void ves_icall_RuntimePropertyInfo_get_property_info_raw (int,int,int,int);
int ves_icall_reflection_get_token_raw (int,int);
int ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw (int,int,int);
void ves_icall_DynamicMethod_create_dynamic_method_raw (int,int,int,int,int);
void ves_icall_AssemblyBuilder_basic_init_raw (int,int);
void ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw (int,int);
void ves_icall_ModuleBuilder_basic_init_raw (int,int);
void ves_icall_ModuleBuilder_set_wrappers_type_raw (int,int,int);
int ves_icall_ModuleBuilder_getUSIndex_raw (int,int,int);
int ves_icall_ModuleBuilder_getToken_raw (int,int,int,int);
int ves_icall_ModuleBuilder_getMethodToken_raw (int,int,int,int);
void ves_icall_ModuleBuilder_RegisterToken_raw (int,int,int,int);
int ves_icall_TypeBuilder_create_runtime_class_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw (int,int);
int ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw (int,int);
int ves_icall_System_Diagnostics_Debugger_IsAttached_internal ();
int ves_icall_System_Diagnostics_StackFrame_GetFrameInfo (int,int,int,int,int,int,int,int);
void ves_icall_System_Diagnostics_StackTrace_GetTrace (int,int,int,int);
int ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass (int);
void ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree (int);
int ves_icall_Mono_SafeStringMarshal_StringToUtf8 (int);
void ves_icall_Mono_SafeStringMarshal_GFree (int);
static void *corlib_icall_funcs [] = {
// token 221,
ves_icall_System_Array_InternalCreate,
// token 231,
ves_icall_System_Array_GetCorElementTypeOfElementTypeInternal,
// token 232,
ves_icall_System_Array_IsValueOfElementTypeInternal,
// token 233,
ves_icall_System_Array_CanChangePrimitive,
// token 234,
ves_icall_System_Array_FastCopy,
// token 235,
ves_icall_System_Array_GetLengthInternal_raw,
// token 236,
ves_icall_System_Array_GetLowerBoundInternal_raw,
// token 237,
ves_icall_System_Array_GetGenericValue_icall,
// token 238,
ves_icall_System_Array_GetValueImpl_raw,
// token 239,
ves_icall_System_Array_SetGenericValue_icall,
// token 242,
ves_icall_System_Array_SetValueImpl_raw,
// token 243,
ves_icall_System_Array_SetValueRelaxedImpl_raw,
// token 345,
ves_icall_System_Runtime_RuntimeImports_ZeroMemory,
// token 346,
ves_icall_System_Runtime_RuntimeImports_Memmove,
// token 347,
ves_icall_System_Buffer_BulkMoveWithWriteBarrier,
// token 375,
ves_icall_System_Delegate_AllocDelegateLike_internal_raw,
// token 376,
ves_icall_System_Delegate_CreateDelegate_internal_raw,
// token 377,
ves_icall_System_Delegate_GetVirtualMethod_internal_raw,
// token 404,
ves_icall_System_Enum_GetEnumValuesAndNames_raw,
// token 405,
ves_icall_System_Enum_InternalGetCorElementType,
// token 406,
ves_icall_System_Enum_InternalGetUnderlyingType_raw,
// token 504,
ves_icall_System_Environment_get_ProcessorCount,
// token 505,
ves_icall_System_Environment_get_TickCount,
// token 506,
ves_icall_System_Environment_get_TickCount64,
// token 509,
ves_icall_System_Environment_FailFast_raw,
// token 554,
ves_icall_System_GC_register_ephemeron_array_raw,
// token 555,
ves_icall_System_GC_get_ephemeron_tombstone_raw,
// token 557,
ves_icall_System_GC_SuppressFinalize_raw,
// token 559,
ves_icall_System_GC_ReRegisterForFinalize_raw,
// token 561,
ves_icall_System_GC_GetGCMemoryInfo,
// token 563,
ves_icall_System_GC_AllocPinnedArray_raw,
// token 568,
ves_icall_System_Object_MemberwiseClone_raw,
// token 576,
ves_icall_System_Math_Atan2,
// token 577,
ves_icall_System_Math_Ceiling,
// token 578,
ves_icall_System_Math_Cos,
// token 579,
ves_icall_System_Math_Exp,
// token 580,
ves_icall_System_Math_Floor,
// token 581,
ves_icall_System_Math_Log,
// token 582,
ves_icall_System_Math_Pow,
// token 583,
ves_icall_System_Math_Sin,
// token 584,
ves_icall_System_Math_Sqrt,
// token 585,
ves_icall_System_Math_Tan,
// token 586,
ves_icall_System_Math_ModF,
// token 638,
ves_icall_System_MathF_Cos,
// token 639,
ves_icall_System_MathF_Sin,
// token 683,
ves_icall_RuntimeMethodHandle_GetFunctionPointer_raw,
// token 692,
ves_icall_RuntimeMethodHandle_ReboxFromNullable_raw,
// token 693,
ves_icall_RuntimeMethodHandle_ReboxToNullable_raw,
// token 761,
ves_icall_RuntimeType_GetCorrespondingInflatedMethod_raw,
// token 767,
ves_icall_RuntimeType_make_array_type_raw,
// token 770,
ves_icall_RuntimeType_make_byref_type_raw,
// token 772,
ves_icall_RuntimeType_make_pointer_type_raw,
// token 777,
ves_icall_RuntimeType_MakeGenericType_raw,
// token 778,
ves_icall_RuntimeType_GetMethodsByName_native_raw,
// token 780,
ves_icall_RuntimeType_GetPropertiesByName_native_raw,
// token 781,
ves_icall_RuntimeType_GetConstructors_native_raw,
// token 785,
ves_icall_System_RuntimeType_CreateInstanceInternal_raw,
// token 786,
ves_icall_RuntimeType_GetDeclaringMethod_raw,
// token 788,
ves_icall_System_RuntimeType_getFullName_raw,
// token 789,
ves_icall_RuntimeType_GetGenericArgumentsInternal_raw,
// token 792,
ves_icall_RuntimeType_GetGenericParameterPosition,
// token 793,
ves_icall_RuntimeType_GetEvents_native_raw,
// token 794,
ves_icall_RuntimeType_GetFields_native_raw,
// token 797,
ves_icall_RuntimeType_GetInterfaces_raw,
// token 799,
ves_icall_RuntimeType_GetNestedTypes_native_raw,
// token 802,
ves_icall_RuntimeType_GetDeclaringType_raw,
// token 804,
ves_icall_RuntimeType_GetName_raw,
// token 806,
ves_icall_RuntimeType_GetNamespace_raw,
// token 815,
ves_icall_RuntimeType_FunctionPointerReturnAndParameterTypes_raw,
// token 878,
ves_icall_RuntimeTypeHandle_GetAttributes,
// token 880,
ves_icall_RuntimeTypeHandle_GetMetadataToken_raw,
// token 882,
ves_icall_RuntimeTypeHandle_GetGenericTypeDefinition_impl_raw,
// token 892,
ves_icall_RuntimeTypeHandle_GetCorElementType,
// token 893,
ves_icall_RuntimeTypeHandle_HasInstantiation,
// token 894,
ves_icall_RuntimeTypeHandle_IsInstanceOfType_raw,
// token 896,
ves_icall_RuntimeTypeHandle_HasReferences_raw,
// token 902,
ves_icall_RuntimeTypeHandle_GetArrayRank_raw,
// token 903,
ves_icall_RuntimeTypeHandle_GetAssembly_raw,
// token 904,
ves_icall_RuntimeTypeHandle_GetElementType_raw,
// token 905,
ves_icall_RuntimeTypeHandle_GetModule_raw,
// token 906,
ves_icall_RuntimeTypeHandle_GetBaseType_raw,
// token 914,
ves_icall_RuntimeTypeHandle_type_is_assignable_from_raw,
// token 915,
ves_icall_RuntimeTypeHandle_IsGenericTypeDefinition,
// token 916,
ves_icall_RuntimeTypeHandle_GetGenericParameterInfo_raw,
// token 920,
ves_icall_RuntimeTypeHandle_is_subclass_of_raw,
// token 921,
ves_icall_RuntimeTypeHandle_IsByRefLike_raw,
// token 923,
ves_icall_System_RuntimeTypeHandle_internal_from_name_raw,
// token 925,
ves_icall_System_String_FastAllocateString_raw,
// token 1123,
ves_icall_System_Type_internal_from_handle_raw,
// token 1298,
ves_icall_System_ValueType_InternalGetHashCode_raw,
// token 1299,
ves_icall_System_ValueType_Equals_raw,
// token 7399,
ves_icall_System_Threading_Interlocked_CompareExchange_Int,
// token 7400,
ves_icall_System_Threading_Interlocked_CompareExchange_Object,
// token 7402,
ves_icall_System_Threading_Interlocked_Decrement_Int,
// token 7403,
ves_icall_System_Threading_Interlocked_Increment_Int,
// token 7404,
ves_icall_System_Threading_Interlocked_Increment_Long,
// token 7405,
ves_icall_System_Threading_Interlocked_Exchange_Int,
// token 7406,
ves_icall_System_Threading_Interlocked_Exchange_Object,
// token 7408,
ves_icall_System_Threading_Interlocked_CompareExchange_Long,
// token 7409,
ves_icall_System_Threading_Interlocked_Exchange_Long,
// token 7410,
ves_icall_System_Threading_Interlocked_Read_Long,
// token 7411,
ves_icall_System_Threading_Interlocked_Add_Int,
// token 7412,
ves_icall_System_Threading_Interlocked_Add_Long,
// token 7430,
ves_icall_System_Threading_Monitor_Monitor_Enter_raw,
// token 7432,
mono_monitor_exit_icall_raw,
// token 7437,
ves_icall_System_Threading_Monitor_Monitor_pulse_raw,
// token 7439,
ves_icall_System_Threading_Monitor_Monitor_pulse_all_raw,
// token 7441,
ves_icall_System_Threading_Monitor_Monitor_wait_raw,
// token 7443,
ves_icall_System_Threading_Monitor_Monitor_try_enter_with_atomic_var_raw,
// token 7494,
ves_icall_System_Threading_Thread_StartInternal_raw,
// token 7500,
ves_icall_System_Threading_Thread_InitInternal_raw,
// token 7501,
ves_icall_System_Threading_Thread_GetCurrentThread,
// token 7503,
ves_icall_System_Threading_InternalThread_Thread_free_internal_raw,
// token 7504,
ves_icall_System_Threading_Thread_GetState_raw,
// token 7505,
ves_icall_System_Threading_Thread_SetState_raw,
// token 7506,
ves_icall_System_Threading_Thread_ClrState_raw,
// token 7507,
ves_icall_System_Threading_Thread_SetName_icall_raw,
// token 7509,
ves_icall_System_Threading_Thread_YieldInternal,
// token 7511,
ves_icall_System_Threading_Thread_SetPriority_raw,
// token 8647,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_PrepareForAssemblyLoadContextRelease_raw,
// token 8651,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_GetLoadContextForAssembly_raw,
// token 8653,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFile_raw,
// token 8654,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalInitializeNativeALC_raw,
// token 8655,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalLoadFromStream_raw,
// token 8656,
ves_icall_System_Runtime_Loader_AssemblyLoadContext_InternalGetLoadedAssemblies_raw,
// token 9107,
ves_icall_System_GCHandle_InternalAlloc_raw,
// token 9108,
ves_icall_System_GCHandle_InternalFree_raw,
// token 9109,
ves_icall_System_GCHandle_InternalGet_raw,
// token 9110,
ves_icall_System_GCHandle_InternalSet_raw,
// token 9131,
ves_icall_System_Runtime_InteropServices_Marshal_GetLastPInvokeError,
// token 9132,
ves_icall_System_Runtime_InteropServices_Marshal_SetLastPInvokeError,
// token 9133,
ves_icall_System_Runtime_InteropServices_Marshal_StructureToPtr_raw,
// token 9186,
ves_icall_System_Runtime_InteropServices_NativeLibrary_LoadByName_raw,
// token 9269,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalGetHashCode_raw,
// token 9272,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetObjectValue_raw,
// token 9281,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetUninitializedObjectInternal_raw,
// token 9282,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray_raw,
// token 9283,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_GetSpanDataFrom_raw,
// token 9284,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_RunClassConstructor_raw,
// token 9285,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_SufficientExecutionStack,
// token 9286,
ves_icall_System_Runtime_CompilerServices_RuntimeHelpers_InternalBox_raw,
// token 9593,
ves_icall_System_Reflection_Assembly_GetEntryAssembly_raw,
// token 9598,
ves_icall_System_Reflection_Assembly_InternalLoad_raw,
// token 9599,
ves_icall_System_Reflection_Assembly_InternalGetType_raw,
// token 9624,
ves_icall_System_Reflection_AssemblyName_GetNativeName,
// token 9639,
ves_icall_MonoCustomAttrs_GetCustomAttributesInternal_raw,
// token 9645,
ves_icall_MonoCustomAttrs_GetCustomAttributesDataInternal_raw,
// token 9652,
ves_icall_MonoCustomAttrs_IsDefinedInternal_raw,
// token 9663,
ves_icall_System_Reflection_FieldInfo_internal_from_handle_type_raw,
// token 9666,
ves_icall_System_Reflection_FieldInfo_get_marshal_info_raw,
// token 9685,
ves_icall_System_Reflection_LoaderAllocatorScout_Destroy,
// token 9759,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceNames_raw,
// token 9767,
ves_icall_System_Reflection_RuntimeAssembly_GetInfo_raw,
// token 9769,
ves_icall_System_Reflection_RuntimeAssembly_GetManifestResourceInternal_raw,
// token 9770,
ves_icall_System_Reflection_Assembly_GetManifestModuleInternal_raw,
// token 9777,
ves_icall_System_Reflection_RuntimeCustomAttributeData_ResolveArgumentsInternal_raw,
// token 9791,
ves_icall_RuntimeEventInfo_get_event_info_raw,
// token 9810,
ves_icall_reflection_get_token_raw,
// token 9811,
ves_icall_System_Reflection_EventInfo_internal_from_handle_type_raw,
// token 9819,
ves_icall_RuntimeFieldInfo_ResolveType_raw,
// token 9821,
ves_icall_RuntimeFieldInfo_GetParentType_raw,
// token 9827,
ves_icall_RuntimeFieldInfo_GetFieldOffset_raw,
// token 9828,
ves_icall_RuntimeFieldInfo_GetValueInternal_raw,
// token 9831,
ves_icall_RuntimeFieldInfo_SetValueInternal_raw,
// token 9833,
ves_icall_RuntimeFieldInfo_GetRawConstantValue_raw,
// token 9838,
ves_icall_reflection_get_token_raw,
// token 9844,
ves_icall_get_method_info_raw,
// token 9845,
ves_icall_get_method_attributes,
// token 9852,
ves_icall_System_Reflection_MonoMethodInfo_get_parameter_info_raw,
// token 9854,
ves_icall_System_MonoMethodInfo_get_retval_marshal_raw,
// token 9866,
ves_icall_System_Reflection_RuntimeMethodInfo_GetMethodFromHandleInternalType_native_raw,
// token 9869,
ves_icall_RuntimeMethodInfo_get_name_raw,
// token 9870,
ves_icall_RuntimeMethodInfo_get_base_method_raw,
// token 9871,
ves_icall_reflection_get_token_raw,
// token 9881,
ves_icall_InternalInvoke_raw,
// token 9890,
ves_icall_RuntimeMethodInfo_GetPInvoke_raw,
// token 9896,
ves_icall_RuntimeMethodInfo_MakeGenericMethod_impl_raw,
// token 9897,
ves_icall_RuntimeMethodInfo_GetGenericArguments_raw,
// token 9898,
ves_icall_RuntimeMethodInfo_GetGenericMethodDefinition_raw,
// token 9900,
ves_icall_RuntimeMethodInfo_get_IsGenericMethodDefinition_raw,
// token 9901,
ves_icall_RuntimeMethodInfo_get_IsGenericMethod_raw,
// token 9918,
ves_icall_InvokeClassConstructor_raw,
// token 9920,
ves_icall_InternalInvoke_raw,
// token 9934,
ves_icall_reflection_get_token_raw,
// token 9951,
ves_icall_System_Reflection_RuntimeModule_ResolveMethodToken_raw,
// token 9977,
ves_icall_RuntimePropertyInfo_get_property_info_raw,
// token 10006,
ves_icall_reflection_get_token_raw,
// token 10007,
ves_icall_System_Reflection_RuntimePropertyInfo_internal_from_handle_type_raw,
// token 10483,
ves_icall_DynamicMethod_create_dynamic_method_raw,
// token 10563,
ves_icall_AssemblyBuilder_basic_init_raw,
// token 10564,
ves_icall_AssemblyBuilder_UpdateNativeCustomAttributes_raw,
// token 10766,
ves_icall_ModuleBuilder_basic_init_raw,
// token 10767,
ves_icall_ModuleBuilder_set_wrappers_type_raw,
// token 10774,
ves_icall_ModuleBuilder_getUSIndex_raw,
// token 10775,
ves_icall_ModuleBuilder_getToken_raw,
// token 10776,
ves_icall_ModuleBuilder_getMethodToken_raw,
// token 10781,
ves_icall_ModuleBuilder_RegisterToken_raw,
// token 10833,
ves_icall_TypeBuilder_create_runtime_class_raw,
// token 11311,
ves_icall_System_IO_Stream_HasOverriddenBeginEndRead_raw,
// token 11312,
ves_icall_System_IO_Stream_HasOverriddenBeginEndWrite_raw,
// token 11703,
ves_icall_System_Diagnostics_Debugger_IsAttached_internal,
// token 11707,
ves_icall_System_Diagnostics_StackFrame_GetFrameInfo,
// token 11717,
ves_icall_System_Diagnostics_StackTrace_GetTrace,
// token 12543,
ves_icall_Mono_RuntimeClassHandle_GetTypeFromClass,
// token 12564,
ves_icall_Mono_RuntimeGPtrArrayHandle_GPtrArrayFree,
// token 12566,
ves_icall_Mono_SafeStringMarshal_StringToUtf8,
// token 12568,
ves_icall_Mono_SafeStringMarshal_GFree,
};
static uint8_t corlib_icall_flags [] = {
0,
0,
0,
0,
0,
4,
4,
0,
4,
0,
4,
4,
0,
0,
0,
4,
4,
4,
4,
0,
4,
0,
0,
0,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
4,
0,
0,
0,
0,
0,
0,
0,
};
