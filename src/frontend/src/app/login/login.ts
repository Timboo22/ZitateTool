import { Component, signal } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {Auth} from '../auth';
import {Router} from '@angular/router';
import {InputGroupAddon} from 'primeng/inputgroupaddon';
import {InputText} from 'primeng/inputtext';
import {InputGroup} from 'primeng/inputgroup';
import {Button} from 'primeng/button';

interface loginParams {
  username: string;
  password: string;
}
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, InputGroupAddon, InputText, InputGroup, Button],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {

  parms = signal<loginParams>({
    username: '',
    password: '',
  })

  constructor(public authService: Auth, private router: Router) {}

  onLogin() {
    this.authService.login(this.parms().username, this.parms().username).subscribe({
      next: () => {
        this.router.navigate(['/Home']) ;
      },
    });
  }
}
