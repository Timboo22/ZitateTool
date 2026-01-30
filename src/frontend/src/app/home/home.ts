import {Component, signal} from '@angular/core';
import {InputText} from 'primeng/inputtext';
import "primeicons/primeicons.css";
import {TableModule} from 'primeng/table';
import {HttpClient} from '@angular/common/http';
import {FormsModule} from '@angular/forms';
import {JsonPipe} from '@angular/common';

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
    JsonPipe
  ],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {

  zitat = signal<Zitat>({id: 0,zitateName :""});

  gefundeneZitate = signal<Zitat[]>([]);

  constructor(private httpClient: HttpClient) {};

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
  }
  private  HoleZitateAusDb() {
    this.httpClient.get<Zitat[]>("http://localhost:5202/holeZitate").subscribe((res: any) => {
      this.gefundeneZitate.set(res);
    });
  }



}
