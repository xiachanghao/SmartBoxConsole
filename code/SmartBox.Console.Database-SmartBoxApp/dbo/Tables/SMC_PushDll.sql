CREATE TABLE [dbo].[SMC_PushDll] (
    [pd_id]              INT            NOT NULL,
    [pd_name]            NVARCHAR (100) NULL,
    [pd_dll_filename]    NVARCHAR (100) NULL,
    [pd_xml_filename]    NVARCHAR (100) NULL,
    [pd_path]            NVARCHAR (MAX) NULL,
    [pd_status]          BIT            NULL,
    [pd_dll_status]      NVARCHAR (50)  NULL,
    [pd_zip_filename]    NVARCHAR (100) NULL,
    [pd_zip_extension]   NVARCHAR (100) NULL,
    [pd_zip_size]        INT            NULL,
    [pd_zip_contenttype] NVARCHAR (50)  NULL,
    [pd_createdtime]     DATETIME       NULL,
    [pd_updatetime]      DATETIME       NULL,
    CONSTRAINT [PK_SMC_PullDll] PRIMARY KEY CLUSTERED ([pd_id] ASC)
);

