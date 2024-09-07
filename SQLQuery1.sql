Create trigger arttir
on TBLKITAP
after insert 
as
update TBLSAYAC set ADET=ADET+1