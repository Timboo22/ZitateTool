import {Component, computed, Inject, signal} from '@angular/core';
import {InputText} from 'primeng/inputtext';
import "primeicons/primeicons.css";
import {TableModule} from 'primeng/table';
import {HttpClient} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {MessageService} from 'primeng/api';
import {Toast} from 'primeng/toast';

interface Zitat {
  id: number;
  zitateName : string;
}
@Component({
  selector: 'app-home',
  imports: [
    InputText,
    TableModule,
    FormsModule,
    Toast
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
  providers: [MessageService]
})
export class Home {

  zitat = signal<Zitat>({id: 0,zitateName :""});

  gefundeneZitate = signal<Zitat[]>([]);

  aktuelleZitate = computed(() =>
    this.gefundeneZitate().slice(-7)
  );
  constructor(
    private httpClient: HttpClient,
    private messageService: MessageService,)
  {};

  public ngOnInit() {
    this.HoleZitateAusDb();
  }
  public ErstelleZitat() {
    this.httpClient.post("http://localhost:5202/erstelleZitat", this.zitat()).subscribe((res : any) =>{
      this.gefundeneZitate.update(list => [...list, res])
    });
    this.zitat.set({
      id : 0,
      zitateName : "",
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
}
