import { Component,OnInit } from '@angular/core';
import { CovidService } from './Services/covid.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'UdemySignalRAngular';

  constructor(public covidService:CovidService)  {}
  ngOnInit(): void {
    this.covidService.startConnection();
    this.covidService.startListener();
  }

  columnNames = ["Tarih", "Istanbul", "Manisa", "Yozgat", "Izmir","Ankara"];

  options:any={legend:{position:"Bottom"}};

}
