use erste;

insert into osoba(Id,Ime,Prezime,BrojTelefona,Email, Vazeci) VALUES(1,"Marko","Markovic","065/000-000","marko.markovic@mail.com", true);
insert into osoba(Id,Ime,Prezime,BrojTelefona,Email, Vazeci) VALUES(2,"Mirko","Mirkovic","065/000-000","mirko.mirkovic@mail.com", true);
insert into osoba(Id,Ime,Prezime,BrojTelefona,Email, Vazeci) VALUES(3,"Nikola","Nikolic","065/000-000","nikola.nikolic@mail.com", true);
insert into osoba(Id,Ime,Prezime,BrojTelefona,Email, Vazeci) VALUES(4,"Jovan","Jovanovic","065/000-000","jovan.jovanovic@mail.com", true);

-- lozinka je "lozinka"
insert into administrator(Id,KorisnickoIme,LozinkaHash) values(1,"marko","0851f9cb7ed5c951298d9387b06985bf8fd15f98b4e700c81cc4adeddcd8c2cd");
insert into administrator(Id,KorisnickoIme,LozinkaHash) values(2,"mirko","0851f9cb7ed5c951298d9387b06985bf8fd15f98b4e700c81cc4adeddcd8c2cd");
insert into sluzbenik(Id,KorisnickoIme,LozinkaHash) values(3,"nikola","0851f9cb7ed5c951298d9387b06985bf8fd15f98b4e700c81cc4adeddcd8c2cd");
insert into sluzbenik(Id,KorisnickoIme,LozinkaHash) values(4,"jovan","0851f9cb7ed5c951298d9387b06985bf8fd15f98b4e700c81cc4adeddcd8c2cd");

insert into jezik(Id,Naziv, Vazeci) values(1,"Engleski", true);
insert into jezik(Id,Naziv, Vazeci) values(2,"Njemacki", true);
insert into jezik(Id,Naziv, Vazeci) values(3,"Francuski", true);

insert into kurs(Id,Nivo,JezikId, Vazeci) values(1,"A1.1",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(2,"A1.2",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(3,"A2.1",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(4,"A2.2",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(5,"B1.1",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(6,"B1.2",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(7,"B2.1",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(8,"B2.2",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(9,"C1.1",1, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(10,"C1.2",1, true);

insert into kurs(Id,Nivo,JezikId, Vazeci) values(11,"A1.1",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(12,"A1.2",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(13,"A2.1",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(14,"A2.2",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(15,"B1.1",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(16,"B1.2",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(17,"B2.1",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(18,"B2.2",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(19,"C1.1",2, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(20,"C1.2",2, true);

insert into kurs(Id,Nivo,JezikId, Vazeci) values(21,"A1.1",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(22,"A1.2",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(23,"A2.1",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(24,"A2.2",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(25,"B1.1",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(26,"B1.2",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(27,"B2.1",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(28,"B2.2",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(29,"C1.1",3, true);
insert into kurs(Id,Nivo,JezikId, Vazeci) values(30,"C1.2",3, true);

insert into osoba(Id,Ime,Prezime,Email,BrojTelefona, Vazeci) values(5,"Jovana","Jovanovic","jovana.jovanovic","065/000-000", true);
insert into osoba(Id,Ime,Prezime,Email,BrojTelefona, Vazeci) values(6,"Mira","Mirkovic","mira.mirkovic","065/000-000", true);

insert into profesor(Id) values(5);
insert into profesor(Id) values(6);