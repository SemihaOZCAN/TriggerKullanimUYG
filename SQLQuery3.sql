USE [TriggerKullanimi]
GO
/****** Object:  Trigger [dbo].[yedek]    Script Date: 7.09.2024 20:03:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 ALTER trigger [dbo].[yedek] 
 on [dbo].[TBLKITAP]
 after delete 
 as
 declare @KITAPAD varchar(150)
 declare @KITAPYAZAR varchar(50)
 declare @KITAPSAYFA char(3)
 declare @KITAPYAYIN varchar(30)
 declare @KITAPTUR varchar(20)
 
 select @KITAPAD=AD,@KITAPYAZAR=YAZAR,@KITAPSAYFA=SAYFA,@KITAPYAYIN=YAYIN,@KITAPTUR=TUR from deleted
 insert into TBLKITAPYEDEK(AD,YAZAR,SAYFA,YAYIN,TUR) values (@KITAPAD,@KITAPYAZAR,@KITAPSAYFA,@KITAPYAYIN,@KITAPTUR)

