import {Component, computed, signal} from '@angular/core';
import {InputText} from 'primeng/inputtext';
import "primeicons/primeicons.css";
import {TableModule} from 'primeng/table';
import {HttpClient} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {MessageService} from 'primeng/api';
import {Select} from 'primeng/select';
import {Textarea} from 'primeng/textarea';
import {Button} from 'primeng/button';
import {environment} from '../../environments/environment';


interface Zitat {
  id: number;
  zitateName : string;
  BenutzerId : number;
  benutzerName?: string
}

interface PersonenModel {
  Id: number;
  Name: string | undefined;
  Lehrjahr: number;
  LieblingsZitat: string;
}

@Component({
  selector: 'app-home',
  imports: [
    TableModule,
    FormsModule,
    Select,
    Textarea,
    Button
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
  providers: [MessageService]
})
export class Home {

  private baseUrl = environment.apiUrl;

  zitat = signal<Zitat>({id: 0,zitateName :"", BenutzerId:0, benutzerName : "", });

  gefundeneZitate = signal<Zitat[]>([]);

  aktuelleZitate = computed(() =>
    this.gefundeneZitate().slice(-4)
  );

  items = signal<PersonenModel[]>([]);

  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService,)
  {};

  public ngOnInit() {
    this.HoleZitateAusDb();
    this.HoleExistierendeBenutzer();
  }
  public ErstelleZitat() {
    this.httpClient.post(this.baseUrl + "/erstelleZitat", this.zitat()).subscribe((res : any) =>{
      this.gefundeneZitate.update(list => [...list, res])
      this.HoleZitateAusDb();
    });
    this.zitat.set({
      id : 0,
      zitateName : "",
      BenutzerId : 0,
      benutzerName : "",
    });

    this.messageService.add({
      key: "toestZitateErstellen",
      severity: 'success',
      summary: 'Success',
      detail: 'Zitat wurde erstellt',
    })
  }
  private  HoleZitateAusDb() {
    this.httpClient.get<Zitat[]>(this.baseUrl + "/holeZitate").subscribe((res: any) => {
      this.gefundeneZitate.set(res);
    });
  }
  private HoleExistierendeBenutzer () {
    this.httpClient.get<PersonenModel[]>(this.baseUrl +'/holeExistierendenNutzer').subscribe((res : any) =>{
      this.items.set(res);
    });
  }

}
