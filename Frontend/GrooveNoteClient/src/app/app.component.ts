import { Component, OnInit } from '@angular/core';
import { AppBootrapService } from './app-bootrap.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {  

  constructor(private appBootstrapService : AppBootrapService) {   
    
  }

  ngOnInit(): void {
    this.appBootstrapService.initializeApp();
  }
}
