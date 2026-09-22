// GENERATED FILE, DO NOT MODIFY");


void * eglGetProcAddress (void *);

void GlobalizationNative_ChangeCase (void *, int32_t, void *, int32_t, int32_t);

void GlobalizationNative_ChangeCaseInvariant (void *, int32_t, void *, int32_t, int32_t);

void GlobalizationNative_ChangeCaseTurkish (void *, int32_t, void *, int32_t, int32_t);

void GlobalizationNative_CloseSortHandle (void *);

int32_t GlobalizationNative_CompareString (void *, void *, int32_t, void *, int32_t, int32_t);

int32_t GlobalizationNative_EndsWith (void *, void *, int32_t, void *, int32_t, int32_t, void *);

int32_t GlobalizationNative_EnumCalendarInfo (void *, void *, uint32_t, int32_t, void *);

int32_t GlobalizationNative_GetCalendarInfo (void *, uint32_t, int32_t, void *, int32_t);

int32_t GlobalizationNative_GetCalendars (void *, void *, int32_t);

int32_t GlobalizationNative_GetDefaultLocaleName (void *, int32_t);

int32_t GlobalizationNative_GetJapaneseEraStartDate (int32_t, void *, void *, void *);

int32_t GlobalizationNative_GetLatestJapaneseEra ();

int32_t GlobalizationNative_GetLocaleInfoGroupingSizes (void *, uint32_t, void *, void *);

int32_t GlobalizationNative_GetLocaleInfoInt (void *, uint32_t, void *);

int32_t GlobalizationNative_GetLocaleInfoString (void *, uint32_t, void *, int32_t, void *);

int32_t GlobalizationNative_GetLocaleName (void *, void *, int32_t);

int32_t GlobalizationNative_GetLocales (void *, int32_t);

int32_t GlobalizationNative_GetLocaleTimeFormat (void *, int32_t, void *, int32_t);

int32_t GlobalizationNative_GetSortHandle (void *, void *);

int32_t GlobalizationNative_GetSortKey (void *, void *, int32_t, void *, int32_t, int32_t);

int32_t GlobalizationNative_IndexOf (void *, void *, int32_t, void *, int32_t, int32_t, void *);

void GlobalizationNative_InitICUFunctions (void *, void *, void *, void *);

void GlobalizationNative_InitOrdinalCasingPage (int32_t, void *);

int32_t GlobalizationNative_IsPredefinedLocale (void *);

int32_t GlobalizationNative_LastIndexOf (void *, void *, int32_t, void *, int32_t, int32_t, void *);

int32_t GlobalizationNative_LoadICU ();

int32_t GlobalizationNative_NormalizeString (int32_t, void *, int32_t, void *, int32_t);

int32_t GlobalizationNative_StartsWith (void *, void *, int32_t, void *, int32_t, int32_t, void *);

int32_t GlobalizationNative_ToAscii (uint32_t, void *, int32_t, void *, int32_t);

int32_t GlobalizationNative_ToUnicode (uint32_t, void *, int32_t, void *, int32_t);

void gr_backendrendertarget_delete (void *);

void * gr_backendrendertarget_new_gl (int32_t, int32_t, int32_t, int32_t, void *);

void * gr_backendrendertarget_new_vulkan (int32_t, int32_t, int32_t, void *);

void gr_direct_context_abandon_context (void *);

void * gr_direct_context_make_gl (void *);

void * gr_direct_context_make_gl_with_options (void *, void *);

void * gr_direct_context_make_vulkan (void *);

void * gr_direct_context_make_vulkan_with_options (void *, void *);

void gr_direct_context_release_resources_and_abandon_context (void *);

void gr_direct_context_set_resource_cache_limit (void *, void *);

void * gr_glinterface_assemble_gl_interface (void *, void *);

void * gr_glinterface_assemble_gles_interface (void *, void *);

void * gr_glinterface_create_native_interface ();

void hb_buffer_add_utf16 (void *, void *, int32_t, uint32_t, int32_t);

void * hb_buffer_create ();

void hb_buffer_destroy (void *);

int32_t hb_buffer_get_content_type (void *);

int32_t hb_buffer_get_direction (void *);

void * hb_buffer_get_glyph_infos (void *, void *);

void * hb_buffer_get_glyph_positions (void *, void *);

uint32_t hb_buffer_get_length (void *);

void hb_buffer_guess_segment_properties (void *);

void hb_buffer_reset (void *);

void hb_buffer_reverse (void *);

void hb_buffer_set_direction (void *, int32_t);

void hb_buffer_set_language (void *, void *);

void hb_feature_to_string (void *, void *, uint32_t);

void * hb_language_from_string (void *, int32_t);

void * hb_language_to_string (void *);

int32_t pthread_self ();

void sk_bitmap_destructor (void *);

void * sk_bitmap_get_pixels (void *, void *);

void * sk_bitmap_new ();

void sk_bitmap_set_immutable (void *);

int32_t sk_bitmap_try_alloc_pixels (void *, void *, void *);

void sk_codec_destroy (void *);

void sk_codec_get_info (void *, void *);

int32_t sk_codec_get_pixels (void *, void *, void *, void *, void *);

void * sk_codec_new_from_data (void *);

void sk_color_get_bit_shift (void *, void *, void *, void *);

void * sk_colorspace_new_srgb ();

void * sk_colorspace_new_srgb_linear ();

void sk_colorspace_unref (void *);

int32_t sk_colortype_get_default_8888 ();

void sk_compatpaint_delete (void *);

void * sk_compatpaint_new ();

void sk_compatpaint_reset (void *);

void * sk_data_new_empty ();

void * sk_data_new_from_stream (void *, void *);

void sk_data_unref (void *);

int32_t sk_fontmgr_count_families (void *);

void * sk_fontmgr_create_default ();

void sk_fontmgr_get_family_name (void *, int32_t, void *);

void * sk_fontmgr_ref_default ();

int32_t sk_image_get_height (void *);

int32_t sk_image_get_width (void *);

void * sk_image_new_from_bitmap (void *);

void sk_managedstream_destroy (void *);

void * sk_managedstream_new (void *);

void sk_managedstream_set_procs (void *);

int32_t sk_paint_get_fill_path (void *, void *, void *, void *, float);

void * sk_paint_get_path_effect (void *);

void sk_paint_set_path_effect (void *, void *);

void sk_paint_set_stroke_cap (void *, int32_t);

void sk_paint_set_stroke_join (void *, int32_t);

void sk_paint_set_stroke_miter (void *, float);

void sk_paint_set_stroke_width (void *, float);

void sk_paint_set_style (void *, int32_t);

void sk_path_add_oval (void *, void *, int32_t);

void sk_path_add_path (void *, void *, int32_t);

void sk_path_add_rect (void *, void *, int32_t);

void sk_path_arc_to (void *, float, float, float, int32_t, int32_t, float, float);

void * sk_path_clone (void *);

void sk_path_close (void *);

int32_t sk_path_contains (void *, float, float);

void sk_path_cubic_to (void *, float, float, float, float, float, float);

void sk_path_delete (void *);

void * sk_path_effect_create_dash (void *, int32_t, float);

void sk_path_line_to (void *, float, float);

void sk_path_move_to (void *, float, float);

void * sk_path_new ();

void sk_path_quad_to (void *, float, float, float, float);

void sk_path_set_filltype (void *, int32_t);

void sk_path_transform (void *, void *);

int32_t sk_pathop_op (void *, void *, int32_t, void *);

int32_t sk_pathop_tight_bounds (void *, void *);

void sk_refcnt_safe_unref (void *);

void sk_region_delete (void *);

void sk_region_get_bounds (void *, void *);

int32_t sk_region_intersects_rect (void *, void *);

void * sk_region_new ();

int32_t sk_region_op_rect (void *, void *, int32_t);

int32_t sk_region_set_empty (void *);

void * sk_stream_get_length (void *);

void * sk_stream_get_position (void *);

int32_t sk_stream_seek (void *, void *);

void sk_string_destructor (void *);

void * sk_string_get_c_str (void *);

void * sk_string_get_size (void *);

void * sk_string_new_empty ();

void * sk_typeface_get_family_name (void *);

void * sk_typeface_ref_default ();

int32_t sk_version_get_increment ();

int32_t sk_version_get_milestone ();

int32_t sqlite3_bind_blob (void *, int32_t, void *, int32_t, void *);

int32_t sqlite3_bind_double (void *, int32_t, double);

int32_t sqlite3_bind_int (void *, int32_t, int32_t);

int32_t sqlite3_bind_int64 (void *, int32_t, int64_t);

int32_t sqlite3_bind_null (void *, int32_t);

int32_t sqlite3_bind_parameter_index (void *, void *);

int32_t sqlite3_bind_text (void *, int32_t, void *, int32_t, void *);

int32_t sqlite3_busy_timeout (void *, int32_t);

int32_t sqlite3_changes (void *);

int32_t sqlite3_close (void *);

int32_t sqlite3_close_v2 (void *);

void * sqlite3_column_blob (void *, int32_t);

int32_t sqlite3_column_bytes (void *, int32_t);

int32_t sqlite3_column_count (void *);

double sqlite3_column_double (void *, int32_t);

int32_t sqlite3_column_int (void *, int32_t);

int64_t sqlite3_column_int64 (void *, int32_t);

void * sqlite3_column_name (void *, int32_t);

void * sqlite3_column_text (void *, int32_t);

int32_t sqlite3_column_type (void *, int32_t);

void * sqlite3_errmsg (void *);

int32_t sqlite3_extended_errcode (void *);

int32_t sqlite3_finalize (void *);

int32_t sqlite3_libversion_number ();

int32_t sqlite3_open_v2 (void *, void *, int32_t, void *);

int32_t sqlite3_prepare_v2 (void *, void *, int32_t, void *, void *);

int32_t sqlite3_step (void *);

int32_t SystemNative_Access (void *, int32_t);

int32_t SystemNative_CanGetHiddenFlag ();

int32_t SystemNative_Close (void *);

int32_t SystemNative_CloseDir (void *);

int32_t SystemNative_ConvertErrorPalToPlatform (int32_t);

int32_t SystemNative_ConvertErrorPlatformToPal (int32_t);

void * SystemNative_Dup (void *);

int32_t SystemNative_FAllocate (void *, int64_t, int64_t);

int32_t SystemNative_FLock (void *, int32_t);

void SystemNative_Free (void *);

int32_t SystemNative_FStat (void *, void *);

int32_t SystemNative_FSync (void *);

int32_t SystemNative_FTruncate (void *, int64_t);

int32_t SystemNative_GetCryptographicallySecureRandomBytes (void *, int32_t);

void * SystemNative_GetCwd (void *, int32_t);

void * SystemNative_GetEnv (void *);

int32_t SystemNative_GetErrNo ();

uint32_t SystemNative_GetFileSystemType (void *);

void SystemNative_GetNonCryptographicallySecureRandomBytes (void *, int32_t);

int32_t SystemNative_GetReadDirRBufferSize ();

int64_t SystemNative_GetSystemTimeAsTicks ();

uint64_t SystemNative_GetTimestamp ();

void * SystemNative_GetTimeZoneData (void *, void *);

int32_t SystemNative_LChflagsCanSetHiddenFlag ();

void SystemNative_LowLevelMonitor_Acquire (void *);

void * SystemNative_LowLevelMonitor_Create ();

void SystemNative_LowLevelMonitor_Destroy (void *);

void SystemNative_LowLevelMonitor_Release (void *);

void SystemNative_LowLevelMonitor_Signal_Release (void *);

int32_t SystemNative_LowLevelMonitor_TimedWait (void *, int32_t);

void SystemNative_LowLevelMonitor_Wait (void *);

int64_t SystemNative_LSeek (void *, int64_t, int32_t);

int32_t SystemNative_LStat (void *, void *);

void * SystemNative_Malloc (void *);

int32_t SystemNative_MkDir (void *, int32_t);

void * SystemNative_Open (void *, int32_t, int32_t);

void * SystemNative_OpenDir (void *);

int32_t SystemNative_PosixFAdvise (void *, int64_t, int64_t, int32_t);

int32_t SystemNative_PRead (void *, void *, int32_t, int64_t);

int64_t SystemNative_PReadV (void *, void *, int32_t, int64_t);

int32_t SystemNative_PWrite (void *, void *, int32_t, int64_t);

int64_t SystemNative_PWriteV (void *, void *, int32_t, int64_t);

int32_t SystemNative_Read (void *, void *, int32_t);

int32_t SystemNative_ReadDirR (void *, void *, int32_t, void *);

int32_t SystemNative_ReadLink (void *, void *, int32_t);

int32_t SystemNative_SchedGetCpu ();

void SystemNative_SetErrNo (int32_t);

int32_t SystemNative_Stat (void *, void *);

void * SystemNative_StrErrorR (int32_t, void *, int32_t);

uint32_t SystemNative_TryGetUInt32OSThreadId ();

int32_t SystemNative_Unlink (void *);

int32_t SystemNative_Write (void *, void *, int32_t);
static PinvokeImport e_sqlite3_imports [] = {
    {"sqlite3_bind_blob", sqlite3_bind_blob}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_double", sqlite3_bind_double}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_int", sqlite3_bind_int}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_int64", sqlite3_bind_int64}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_null", sqlite3_bind_null}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_parameter_index", sqlite3_bind_parameter_index}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_bind_text", sqlite3_bind_text}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_busy_timeout", sqlite3_busy_timeout}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_changes", sqlite3_changes}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_close", sqlite3_close}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_close_v2", sqlite3_close_v2}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_blob", sqlite3_column_blob}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_bytes", sqlite3_column_bytes}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_count", sqlite3_column_count}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_double", sqlite3_column_double}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_int", sqlite3_column_int}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_int64", sqlite3_column_int64}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_name", sqlite3_column_name}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_text", sqlite3_column_text}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_column_type", sqlite3_column_type}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_errmsg", sqlite3_errmsg}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_extended_errcode", sqlite3_extended_errcode}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_finalize", sqlite3_finalize}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_libversion_number", sqlite3_libversion_number}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_open_v2", sqlite3_open_v2}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_prepare_v2", sqlite3_prepare_v2}, // SQLitePCLRaw.provider.e_sqlite3
    {"sqlite3_step", sqlite3_step}, // SQLitePCLRaw.provider.e_sqlite3
    {NULL, NULL}
};
static PinvokeImport libHarfBuzzSharp_imports [] = {
    {"hb_buffer_add_utf16", hb_buffer_add_utf16}, // HarfBuzzSharp
    {"hb_buffer_create", hb_buffer_create}, // HarfBuzzSharp
    {"hb_buffer_destroy", hb_buffer_destroy}, // HarfBuzzSharp
    {"hb_buffer_get_content_type", hb_buffer_get_content_type}, // HarfBuzzSharp
    {"hb_buffer_get_direction", hb_buffer_get_direction}, // HarfBuzzSharp
    {"hb_buffer_get_glyph_infos", hb_buffer_get_glyph_infos}, // HarfBuzzSharp
    {"hb_buffer_get_glyph_positions", hb_buffer_get_glyph_positions}, // HarfBuzzSharp
    {"hb_buffer_get_length", hb_buffer_get_length}, // HarfBuzzSharp
    {"hb_buffer_guess_segment_properties", hb_buffer_guess_segment_properties}, // HarfBuzzSharp
    {"hb_buffer_reset", hb_buffer_reset}, // HarfBuzzSharp
    {"hb_buffer_reverse", hb_buffer_reverse}, // HarfBuzzSharp
    {"hb_buffer_set_direction", hb_buffer_set_direction}, // HarfBuzzSharp
    {"hb_buffer_set_language", hb_buffer_set_language}, // HarfBuzzSharp
    {"hb_feature_to_string", hb_feature_to_string}, // HarfBuzzSharp
    {"hb_language_from_string", hb_language_from_string}, // HarfBuzzSharp
    {"hb_language_to_string", hb_language_to_string}, // HarfBuzzSharp
    {NULL, NULL}
};
static PinvokeImport libSkiaSharp_imports [] = {
    {"eglGetProcAddress", eglGetProcAddress}, // Avalonia.Browser
    {"gr_backendrendertarget_delete", gr_backendrendertarget_delete}, // SkiaSharp
    {"gr_backendrendertarget_new_gl", gr_backendrendertarget_new_gl}, // SkiaSharp
    {"gr_backendrendertarget_new_vulkan", gr_backendrendertarget_new_vulkan}, // SkiaSharp
    {"gr_direct_context_abandon_context", gr_direct_context_abandon_context}, // SkiaSharp
    {"gr_direct_context_make_gl", gr_direct_context_make_gl}, // SkiaSharp
    {"gr_direct_context_make_gl_with_options", gr_direct_context_make_gl_with_options}, // SkiaSharp
    {"gr_direct_context_make_vulkan", gr_direct_context_make_vulkan}, // SkiaSharp
    {"gr_direct_context_make_vulkan_with_options", gr_direct_context_make_vulkan_with_options}, // SkiaSharp
    {"gr_direct_context_release_resources_and_abandon_context", gr_direct_context_release_resources_and_abandon_context}, // SkiaSharp
    {"gr_direct_context_set_resource_cache_limit", gr_direct_context_set_resource_cache_limit}, // SkiaSharp
    {"gr_glinterface_assemble_gl_interface", gr_glinterface_assemble_gl_interface}, // SkiaSharp
    {"gr_glinterface_assemble_gles_interface", gr_glinterface_assemble_gles_interface}, // SkiaSharp
    {"gr_glinterface_create_native_interface", gr_glinterface_create_native_interface}, // SkiaSharp
    {"sk_bitmap_destructor", sk_bitmap_destructor}, // SkiaSharp
    {"sk_bitmap_get_pixels", sk_bitmap_get_pixels}, // SkiaSharp
    {"sk_bitmap_new", sk_bitmap_new}, // SkiaSharp
    {"sk_bitmap_set_immutable", sk_bitmap_set_immutable}, // SkiaSharp
    {"sk_bitmap_try_alloc_pixels", sk_bitmap_try_alloc_pixels}, // SkiaSharp
    {"sk_codec_destroy", sk_codec_destroy}, // SkiaSharp
    {"sk_codec_get_info", sk_codec_get_info}, // SkiaSharp
    {"sk_codec_get_pixels", sk_codec_get_pixels}, // SkiaSharp
    {"sk_codec_new_from_data", sk_codec_new_from_data}, // SkiaSharp
    {"sk_color_get_bit_shift", sk_color_get_bit_shift}, // SkiaSharp
    {"sk_colorspace_new_srgb", sk_colorspace_new_srgb}, // SkiaSharp
    {"sk_colorspace_new_srgb_linear", sk_colorspace_new_srgb_linear}, // SkiaSharp
    {"sk_colorspace_unref", sk_colorspace_unref}, // SkiaSharp
    {"sk_colortype_get_default_8888", sk_colortype_get_default_8888}, // SkiaSharp
    {"sk_compatpaint_delete", sk_compatpaint_delete}, // SkiaSharp
    {"sk_compatpaint_new", sk_compatpaint_new}, // SkiaSharp
    {"sk_compatpaint_reset", sk_compatpaint_reset}, // SkiaSharp
    {"sk_data_new_empty", sk_data_new_empty}, // SkiaSharp
    {"sk_data_new_from_stream", sk_data_new_from_stream}, // SkiaSharp
    {"sk_data_unref", sk_data_unref}, // SkiaSharp
    {"sk_fontmgr_count_families", sk_fontmgr_count_families}, // SkiaSharp
    {"sk_fontmgr_create_default", sk_fontmgr_create_default}, // SkiaSharp
    {"sk_fontmgr_get_family_name", sk_fontmgr_get_family_name}, // SkiaSharp
    {"sk_fontmgr_ref_default", sk_fontmgr_ref_default}, // SkiaSharp
    {"sk_image_get_height", sk_image_get_height}, // SkiaSharp
    {"sk_image_get_width", sk_image_get_width}, // SkiaSharp
    {"sk_image_new_from_bitmap", sk_image_new_from_bitmap}, // SkiaSharp
    {"sk_managedstream_destroy", sk_managedstream_destroy}, // SkiaSharp
    {"sk_managedstream_new", sk_managedstream_new}, // SkiaSharp
    {"sk_managedstream_set_procs", sk_managedstream_set_procs}, // SkiaSharp
    {"sk_paint_get_fill_path", sk_paint_get_fill_path}, // SkiaSharp
    {"sk_paint_get_path_effect", sk_paint_get_path_effect}, // SkiaSharp
    {"sk_paint_set_path_effect", sk_paint_set_path_effect}, // SkiaSharp
    {"sk_paint_set_stroke_cap", sk_paint_set_stroke_cap}, // SkiaSharp
    {"sk_paint_set_stroke_join", sk_paint_set_stroke_join}, // SkiaSharp
    {"sk_paint_set_stroke_miter", sk_paint_set_stroke_miter}, // SkiaSharp
    {"sk_paint_set_stroke_width", sk_paint_set_stroke_width}, // SkiaSharp
    {"sk_paint_set_style", sk_paint_set_style}, // SkiaSharp
    {"sk_path_add_oval", sk_path_add_oval}, // SkiaSharp
    {"sk_path_add_path", sk_path_add_path}, // SkiaSharp
    {"sk_path_add_rect", sk_path_add_rect}, // SkiaSharp
    {"sk_path_arc_to", sk_path_arc_to}, // SkiaSharp
    {"sk_path_clone", sk_path_clone}, // SkiaSharp
    {"sk_path_close", sk_path_close}, // SkiaSharp
    {"sk_path_contains", sk_path_contains}, // SkiaSharp
    {"sk_path_cubic_to", sk_path_cubic_to}, // SkiaSharp
    {"sk_path_delete", sk_path_delete}, // SkiaSharp
    {"sk_path_effect_create_dash", sk_path_effect_create_dash}, // SkiaSharp
    {"sk_path_line_to", sk_path_line_to}, // SkiaSharp
    {"sk_path_move_to", sk_path_move_to}, // SkiaSharp
    {"sk_path_new", sk_path_new}, // SkiaSharp
    {"sk_path_quad_to", sk_path_quad_to}, // SkiaSharp
    {"sk_path_set_filltype", sk_path_set_filltype}, // SkiaSharp
    {"sk_path_transform", sk_path_transform}, // SkiaSharp
    {"sk_pathop_op", sk_pathop_op}, // SkiaSharp
    {"sk_pathop_tight_bounds", sk_pathop_tight_bounds}, // SkiaSharp
    {"sk_refcnt_safe_unref", sk_refcnt_safe_unref}, // SkiaSharp
    {"sk_region_delete", sk_region_delete}, // SkiaSharp
    {"sk_region_get_bounds", sk_region_get_bounds}, // SkiaSharp
    {"sk_region_intersects_rect", sk_region_intersects_rect}, // SkiaSharp
    {"sk_region_new", sk_region_new}, // SkiaSharp
    {"sk_region_op_rect", sk_region_op_rect}, // SkiaSharp
    {"sk_region_set_empty", sk_region_set_empty}, // SkiaSharp
    {"sk_stream_get_length", sk_stream_get_length}, // SkiaSharp
    {"sk_stream_get_position", sk_stream_get_position}, // SkiaSharp
    {"sk_stream_seek", sk_stream_seek}, // SkiaSharp
    {"sk_string_destructor", sk_string_destructor}, // SkiaSharp
    {"sk_string_get_c_str", sk_string_get_c_str}, // SkiaSharp
    {"sk_string_get_size", sk_string_get_size}, // SkiaSharp
    {"sk_string_new_empty", sk_string_new_empty}, // SkiaSharp
    {"sk_typeface_get_family_name", sk_typeface_get_family_name}, // SkiaSharp
    {"sk_typeface_ref_default", sk_typeface_ref_default}, // SkiaSharp
    {"sk_version_get_increment", sk_version_get_increment}, // SkiaSharp
    {"sk_version_get_milestone", sk_version_get_milestone}, // SkiaSharp
    {NULL, NULL}
};
static PinvokeImport libSystem_Native_imports [] = {
    {"SystemNative_Access", SystemNative_Access}, // System.Private.CoreLib
    {"SystemNative_CanGetHiddenFlag", SystemNative_CanGetHiddenFlag}, // System.Private.CoreLib
    {"SystemNative_Close", SystemNative_Close}, // System.Private.CoreLib
    {"SystemNative_CloseDir", SystemNative_CloseDir}, // System.Private.CoreLib
    {"SystemNative_ConvertErrorPalToPlatform", SystemNative_ConvertErrorPalToPlatform}, // System.Console, System.Private.CoreLib
    {"SystemNative_ConvertErrorPlatformToPal", SystemNative_ConvertErrorPlatformToPal}, // System.Console, System.Private.CoreLib
    {"SystemNative_Dup", SystemNative_Dup}, // System.Console
    {"SystemNative_FAllocate", SystemNative_FAllocate}, // System.Private.CoreLib
    {"SystemNative_FLock", SystemNative_FLock}, // System.Private.CoreLib
    {"SystemNative_Free", SystemNative_Free}, // System.Private.CoreLib
    {"SystemNative_FStat", SystemNative_FStat}, // System.Private.CoreLib
    {"SystemNative_FSync", SystemNative_FSync}, // System.Private.CoreLib
    {"SystemNative_FTruncate", SystemNative_FTruncate}, // System.Private.CoreLib
    {"SystemNative_GetCryptographicallySecureRandomBytes", SystemNative_GetCryptographicallySecureRandomBytes}, // System.Private.CoreLib
    {"SystemNative_GetCwd", SystemNative_GetCwd}, // System.Private.CoreLib
    {"SystemNative_GetEnv", SystemNative_GetEnv}, // System.Private.CoreLib
    {"SystemNative_GetErrNo", SystemNative_GetErrNo}, // System.Private.CoreLib
    {"SystemNative_GetFileSystemType", SystemNative_GetFileSystemType}, // System.Private.CoreLib
    {"SystemNative_GetNonCryptographicallySecureRandomBytes", SystemNative_GetNonCryptographicallySecureRandomBytes}, // System.Private.CoreLib
    {"SystemNative_GetReadDirRBufferSize", SystemNative_GetReadDirRBufferSize}, // System.Private.CoreLib
    {"SystemNative_GetSystemTimeAsTicks", SystemNative_GetSystemTimeAsTicks}, // System.Private.CoreLib
    {"SystemNative_GetTimestamp", SystemNative_GetTimestamp}, // System.Private.CoreLib
    {"SystemNative_GetTimeZoneData", SystemNative_GetTimeZoneData}, // System.Private.CoreLib
    {"SystemNative_LChflagsCanSetHiddenFlag", SystemNative_LChflagsCanSetHiddenFlag}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Acquire", SystemNative_LowLevelMonitor_Acquire}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Create", SystemNative_LowLevelMonitor_Create}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Destroy", SystemNative_LowLevelMonitor_Destroy}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Release", SystemNative_LowLevelMonitor_Release}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Signal_Release", SystemNative_LowLevelMonitor_Signal_Release}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_TimedWait", SystemNative_LowLevelMonitor_TimedWait}, // System.Private.CoreLib
    {"SystemNative_LowLevelMonitor_Wait", SystemNative_LowLevelMonitor_Wait}, // System.Private.CoreLib
    {"SystemNative_LSeek", SystemNative_LSeek}, // System.Private.CoreLib
    {"SystemNative_LStat", SystemNative_LStat}, // System.Private.CoreLib
    {"SystemNative_Malloc", SystemNative_Malloc}, // System.Private.CoreLib
    {"SystemNative_MkDir", SystemNative_MkDir}, // System.Private.CoreLib
    {"SystemNative_Open", SystemNative_Open}, // System.Private.CoreLib
    {"SystemNative_OpenDir", SystemNative_OpenDir}, // System.Private.CoreLib
    {"SystemNative_PosixFAdvise", SystemNative_PosixFAdvise}, // System.Private.CoreLib
    {"SystemNative_PRead", SystemNative_PRead}, // System.Private.CoreLib
    {"SystemNative_PReadV", SystemNative_PReadV}, // System.Private.CoreLib
    {"SystemNative_PWrite", SystemNative_PWrite}, // System.Private.CoreLib
    {"SystemNative_PWriteV", SystemNative_PWriteV}, // System.Private.CoreLib
    {"SystemNative_Read", SystemNative_Read}, // System.Private.CoreLib
    {"SystemNative_ReadDirR", SystemNative_ReadDirR}, // System.Private.CoreLib
    {"SystemNative_ReadLink", SystemNative_ReadLink}, // System.Private.CoreLib
    {"SystemNative_SchedGetCpu", SystemNative_SchedGetCpu}, // System.Private.CoreLib
    {"SystemNative_SetErrNo", SystemNative_SetErrNo}, // System.Private.CoreLib
    {"SystemNative_Stat", SystemNative_Stat}, // System.Private.CoreLib
    {"SystemNative_StrErrorR", SystemNative_StrErrorR}, // System.Console, System.Private.CoreLib
    {"SystemNative_TryGetUInt32OSThreadId", SystemNative_TryGetUInt32OSThreadId}, // System.Private.CoreLib
    {"SystemNative_Unlink", SystemNative_Unlink}, // System.Private.CoreLib
    {"SystemNative_Write", SystemNative_Write}, // System.Console, System.Private.CoreLib
    {NULL, NULL}
};
static PinvokeImport libSystem_IO_Compression_Native_imports [] = {
    {NULL, NULL}
};
static PinvokeImport libSystem_Globalization_Native_imports [] = {
    {"GlobalizationNative_ChangeCase", GlobalizationNative_ChangeCase}, // System.Private.CoreLib
    {"GlobalizationNative_ChangeCaseInvariant", GlobalizationNative_ChangeCaseInvariant}, // System.Private.CoreLib
    {"GlobalizationNative_ChangeCaseTurkish", GlobalizationNative_ChangeCaseTurkish}, // System.Private.CoreLib
    {"GlobalizationNative_CloseSortHandle", GlobalizationNative_CloseSortHandle}, // System.Private.CoreLib
    {"GlobalizationNative_CompareString", GlobalizationNative_CompareString}, // System.Private.CoreLib
    {"GlobalizationNative_EndsWith", GlobalizationNative_EndsWith}, // System.Private.CoreLib
    {"GlobalizationNative_EnumCalendarInfo", GlobalizationNative_EnumCalendarInfo}, // System.Private.CoreLib
    {"GlobalizationNative_GetCalendarInfo", GlobalizationNative_GetCalendarInfo}, // System.Private.CoreLib
    {"GlobalizationNative_GetCalendars", GlobalizationNative_GetCalendars}, // System.Private.CoreLib
    {"GlobalizationNative_GetDefaultLocaleName", GlobalizationNative_GetDefaultLocaleName}, // System.Private.CoreLib
    {"GlobalizationNative_GetJapaneseEraStartDate", GlobalizationNative_GetJapaneseEraStartDate}, // System.Private.CoreLib
    {"GlobalizationNative_GetLatestJapaneseEra", GlobalizationNative_GetLatestJapaneseEra}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocaleInfoGroupingSizes", GlobalizationNative_GetLocaleInfoGroupingSizes}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocaleInfoInt", GlobalizationNative_GetLocaleInfoInt}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocaleInfoString", GlobalizationNative_GetLocaleInfoString}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocaleName", GlobalizationNative_GetLocaleName}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocales", GlobalizationNative_GetLocales}, // System.Private.CoreLib
    {"GlobalizationNative_GetLocaleTimeFormat", GlobalizationNative_GetLocaleTimeFormat}, // System.Private.CoreLib
    {"GlobalizationNative_GetSortHandle", GlobalizationNative_GetSortHandle}, // System.Private.CoreLib
    {"GlobalizationNative_GetSortKey", GlobalizationNative_GetSortKey}, // System.Private.CoreLib
    {"GlobalizationNative_IndexOf", GlobalizationNative_IndexOf}, // System.Private.CoreLib
    {"GlobalizationNative_InitICUFunctions", GlobalizationNative_InitICUFunctions}, // System.Private.CoreLib
    {"GlobalizationNative_InitOrdinalCasingPage", GlobalizationNative_InitOrdinalCasingPage}, // System.Private.CoreLib
    {"GlobalizationNative_IsPredefinedLocale", GlobalizationNative_IsPredefinedLocale}, // System.Private.CoreLib
    {"GlobalizationNative_LastIndexOf", GlobalizationNative_LastIndexOf}, // System.Private.CoreLib
    {"GlobalizationNative_LoadICU", GlobalizationNative_LoadICU}, // System.Private.CoreLib
    {"GlobalizationNative_NormalizeString", GlobalizationNative_NormalizeString}, // System.Private.CoreLib
    {"GlobalizationNative_StartsWith", GlobalizationNative_StartsWith}, // System.Private.CoreLib
    {"GlobalizationNative_ToAscii", GlobalizationNative_ToAscii}, // System.Private.CoreLib
    {"GlobalizationNative_ToUnicode", GlobalizationNative_ToUnicode}, // System.Private.CoreLib
    {NULL, NULL}
};
static PinvokeImport _2A__imports [] = {
    {"pthread_self", pthread_self}, // Avalonia.Browser
    {NULL, NULL}
};

static void *pinvoke_tables[] = {
    (void*)e_sqlite3_imports, (void*)libHarfBuzzSharp_imports, (void*)libSkiaSharp_imports, (void*)libSystem_Native_imports, (void*)libSystem_IO_Compression_Native_imports, (void*)libSystem_Globalization_Native_imports, (void*)_2A__imports
};

static char *pinvoke_names[] =  {
    "e_sqlite3", "libHarfBuzzSharp", "libSkiaSharp", "libSystem.Native", "libSystem.IO.Compression.Native", "libSystem.Globalization.Native", "*"
};
#include <mono/utils/details/mono-error-types.h>
                #include <mono/metadata/assembly.h>
                #include <mono/utils/mono-error.h>
                #include <mono/metadata/object.h>
                #include <mono/utils/details/mono-logger-types.h>
                #include "runtime.h"
                InterpFtnDesc wasm_native_to_interp_ftndescs[26] = {};
typedef void (*WasmInterpEntrySig_0) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKBitmapReleaseDelegateProxyImplementation (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_0)wasm_native_to_interp_ftndescs [0].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKBitmapReleaseDelegateProxyImplementation", 2);
  }
  ((WasmInterpEntrySig_0)wasm_native_to_interp_ftndescs [0].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [0].arg);
}

typedef void (*WasmInterpEntrySig_1) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKDataReleaseDelegateProxyImplementation (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_1)wasm_native_to_interp_ftndescs [1].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKDataReleaseDelegateProxyImplementation", 2);
  }
  ((WasmInterpEntrySig_1)wasm_native_to_interp_ftndescs [1].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [1].arg);
}

typedef void (*WasmInterpEntrySig_2) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_2)wasm_native_to_interp_ftndescs [2].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem", 2);
  }
  ((WasmInterpEntrySig_2)wasm_native_to_interp_ftndescs [2].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [2].arg);
}

typedef void (*WasmInterpEntrySig_3) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementation (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_3)wasm_native_to_interp_ftndescs [3].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKImageRasterReleaseDelegateProxyImplementation", 2);
  }
  ((WasmInterpEntrySig_3)wasm_native_to_interp_ftndescs [3].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [3].arg);
}

typedef void (*WasmInterpEntrySig_4) (int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageTextureReleaseDelegateProxyImplementation (void * arg0) { 
  if (!(WasmInterpEntrySig_4)wasm_native_to_interp_ftndescs [4].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKImageTextureReleaseDelegateProxyImplementation", 1);
  }
  ((WasmInterpEntrySig_4)wasm_native_to_interp_ftndescs [4].func) ((int*)&arg0, wasm_native_to_interp_ftndescs [4].arg);
}

typedef void (*WasmInterpEntrySig_5) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKSurfaceReleaseDelegateProxyImplementation (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_5)wasm_native_to_interp_ftndescs [5].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKSurfaceReleaseDelegateProxyImplementation", 2);
  }
  ((WasmInterpEntrySig_5)wasm_native_to_interp_ftndescs [5].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [5].arg);
}

typedef void (*WasmInterpEntrySig_6) (int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_GRGlGetProcDelegateProxyImplementation (void * arg0, void * arg1) { 
  void * res;
  if (!(WasmInterpEntrySig_6)wasm_native_to_interp_ftndescs [6].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "GRGlGetProcDelegateProxyImplementation", 2);
  }
  ((WasmInterpEntrySig_6)wasm_native_to_interp_ftndescs [6].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [6].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_7) (int*, int*, int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_GRVkGetProcDelegateProxyImplementation (void * arg0, void * arg1, void * arg2, void * arg3) { 
  void * res;
  if (!(WasmInterpEntrySig_7)wasm_native_to_interp_ftndescs [7].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "GRVkGetProcDelegateProxyImplementation", 4);
  }
  ((WasmInterpEntrySig_7)wasm_native_to_interp_ftndescs [7].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, wasm_native_to_interp_ftndescs [7].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_8) (int*, int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKGlyphPathDelegateProxyImplementation (void * arg0, void * arg1, void * arg2) { 
  if (!(WasmInterpEntrySig_8)wasm_native_to_interp_ftndescs [8].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "DelegateProxies", "SKGlyphPathDelegateProxyImplementation", 3);
  }
  ((WasmInterpEntrySig_8)wasm_native_to_interp_ftndescs [8].func) ((int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [8].arg);
}

typedef void (*WasmInterpEntrySig_9) (int*, int*, int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_ReadInternal (void * arg0, void * arg1, void * arg2, void * arg3) { 
  void * res;
  if (!(WasmInterpEntrySig_9)wasm_native_to_interp_ftndescs [9].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "ReadInternal", 4);
  }
  ((WasmInterpEntrySig_9)wasm_native_to_interp_ftndescs [9].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, wasm_native_to_interp_ftndescs [9].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_10) (int*, int*, int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_PeekInternal (void * arg0, void * arg1, void * arg2, void * arg3) { 
  void * res;
  if (!(WasmInterpEntrySig_10)wasm_native_to_interp_ftndescs [10].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "PeekInternal", 4);
  }
  ((WasmInterpEntrySig_10)wasm_native_to_interp_ftndescs [10].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, wasm_native_to_interp_ftndescs [10].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_11) (int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_IsAtEndInternal (void * arg0, void * arg1) { 
  int32_t res;
  if (!(WasmInterpEntrySig_11)wasm_native_to_interp_ftndescs [11].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "IsAtEndInternal", 2);
  }
  ((WasmInterpEntrySig_11)wasm_native_to_interp_ftndescs [11].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [11].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_12) (int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_HasPositionInternal (void * arg0, void * arg1) { 
  int32_t res;
  if (!(WasmInterpEntrySig_12)wasm_native_to_interp_ftndescs [12].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "HasPositionInternal", 2);
  }
  ((WasmInterpEntrySig_12)wasm_native_to_interp_ftndescs [12].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [12].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_13) (int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_HasLengthInternal (void * arg0, void * arg1) { 
  int32_t res;
  if (!(WasmInterpEntrySig_13)wasm_native_to_interp_ftndescs [13].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "HasLengthInternal", 2);
  }
  ((WasmInterpEntrySig_13)wasm_native_to_interp_ftndescs [13].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [13].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_14) (int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_RewindInternal (void * arg0, void * arg1) { 
  int32_t res;
  if (!(WasmInterpEntrySig_14)wasm_native_to_interp_ftndescs [14].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "RewindInternal", 2);
  }
  ((WasmInterpEntrySig_14)wasm_native_to_interp_ftndescs [14].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [14].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_15) (int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_GetPositionInternal (void * arg0, void * arg1) { 
  void * res;
  if (!(WasmInterpEntrySig_15)wasm_native_to_interp_ftndescs [15].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "GetPositionInternal", 2);
  }
  ((WasmInterpEntrySig_15)wasm_native_to_interp_ftndescs [15].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [15].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_16) (int*, int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_SeekInternal (void * arg0, void * arg1, void * arg2) { 
  int32_t res;
  if (!(WasmInterpEntrySig_16)wasm_native_to_interp_ftndescs [16].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "SeekInternal", 3);
  }
  ((WasmInterpEntrySig_16)wasm_native_to_interp_ftndescs [16].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [16].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_17) (int*, int*, int*, int*, int*);
int32_t wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_MoveInternal (void * arg0, void * arg1, int32_t arg2) { 
  int32_t res;
  if (!(WasmInterpEntrySig_17)wasm_native_to_interp_ftndescs [17].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "MoveInternal", 3);
  }
  ((WasmInterpEntrySig_17)wasm_native_to_interp_ftndescs [17].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, wasm_native_to_interp_ftndescs [17].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_18) (int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_GetLengthInternal (void * arg0, void * arg1) { 
  void * res;
  if (!(WasmInterpEntrySig_18)wasm_native_to_interp_ftndescs [18].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "GetLengthInternal", 2);
  }
  ((WasmInterpEntrySig_18)wasm_native_to_interp_ftndescs [18].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [18].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_19) (int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_DuplicateInternal (void * arg0, void * arg1) { 
  void * res;
  if (!(WasmInterpEntrySig_19)wasm_native_to_interp_ftndescs [19].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "DuplicateInternal", 2);
  }
  ((WasmInterpEntrySig_19)wasm_native_to_interp_ftndescs [19].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [19].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_20) (int*, int*, int*, int*);
void * wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_ForkInternal (void * arg0, void * arg1) { 
  void * res;
  if (!(WasmInterpEntrySig_20)wasm_native_to_interp_ftndescs [20].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "ForkInternal", 2);
  }
  ((WasmInterpEntrySig_20)wasm_native_to_interp_ftndescs [20].func) ((int*)&res, (int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [20].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_21) (int*, int*, int*);
void wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_DestroyInternal (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_21)wasm_native_to_interp_ftndescs [21].func) {
   mono_wasm_marshal_get_managed_wrapper ("SkiaSharp","SkiaSharp", "SKAbstractManagedStream", "DestroyInternal", 2);
  }
  ((WasmInterpEntrySig_21)wasm_native_to_interp_ftndescs [21].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [21].arg);
}

typedef void (*WasmInterpEntrySig_22) (int*, int*, int*, int*, int*, int*, int*, int*);
int32_t wasm_native_to_interp_Internal_Runtime_InteropServices_System_Private_CoreLib_ComponentActivator_GetFunctionPointer (void * arg0, void * arg1, void * arg2, void * arg3, void * arg4, void * arg5) { 
  int32_t res;
  if (!(WasmInterpEntrySig_22)wasm_native_to_interp_ftndescs [22].func) {
   mono_wasm_marshal_get_managed_wrapper ("System.Private.CoreLib","Internal.Runtime.InteropServices", "ComponentActivator", "GetFunctionPointer", 6);
  }
  ((WasmInterpEntrySig_22)wasm_native_to_interp_ftndescs [22].func) ((int*)&res, (int*)&arg0, (int*)&arg1, (int*)&arg2, (int*)&arg3, (int*)&arg4, (int*)&arg5, wasm_native_to_interp_ftndescs [22].arg);
  return res;
}

typedef void (*WasmInterpEntrySig_23) (int*, int*, int*);
void wasm_native_to_interp_System_Globalization_System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback (void * arg0, void * arg1) { 
  if (!(WasmInterpEntrySig_23)wasm_native_to_interp_ftndescs [23].func) {
   mono_wasm_marshal_get_managed_wrapper ("System.Private.CoreLib","System.Globalization", "CalendarData", "EnumCalendarInfoCallback", 2);
  }
  ((WasmInterpEntrySig_23)wasm_native_to_interp_ftndescs [23].func) ((int*)&arg0, (int*)&arg1, wasm_native_to_interp_ftndescs [23].arg);
}

typedef void (*WasmInterpEntrySig_24) (int*);
void wasm_native_to_interp_System_Threading_System_Private_CoreLib_ThreadPool_BackgroundJobHandler () { 
  if (!(WasmInterpEntrySig_24)wasm_native_to_interp_ftndescs [24].func) {
   mono_wasm_marshal_get_managed_wrapper ("System.Private.CoreLib","System.Threading", "ThreadPool", "BackgroundJobHandler", 0);
  }
  ((WasmInterpEntrySig_24)wasm_native_to_interp_ftndescs [24].func) (wasm_native_to_interp_ftndescs [24].arg);
}

typedef void (*WasmInterpEntrySig_25) (int*);
void wasm_native_to_interp_System_Threading_System_Private_CoreLib_TimerQueue_TimerHandler () { 
  if (!(WasmInterpEntrySig_25)wasm_native_to_interp_ftndescs [25].func) {
   mono_wasm_marshal_get_managed_wrapper ("System.Private.CoreLib","System.Threading", "TimerQueue", "TimerHandler", 0);
  }
  ((WasmInterpEntrySig_25)wasm_native_to_interp_ftndescs [25].func) (wasm_native_to_interp_ftndescs [25].arg);
}


static void *wasm_native_to_interp_funcs[] = {
    wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKBitmapReleaseDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKDataReleaseDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKImageTextureReleaseDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKSurfaceReleaseDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_GRGlGetProcDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_GRVkGetProcDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_DelegateProxies_SKGlyphPathDelegateProxyImplementation, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_ReadInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_PeekInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_IsAtEndInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_HasPositionInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_HasLengthInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_RewindInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_GetPositionInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_SeekInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_MoveInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_GetLengthInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_DuplicateInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_ForkInternal, wasm_native_to_interp_SkiaSharp_SkiaSharp_SKAbstractManagedStream_DestroyInternal, wasm_native_to_interp_Internal_Runtime_InteropServices_System_Private_CoreLib_ComponentActivator_GetFunctionPointer, wasm_native_to_interp_System_Globalization_System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback, wasm_native_to_interp_System_Threading_System_Private_CoreLib_ThreadPool_BackgroundJobHandler, wasm_native_to_interp_System_Threading_System_Private_CoreLib_TimerQueue_TimerHandler
};

// these strings need to match the keys generated in get_native_to_interp
static const char *wasm_native_to_interp_map[] = {
    "SkiaSharp_DelegateProxies_SKBitmapReleaseDelegateProxyImplementation", "SkiaSharp_DelegateProxies_SKDataReleaseDelegateProxyImplementation", "SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementationForCoTaskMem", "SkiaSharp_DelegateProxies_SKImageRasterReleaseDelegateProxyImplementation", "SkiaSharp_DelegateProxies_SKImageTextureReleaseDelegateProxyImplementation", "SkiaSharp_DelegateProxies_SKSurfaceReleaseDelegateProxyImplementation", "SkiaSharp_DelegateProxies_GRGlGetProcDelegateProxyImplementation", "SkiaSharp_DelegateProxies_GRVkGetProcDelegateProxyImplementation", "SkiaSharp_DelegateProxies_SKGlyphPathDelegateProxyImplementation", "SkiaSharp_SKAbstractManagedStream_ReadInternal", "SkiaSharp_SKAbstractManagedStream_PeekInternal", "SkiaSharp_SKAbstractManagedStream_IsAtEndInternal", "SkiaSharp_SKAbstractManagedStream_HasPositionInternal", "SkiaSharp_SKAbstractManagedStream_HasLengthInternal", "SkiaSharp_SKAbstractManagedStream_RewindInternal", "SkiaSharp_SKAbstractManagedStream_GetPositionInternal", "SkiaSharp_SKAbstractManagedStream_SeekInternal", "SkiaSharp_SKAbstractManagedStream_MoveInternal", "SkiaSharp_SKAbstractManagedStream_GetLengthInternal", "SkiaSharp_SKAbstractManagedStream_DuplicateInternal", "SkiaSharp_SKAbstractManagedStream_ForkInternal", "SkiaSharp_SKAbstractManagedStream_DestroyInternal", "System_Private_CoreLib_ComponentActivator_GetFunctionPointer", "System_Private_CoreLib_CalendarData_EnumCalendarInfoCallback", "System_Private_CoreLib_ThreadPool_BackgroundJobHandler", "System_Private_CoreLib_TimerQueue_TimerHandler"
};
