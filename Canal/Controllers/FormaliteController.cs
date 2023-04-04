using Microsoft.AspNetCore.Mvc;
using Canal.Models;

namespace Canal.Controllers;

public class FormaliteController : Controller
{
    public IActionResult Accueil(string email, string password) {
    Utilisateur utilisateur = new Utilisateur();
    utilisateur = utilisateur.VerifierUtilisateur(email, password);
        if (utilisateur != null) {
            UserValideBouquet us = new UserValideBouquet();
            us.idUtilisateur = utilisateur.IdUtilisateur;
            List<UserValideBouquet> lus = us.GetUserValideBouquet();
            ViewBag.idUtilisateur = utilisateur.IdUtilisateur;
            return View(lus);
        } else {
            ViewBag.Erreur = "Identifiants incorrects. Veuillez réessayer.";
            return RedirectToRoute( new {controller="Formalite", action="Login"});
        }
    }

    public IActionResult Inscrire([FromBody] Utilisateur utilisateur){
        if(ModelState.IsValid){
            return View();
        }
        else{
            return View();
        }
    }
    public IActionResult Index(){
        return RedirectToRoute(new { controller = "Formalite", action = "Login"});
    }
    public IActionResult Login(){
        return View();
    }
    public IActionResult Inscription(){
        return View();
    }
}
