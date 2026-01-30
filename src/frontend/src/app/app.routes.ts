import { Routes } from '@angular/router';
import {Home} from './home/home';
import {Benutzer} from './benutzer/benutzer';

export const routes: Routes = [
  {path: '', component: Home, pathMatch: 'full'},
  {path:"Home" , component: Home},
  {path:"Benutzer", component: Benutzer},
];
