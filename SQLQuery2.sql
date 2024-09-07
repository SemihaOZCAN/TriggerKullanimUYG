create trigger azalt
on TBLKITAP
after delete 
as
update TBLSAYAC set ADET=ADET-1