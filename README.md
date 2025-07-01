# Portfolio de Sacha Machado

Ce dépôt contient le code source de mon site portfolio développé en **Blazor WebAssembly** avec une **API ASP.NET Core** côté serveur.

Le site permet de :
- Présenter mon parcours et mes compétences
- Me contacter via un formulaire (envoi d’email via SMTP Gmail)
- Télécharger mon CV
- Voir et lancer mes différents projets
- Être hébergé sur Netlify

## Structure

- `Portfolio.Client` : le front-end (Blazor WebAssembly)
- `Portfolio.Server` : l'API (formulaire de contact)
- `wwwroot` : dossier généré à publier sur Netlify

## Fonctionnement du formulaire de contact

- Utilise `Pxxx@gmail.com` pour envoyer les messages
- Les messages sont redirigés automatiquement vers `Mxxx@hotmail.com`
- Validation des champs côté client et serveur

## Déploiement

Le site est hébergé gratuitement sur [Netlify](https://www.netlify.com).
