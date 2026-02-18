import { Routes } from '@angular/router';
import {Home} from './home/home';
import {Benutzer} from './benutzer/benutzer';
import {LoginComponent} from './login/login';

export const routes: Routes = [
  {path: '', component: LoginComponent, pathMatch: 'full'},
  {path:"Home" , component: Home},
  {path:"Benutzer", component: Benutzer},
];
