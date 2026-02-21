import {Component, signal} from '@angular/core';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { DatePickerModule } from 'primeng/datepicker';
import {FormsModule} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {Select} from 'primeng/select';
import {InputGroup} from 'primeng/inputgroup';
import {InputGroupAddon} from 'primeng/inputgroupaddon';
import {HttpClient} from '@angular/common/http';
import {Button} from 'primeng/button';
import {MessageService} from 'primeng/api';
import {environment} from '../../environments/environment';

interface PersonenModel {
  Id: number;
  Name: string | undefined;
  Lehrjahr: number;
  LieblingsZitat: string;
}

@Component({
  selector: 'app-benutzer',
  imports: [ScrollPanelModule,
    DatePickerModule, FormsModule, InputText, Select, InputGroup, InputGroupAddon, Button],
  templateUrl: './benutzer.html',
  styleUrl: './benutzer.css',
  providers: [MessageService],
})

export class Benutzer {

  private baseUrl = environment.apiUrl;

  personenModelWritableSignal = signal<PersonenModel>({
    Id: 0,
    Name: '' ,
    Lehrjahr: 1,
    LieblingsZitat: '',
  });

  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService,)
  {};

  lehrjahre : number[] = [1 , 2 , 3] ;

  triggerNameValidator = false;

  public TriggerNameValidatorDown(){
    this.triggerNameValidator = !this.personenModelWritableSignal().Name;
  }

  public ErstelleBenutzer()  {
    if(!this.personenModelWritableSignal().Name) {
      this.messageService.add({
        key: "fehlerBeimBenutzerErstellen",
        severity: 'error',
        summary: 'Fehler',
        detail: 'Bitte wähle einen Namen aus',
      })
    }
      this.httpClient.post(this.baseUrl + "/benutzerHinzufuegen", this.personenModelWritableSignal()).subscribe();
      this.messageService.add({
        key: "toastBenutzerErstellen",
        severity: 'success',
        summary: 'Success',
        detail: 'Benutzer wurde erstellt',
      })

      this.personenModelWritableSignal.set({
        Id: 0,
        Name: '',
        Lehrjahr: 1,
        LieblingsZitat: '',
      });
    }}



