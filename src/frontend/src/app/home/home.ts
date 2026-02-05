import {Component, computed, signal} from '@angular/core';
import {InputText} from 'primeng/inputtext';
import "primeicons/primeicons.css";
import {TableModule} from 'primeng/table';
import {HttpClient} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {MessageService} from 'primeng/api';
import {Toast} from 'primeng/toast';
import {Select} from 'primeng/select';

interface Zitat {
  id: number;
  zitateName : string;
  BenutzerId : number;
}

interface PersonenModel {
  Id: number;
  Name: string | undefined;
  Lehrjahr: number;
  LieblingsZitat: string;
  AvatarFileName : string;
}

@Component({
  selector: 'app-home',
  imports: [
    InputText,
    TableModule,
    FormsModule,
    Toast,
    Select
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
  providers: [MessageService]
})
export class Home {

  zitat = signal<Zitat>({id: 0,zitateName :"", BenutzerId:0});

  gefundeneZitate = signal<Zitat[]>([]);

  aktuelleZitate = computed(() =>
    this.gefundeneZitate().slice(-7)
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
    this.httpClient.post("http://localhost:5202/erstelleZitat", this.zitat()).subscribe((res : any) =>{
      this.gefundeneZitate.update(list => [...list, res])
    });
    this.zitat.set({
      id : 0,
      zitateName : "",
      BenutzerId : 0,
    });

    this.messageService.add({
      key: "toestZitateErstellen",
      severity: 'success',
      summary: 'Success',
      detail: 'Zitat wurde erstellt',
    })
  }
  private  HoleZitateAusDb() {
    this.httpClient.get<Zitat[]>("http://localhost:5202/holeZitate").subscribe((res: any) => {
      this.gefundeneZitate.set(res);
    });
  }
  private HoleExistierendeBenutzer () {
    this.httpClient.get<PersonenModel[]>('http://localhost:5202/holeExistierendenNutzer').subscribe((res : any) =>{
      this.items.set(res);
    });
  }

}
