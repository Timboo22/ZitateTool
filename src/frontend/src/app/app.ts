import { Component, signal } from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {Navbar} from './navbar/navbar';
import {Card} from 'primeng/card';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, Navbar, Card],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('Sanoa');
}
