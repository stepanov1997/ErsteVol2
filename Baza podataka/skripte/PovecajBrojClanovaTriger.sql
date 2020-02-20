use erste;

delimiter %%
create trigger povecaj_broj_clanova after insert
on polaznik_grupa
for each row
begin
     update grupa
     set BrojClanova = BrojClanova+1
     where new.GrupaId=Id;
end%%
delimiter ;