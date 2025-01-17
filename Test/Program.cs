﻿using System.ComponentModel.DataAnnotations;
using RotRut;
using RotRut.Begaran.Rot;
using RotRut.Begaran.Rut;

await ExportRequest();

static async Task ExportRequest()
{
    var begaran1 = new RotRut.Begaran.BegaranFil()
    {
        NamnPaBegaran = "Foo",
        RotBegaran = new RotBegaran()
        {
            Arenden = new RotBegaranArenden[] {
                        new() {
                            BetalningsDatum = DateTime.Now.Date,
                            FakturaNr = "1234",
                            Kopare = "195705277860",
                            BetaltBelopp = 2500,
                            BegartBelopp = 1250,
                            Fastighetsbeteckning = "",
                            LagenhetsNr = 1234,
                            BrfOrgNr = "232445",
                            UtfortArbete = new RotBegaranArendenUtfortArbete() {
                                Bygg = new RotBegaranArendenUtfortArbeteBygg() {
                                    AntalTimmar = 1.5
                                }
                            }
                        }
                    }
        }
    };

    var begaran2 = new RotRut.Begaran.BegaranFil()
    {
        NamnPaBegaran = "Foo",
        HushallBegaran = new HushallBegaran()
        {
            Arenden = new HushallBegaranArenden[]
            {
                        new () {
                            BetalningsDatum = DateTime.Now.Date,
                            FakturaNr = "1234",
                            Kopare = "198912177121",
                            BetaltBelopp = 2500,
                            BegartBelopp = 1250,
                            PrisForArbete = 1000,
                            UtfortArbete = new HushallBegaranArendenUtfortArbete() {
                                Stadning = new HushallBegaranArendenUtfortArbeteStadning() {
                                    AntalTimmar = 1.5
                                }
                            }
                        }
            }
        }
    };

    if (File.Exists("Begaran.xml"))
        File.Delete("Begaran.xml");

    List<ValidationResult> results = new List<ValidationResult>();
    RotRutBegaran.Validate(begaran2, results);

    using (var file2 = File.OpenWrite("Begaran.xml"))
    {
        RotRutBegaran.Serialize(file2, begaran2);
    }

    RotRut.Begaran.BegaranFil begaran = ImportRequest();

    await Task.CompletedTask;
}

static RotRut.Begaran.BegaranFil ImportRequest()
{
    using var file = File.OpenRead(Path.Combine("Begaran.xml"));
    return RotRutBegaran.Deserialize(file);
}