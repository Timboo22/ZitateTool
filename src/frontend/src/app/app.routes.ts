import {Router, Routes} from '@angular/router';
import {Home} from './home/home';
import {Benutzer} from './benutzer/benutzer';
import {LoginComponent} from './login/login';
import { inject } from "@angular/core";

const authGuard = () => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  if (token) {
    return true;
  }

  router.navigate(['/Login']);
  return false;
};
export const routes: Routes = [
  { path: '', component: Home, pathMatch: 'full', canActivate: [authGuard] },
  { path: "Login", component: LoginComponent},
  { path: "Home", component: Home, canActivate: [authGuard] },
  { path: "Benutzer", component: Benutzer, canActivate: [authGuard] },
];
