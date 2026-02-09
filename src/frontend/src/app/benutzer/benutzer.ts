import {Component, signal} from '@angular/core';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { DatePickerModule } from 'primeng/datepicker';
import {FormsModule} from '@angular/forms';
import {InputText} from 'primeng/inputtext';
import {Select} from 'primeng/select';
import {InputGroup} from 'primeng/inputgroup';
import {InputGroupAddon} from 'primeng/inputgroupaddon';
import {Textarea} from 'primeng/textarea';
import {HttpClient} from '@angular/common/http';
import {Button} from 'primeng/button';
import {FileUpload} from 'primeng/fileupload';
import {Dialog} from 'primeng/dialog';
import {MessageService} from 'primeng/api';
import {Toast} from 'primeng/toast';

interface PersonenModel {
  Id: number;
  Name: string | undefined;
  Lehrjahr: number;
  LieblingsZitat: string;
  AvatarFileName : string;
}

interface SuchePersonenModel {
  id: number;
  name: string;
  avatarFileName: string,
  Lehrjahr: number;
}

@Component({
  selector: 'app-benutzer',
  imports: [ScrollPanelModule,
    DatePickerModule, FormsModule, InputText, Select, InputGroup, InputGroupAddon, Textarea, Button, FileUpload, Dialog, Toast],
  templateUrl: './benutzer.html',
  styleUrl: './benutzer.css',
  providers: [MessageService],
})

export class Benutzer {

  personenModelWritableSignal = signal<PersonenModel>({
    Id: 0,
    Name: '' ,
    Lehrjahr: 1,
    LieblingsZitat: '',
    AvatarFileName: '',
  });

  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService,)
  {};

  lehrjahre : number[] = [1 , 2 , 3] ;

  avatarFile = signal<FormData | null>(null);

  avatarPrieview = signal<string | null>("https://www.shutterstock.com/image-vector/default-avatar-social-media-display-600nw-2632690107.jpg");

  items = signal<PersonenModel[]>([]);

  keyWord = "https://www.shutterstock.com/image-vector/default-avatar-social-media-display-600nw-2632690107.jpg";

  triggerNameValidator = false;




  gesuchteBenutzer = signal<SuchePersonenModel[]>([]);
  ngOnInit(): void {
    this.HoleExistierendeBenutzer();
  }
  private HoleExistierendeBenutzer () {
      this.httpClient.get<PersonenModel[]>('http://localhost:5202/holeExistierendenNutzer').subscribe((res : any) =>{
      this.items.set(res);
    });
  }

  public TriggerNameValidatorDown(){
    this.triggerNameValidator = !this.personenModelWritableSignal().Name;
  }

  public onAvatarSelect (event: any) {
    const file = event.files[0];
    this.personenModelWritableSignal().AvatarFileName = file.name;
    this.SetzeProfilBild(file);
    this.CreateFormData(file);
  }
  public SucheBenutzer(suchbegriff: string): void {

    if (!suchbegriff || suchbegriff.trim().length < 2) {
      this.gesuchteBenutzer.set([]);
      return;
    }
    this.httpClient
      .get<SuchePersonenModel[]>(
        'http://localhost:5202/SuchBenutzer',
        {
          params: { suchBegriff: suchbegriff.trim() }
        }
      )
      .subscribe({
        next: (res) => {
          console.log('Suchergebnis:', res);
          this.gesuchteBenutzer.set(res ?? []);
        },
        error: (err) => {
          console.error('SucheBenutzer Fehler:', err);
          this.gesuchteBenutzer.set([]);
        }
      });
  }

  LoeschenAbfrageSichtbarkeit: boolean = false;
  BearbeitenAbfrageSichtbarkeit: boolean = false;

  AendereSichtbarkeitLoeschen() {
    this.LoeschenAbfrageSichtbarkeit = true;
  }

  AendereSichtbarkeitBearbeiten() {
    this.BearbeitenAbfrageSichtbarkeit = true;
  }
  public AbfrageLoeschen(id : number): void {
    this.LoeschenAbfrageSichtbarkeit = false;
    this.httpClient.post("http://localhost:5202/LoescheBenutzer", {id : id}).subscribe();
    this.SucheBenutzer("")
  }

  private SetzeProfilBild(file: any) {
    const url = URL.createObjectURL(file);
    this.avatarPrieview.set(url)
  }
  private CreateFormData (file: any) {

    const renamedFile = new File(
      [file],
      `${this.personenModelWritableSignal().Name}.png`,
      { type: file.type }
    );

    const formData = new FormData();
    console.log(this.personenModelWritableSignal());
    formData.append('File', renamedFile);
    this.avatarFile.set(formData);
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
    if(this.avatarPrieview()?.includes(this.keyWord)) {
      this.messageService.add({
        key: "fehlerBeimBenutzerErstellen",
        severity: 'error',
        summary: 'Fehler',
        detail: 'Bitte wähle ein Profilbild aus',
      })
    }

    if(this.personenModelWritableSignal().Name && !this.avatarPrieview()?.includes(this.keyWord))
    {
      this.UploadAvatarFile();
      this.httpClient.post("http://localhost:5202/benutzerHinzufuegen", this.personenModelWritableSignal()).subscribe();
      this.messageService.add({
        key: "toestBenutzerErstellen",
        severity: 'success',
        summary: 'Success',
        detail: 'Benutzer wurde erstellt',
      })

      this.personenModelWritableSignal.set({
        Id: 0,
        Name: '',
        Lehrjahr: 1,
        LieblingsZitat: '',
        AvatarFileName: '',
      });
    }
  }

  private UploadAvatarFile() {
    this.httpClient.post("http://localhost:5202/avatarBildUpload", this.avatarFile()).subscribe();
  }
}
