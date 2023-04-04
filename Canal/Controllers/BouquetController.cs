using System.Globalization;
using Microsoft.AspNetCore.Mvc;
namespace Canal.Controllers;

public class BouquetController : Controller
{
    public BouquetController(){
        UserBouquets ub = new UserBouquets();
        ub.verifierAbonnement();
    }
    public IActionResult Client(){
        return View();
    }
    public IActionResult Recharger(int idBouquet, int idUtilisateur, int idRechargement){
        Bouquet b = new Bouquet();
        b.IdBouquet = idBouquet;
        b = b.GetBouquetById();
        ViewBag.idUtilisateur = idUtilisateur;
        ViewBag.idRechargement = idRechargement;
        return View(b);
    }
    public IActionResult ListeChaine(int idBouquet){
        Chaine c = new Chaine();
        Bouquet b = new Bouquet();
        b.IdBouquet = idBouquet;
        b = b.GetBouquetById();
        ViewBag.Nom = b.GetNom();
        List<Chaine> cs = c.getChaineById(idBouquet);
        return View(cs);
    }
    public IActionResult Bouquets(int idUser) {
        Bouquet bouquet = new Bouquet();
        List<Bouquet> bouquets = bouquet.GetValideBouquets(idUser);
        ViewBag.idUser = idUser;
        return View(bouquets);
    }
    public IActionResult ChoixChaine(int idRegion){
        Chaine c = new Chaine();
        List<Chaine> cs = c.getListeChaine(idRegion);
        return View(cs);
    }
    public IActionResult Commande(int id){
        Bouquet b = new Bouquet();
        b.IdBouquet = id;
        return View(b);
    }
    public IActionResult Achats(){
        string idUtilisateur = Request.Form["idUtilisateur"];
        idUtilisateur = idUtilisateur.Trim();
        string dateDebut = Request.Form["date"];
        string nombreDeMois = Request.Form["nombre"];
        if(Request.Form.TryGetValue("bouquets[]", out var s)){
            foreach(var o in s){
                Console.WriteLine(o);
                Bouquet b = new Bouquet();
                b.IdBouquet  = int.Parse(o);
                b = b.GetBouquetById();
                UserBouquets ub = new UserBouquets();
                ub.dateDebut = DateTime.ParseExact(dateDebut, "yyyy-MM-dd'T'HH:mm", CultureInfo.InvariantCulture);
                ub.idBouquet = int.Parse(o);
                ub.idUtilisateur = int.Parse(idUtilisateur);
                ub.dateFin = ub.dateDebut.AddMonths(int.Parse(nombreDeMois));
                ub.etat = 1;
                ub.prix = b.GetTotalPrice()*(int.Parse(nombreDeMois));
                ub.insert();
            }
        }
        // foreach(string a in selectedBouquets){
        //     Console.WriteLine(a);
        // }
        return View();
    }
    public IActionResult Continu(){
        string idUtilisateur = Request.Form["idUtilisateur"];
        idUtilisateur = idUtilisateur.Trim();
        // string dateDebut = Request.Form["date"];
        string nombreDeMois = Request.Form["nombre"];
        nombreDeMois = nombreDeMois.Trim();
        string idBouquet = Request.Form["idBouquet"];
        idBouquet = idBouquet.Trim();
        string idRechargement = Request.Form["idRechargement"];

        UserBouquets ub = new UserBouquets();
        ub.idRechargement = int.Parse(idRechargement);
        ub = ub.getLastContract();

        Bouquet b = new Bouquet();
        b.IdBouquet  = int.Parse(idBouquet);
        b = b.GetBouquetById();

        UserBouquets ubn = new UserBouquets();
        ubn = ub;
        ubn.dateDebut = ub.dateFin;
        ubn.dateFin = ub.dateFin.AddMonths(int.Parse(nombreDeMois));
        Console.WriteLine(ubn.dateFin);
        
        Console.WriteLine(ubn.dateDebut);
        ubn.prix = b.GetTotalPrice()*int.Parse(nombreDeMois);
        ubn.insert();
        
        return RedirectToRoute(new { controller = "Bouquet", action="Client"});
    }
    // public IActionResult Acheter(string dateDebut, string durree, string idBouquet){
    //     UserBouquets us = new UserBouquets(1, (int) idBouquet,(DateTime) dateDebut,(DateTime) dateDebut, 1);
    //     us.insert();
    //     //Ilaina ny id an'ilay utilisateur mi submit
    //     return RedirectToRoute( new { controller="Bouquet", action="Bouquets",  idBouquet});
    // }
}